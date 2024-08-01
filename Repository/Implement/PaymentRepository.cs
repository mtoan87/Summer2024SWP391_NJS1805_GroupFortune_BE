using DAL.Enums;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class PaymentRepository : RepositoryGeneric<Payment> , IPaymentRepository
    {
        public PaymentRepository(JewelryAuctionContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Payment>> GetPaymentByAccountId(int accountId)
        {
            return await _context.Payments               
                .Where(a => a.AccountId == accountId)
                .ToListAsync();
        }
        public async Task<bool> ProcessPaymentAsync(Payment payment)
        {
            // Check if the auction result has already been paid
            bool isAlreadyPaid = await _context.Payments
                .AnyAsync(p => p.AuctionResultId == payment.AuctionResultId && p.Status == AuctionStatus.Successful.ToString());

            if (isAlreadyPaid)
                throw new InvalidOperationException("This auction result has already been paid.");
            // Validate the payment and retrieve related data
            var auctionResult = await _context.AuctionResults
                .Include(ar => ar.Joinauction)
                .FirstOrDefaultAsync(ar => ar.AuctionresultId == payment.AuctionResultId);

            if (auctionResult == null || auctionResult.Status != "Win")
                throw new InvalidOperationException("Invalid auction result or status.");

            var winningAccountWallet = await _context.AccountWallets
                .FirstOrDefaultAsync(aw => aw.AccountId == auctionResult.AccountId);

            var auction = await _context.Auctions
                .FirstOrDefaultAsync(a => a.AuctionId == auctionResult.Joinauction.AuctionId);

            if (auction == null)
                throw new InvalidOperationException("Auction not found.");

            var auctionOwnerWallet = await _context.AccountWallets
                .FirstOrDefaultAsync(aw => aw.AccountId == auction.AccountId);

            if (auctionOwnerWallet == null || winningAccountWallet == null)
                throw new InvalidOperationException("Invalid account wallets.");

            // Set the price, total price with 30% increase, and fee
            payment.Price = auctionResult.Price;
            payment.Totalprice = payment.Price * 1.3;
            payment.Fee = payment.Totalprice - payment.Price;

            // Ensure the winning account has sufficient budget
            if (winningAccountWallet.Budget < payment.Totalprice)
                throw new InvalidOperationException("Insufficient budget.");

            // Update the wallets
            winningAccountWallet.Budget -= payment.Totalprice;
            auctionOwnerWallet.Budget += payment.Price;

            // Record the wallet transactions
            var winningAccountTransaction = new WalletTransaction
            {
                AccountwalletId = winningAccountWallet.AccountwalletId,
                Amount = -payment.Totalprice,
                DateTime = DateTime.UtcNow
            };

            var auctionOwnerTransaction = new WalletTransaction
            {
                AccountwalletId = auctionOwnerWallet.AccountwalletId,
                Amount = payment.Price,
                DateTime = DateTime.UtcNow
            };

            await _context.WalletTransactions.AddRangeAsync(winningAccountTransaction, auctionOwnerTransaction);

            // Save the changes
            await _context.SaveChangesAsync();

            return true;
        }
        public double GetTotalFees()
        {
            return _context.Payments.Sum(p => p.Fee);
        }

        public double GetTotalPrice()
        {
            return _context.Payments.Sum(p => p.Totalprice);
        }

        public double GetPrice()
        {
            return _context.Payments.Sum(p => p.Price);
        }
        public IEnumerable<object> GetFeesStatisticsByDate()
        {
            return _context.Payments
                .GroupBy(p => p.Date.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    TotalFees = g.Sum(p => p.Fee),
                    TotalPrice = g.Sum(p => p.Totalprice)
                })
                .ToList();
        }

        public IEnumerable<object> GetFeesStatisticsByMonth()
        {
            return _context.Payments
                .GroupBy(p => new { p.Date.Year, p.Date.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalFees = g.Sum(p => p.Fee),
                    TotalPrice = g.Sum(p => p.Totalprice)
                })
                .ToList();
        }
    }
}
