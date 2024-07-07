using DAL.DTO.JewelryDTO.Gold;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IJewelryGoldService
    {
        Task<IEnumerable<JewelryGold>> GetAllGoldJewelries();
        Task<JewelryGold> GetJewelryById(int id);
        Task<JewelryGold> CreateJewelry(CreateJewelryGoldDTO createjew);
        Task<JewelryGold> UpdateJewelryMember(int id, UpdateJewelryDTO updateJewelry);
        Task<JewelryGold> UpdateJewelryStaff(int id, UpdateJewelryStaffDTO updateJewelry);
        Task<JewelryGold> UpdateJewelryManager(int id, UpdateJewelryManagerDTO updateJewelry);
        Task<JewelryGold> DeleteJewelry(int id);
        Task<IEnumerable<JewelryGold>> GetAuctionAndJewelryGoldByAccountIdAsync(int accountId);
    }
}
