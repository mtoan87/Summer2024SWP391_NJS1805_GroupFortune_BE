using DAL.DTO.JoinAuctionDTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IJoinAuctionService
    {
        Task<IEnumerable<JoinAuction>> GetAllJoinAuctions();
        Task<IEnumerable<JoinAuction>> GetAuctionByJoinauctionIdAsync(int joinAuctionId);
        Task<IEnumerable<JoinAuction>> GetJoinAuctionByAccountIdAsync(int accountId);
        Task<JoinAuction> GetJoinAuctionById(int id);
        Task<JoinAuction> CreateJoinAuction(CreateJoinAuctionDTO createJoinAuction);
        Task<bool> CanJoinAuction(int accountId, int auctionId);
        Task<JoinAuction> UpdateJoinAuction(int id, UpdateJoinAuctionDTO updateJoinAuction);
        Task<JoinAuction> DeleteJoinAuction(int id);
    }
}
