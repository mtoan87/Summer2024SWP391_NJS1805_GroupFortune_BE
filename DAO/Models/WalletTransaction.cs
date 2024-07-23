using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class WalletTransaction
    {
        public int TransactionId { get; set; }
        public int? AccountwalletId { get; set; }
        public double? Amount { get; set; }
        public DateTime? DateTime { get; set; }

        public virtual AccountWallet? Accountwallet { get; set; }
    }
}
