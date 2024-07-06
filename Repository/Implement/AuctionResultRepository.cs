using DAL.Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class AuctionResultRepository : RepositoryGeneric<AuctionResult> , IAuctionResultRepository
    {
        public AuctionResultRepository(JewelryAuctionContext context) : base(context)
        {
            
        }
       
        
    }
}
