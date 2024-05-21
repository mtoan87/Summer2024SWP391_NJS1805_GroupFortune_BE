using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Role
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
            JoinAuctions = new HashSet<JoinAuction>();
        }

        public int RoleId { get; set; }
        public string? RoleName { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<JoinAuction> JoinAuctions { get; set; }
    }
}
