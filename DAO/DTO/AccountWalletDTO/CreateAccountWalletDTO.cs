using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.AccountWalletDTO
{
    public class CreateAccountWalletDTO
    {
        public int? AccountId { get; set; }
        public string BankName { get; set; } = null!;
        public double BankNo { get; set; }
        public double Budget { get; set; }
    }
}
