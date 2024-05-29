using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Authenticate
{
    public class LoginModel
    {
        public string AccountEmail { get; set; } = null!;
        public string AccountPassword { get; set; } = null!;
    }
}
