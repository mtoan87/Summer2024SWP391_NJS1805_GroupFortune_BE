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
    }
}
