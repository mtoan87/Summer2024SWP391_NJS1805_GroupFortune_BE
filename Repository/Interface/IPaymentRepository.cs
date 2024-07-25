using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IPaymentRepository : IRepositoryGeneric<Payment>
    {
        Task<IEnumerable<Payment>> GetPaymentByAccountId(int accountId);

        Task<bool> ProcessPaymentAsync(Payment payment);
    }
}
