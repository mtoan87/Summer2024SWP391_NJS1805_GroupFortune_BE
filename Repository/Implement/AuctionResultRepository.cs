using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class AuctionResultRepository : RepositoryGeneric<AuctionResult>
    {
        public AuctionResultRepository(JewelryAuctionContext context) : base(context)
        {
            
        }
       
        
    }
}
