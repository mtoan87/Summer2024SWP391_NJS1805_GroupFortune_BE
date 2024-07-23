using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.WalletTransaction
{
    public class WalletTransactionDTO
    {
        public int AccountId { get; set; }
        public string BankName { get; set; }
        public double BankNo { get; set; }
        public double Amount { get; set; }
    }
}
