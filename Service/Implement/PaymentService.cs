﻿using DAL.DTO.BidDTO;
using DAL.DTO.PaymentDTO;
using DAL.Models;
using Repository.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class PaymentService
    {
        private readonly PaymentRepository _paymentRepository;
        public PaymentService(PaymentRepository paymentRepository)
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

        public async Task<Payment> CreatePayment(CreatePaymentDTO createPayment)
        {
            var newPayment = new Payment
            {
                AccountId = createPayment.AccountId,
                AuctionResultId = createPayment.AuctionResultId,
                Status = createPayment.Status,
                Paymentmethod = createPayment.Paymentmethod,
                Date = createPayment.Date, 
                Price = createPayment.Price,
                Totalprice = createPayment.Totalprice,
                Fee = createPayment.Fee,
                
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
