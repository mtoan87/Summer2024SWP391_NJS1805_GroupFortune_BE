using DAL.Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class JoinAuctionRepository : RepositoryGeneric<JoinAuction> , IJoinAuctionRepository
    {
        public JoinAuctionRepository(JewelryAuctionContext context) : base(context)
        {
            
        }
        
        
    }
}
