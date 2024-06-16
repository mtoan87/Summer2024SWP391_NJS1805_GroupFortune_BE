using System;
using System.Collections.Generic;

namespace DAL.Model
{
    public partial class AccountWallet
    {
        public int AccountwalletId { get; set; }
        public int? AccountId { get; set; }
        public string BankName { get; set; } = null!;
        public int BankNo { get; set; }
        public int Budget { get; set; }

        public virtual Account? Account { get; set; }
    }
}
