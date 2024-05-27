using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class JewelryRepository : RepositoryGeneric<Jewelry>
    {
        public JewelryRepository(JewelryAuctionContext context) : base(context)
        {

        }
    }
}
