using DAL.DTO.PaymentDTO;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetAllPayments();
        Task<Payment> GetPaymentById(int id);
        Task<IEnumerable<Payment>> GetPaymentByAccountId(int id);
        Task<Payment> CreatePayment(CreatePaymentDTO createPayment);
        Task<Payment> UpdatePayment(int id, UpdatePaymentDTO updatePayment);
        Task<IEnumerable<Payment>> CreatePaymentAsync(CreatePaymentDTO paymentdto);
        IEnumerable<object> GetFeesStatisticsByMonth();
        IEnumerable<object> GetFeesStatisticsByDate();
        Task<Payment> DeletePayment(int id);
        double GetPrice();
        double GetTotalPrice();
        double GetTotalFees();
        
    }
}
