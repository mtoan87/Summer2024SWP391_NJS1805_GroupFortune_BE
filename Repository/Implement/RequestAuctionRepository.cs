using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class RequestAuctionRepository : RepositoryGeneric<Auction>
    {
        public RequestAuctionRepository(JewelryAuctionContext context) : base(context)
        {
            
        }
    }
}
