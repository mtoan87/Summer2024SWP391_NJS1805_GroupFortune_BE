using DAL.Models;
using Repository.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class JoinAuctionService
    {
        private readonly JoinAuctionRepository _joinAuctionRepository;
        public JoinAuctionService(JoinAuctionRepository joinAuctionRepository)
        {
            _joinAuctionRepository = joinAuctionRepository;
        }
        public IEnumerable<JoinAuction> GetAllJoinAuctions()
        {
            return _joinAuctionRepository.GetAllJoinAuction();
        }
    }
}
