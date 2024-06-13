using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class AccountWallet
    {
        public int AccountwalletId { get; set; }
        public int? AccountId { get; set; }
        public string BankName { get; set; } = null!;
        public double BankNo { get; set; }
        public double Budget { get; set; }

        public virtual Account? Account { get; set; }
    }
}
