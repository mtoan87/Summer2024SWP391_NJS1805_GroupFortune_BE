using DAL.DTO.JewelryDTO.GoldDiamond;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IJewelryGoldDiamondService
    {
        Task<IEnumerable<JewelryGoldDiamond>> GetAllGoldDiaJewelries();
        Task<JewelryGoldDiamond> GetJewelryById(int id);
        Task<JewelryGoldDiamond> CreateJewelry(CreateJewelryGoldDiamondDTO createjew);
        Task<JewelryGoldDiamond> UpdateJewelryMember(int id, UpdateJewelryGoldDiaDTO updateJewelry);
        Task<JewelryGoldDiamond> UpdateJewelryStaff(int id, UpdateJewelryGoldDiamondStaffDTO updateJewelry);
        Task<JewelryGoldDiamond> UpdateJewelryManager(int id, UpdateJewelryGoldDiamondManagerDTO updateJewelry);
        Task<JewelryGoldDiamond> DeleteJewelry(int id);
        Task<IEnumerable<JewelryGoldDiamond>> GetAuctionAndJewelryGoldDiaByAccountIdAsync(int accountId);
    }
}
