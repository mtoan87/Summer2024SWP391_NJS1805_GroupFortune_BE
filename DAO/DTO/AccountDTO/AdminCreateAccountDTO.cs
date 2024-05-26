using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.AccountDTO
{
    public class AdminCreateAccountDTO
    {
        
        public string AccountName { get; set; } = null!;
        public string AccountEmail { get; set; } = null!;
        public string AccountPassword { get; set; } = null!;
        public string AccountPhone { get; set; } = null!;
        public int? RoleId { get; set; }
    }
}
