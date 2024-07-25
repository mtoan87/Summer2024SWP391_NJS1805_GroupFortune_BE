using DAL.DTO.AuctionResultDTO;
using DAL.DTO.BidDTO;
using DAL.DTO.PaymentDTO;
using DAL.Models;
using Repository.Implement;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            return await _paymentRepository.GetAllAsync();
        }

        public async Task<Payment> GetPaymentById(int id)
        {
            return await _paymentRepository.GetByIdAsync(id);
        }
        
        public async Task<IEnumerable<Payment>> GetPaymentByAccountId(int id)
        {
            return await _paymentRepository.GetPaymentByAccountId(id);
        }

        private Payment ConvertDtoToEntity(CreatePaymentDTO dto)
        {
            return new Payment
            {
                AuctionResultId = dto.AuctionResultId,
                Date = DateTime.Now,
                Status = dto.Status,               
                AccountId = dto.AccountId,
                Paymentmethod = "Wallet"
            };
        }
        public async Task<IEnumerable<Payment>> CreatePaymentAsync(CreatePaymentDTO paymentdto)
        {
            var payment = ConvertDtoToEntity(paymentdto);
            await _paymentRepository.AddAsync(payment);
            bool processed = await _paymentRepository.ProcessPaymentAsync(payment);
            if (!processed) 
                return Enumerable.Empty<Payment>();
            await _paymentRepository.SaveChangesAsync();
            return await _paymentRepository.GetAllAsync();  
        }
        public async Task<Payment> CreatePayment(CreatePaymentDTO createPayment)
        {
            var newPayment = new Payment
            {
                AccountId = createPayment.AccountId,
                AuctionResultId = createPayment.AuctionResultId,
                Status = createPayment.Status,
               // Paymentmethod = createPayment.Paymentmethod,
               // Date = createPayment.Date, 
                //Price = createPayment.Price,
                //Totalprice = createPayment.Totalprice,
               // Fee = createPayment.Fee,
                
            };
            await _paymentRepository.AddAsync(newPayment);
            await _paymentRepository.SaveChangesAsync();
            return newPayment;
        }

        public async Task<Payment> UpdatePayment(int id, UpdatePaymentDTO updatePayment)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
            {
                throw new Exception($"Payment with ID{id} not found");
            }
            payment.AccountId = updatePayment.AccountId;
            payment.AuctionResultId = updatePayment.AuctionResultId;
            payment.Status = updatePayment.Status;
            payment.Paymentmethod = updatePayment.Paymentmethod;
            payment.Date = updatePayment.Date;  
            payment.Price = updatePayment.Price;
            payment.Totalprice = updatePayment.Totalprice;
            payment.Fee = updatePayment.Fee;       
            await _paymentRepository.UpdateAsync(payment);

            return payment;

        }
    }
}
