using DAL.DTO.JewelryDTO.Silver;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IJewelrySilverService
    {
        Task<IEnumerable<JewelrySilver>> GetAllSilverJewelries();
        Task<JewelrySilver> GetJewelryById(int id);
        Task<JewelrySilver> CreateJewelry(CreateJewelrySilverDTO createjew);
        Task<JewelrySilver> UpdateJewelryMember(int id, UpdateJewelrySilverDTO updateJewelry);
        Task<JewelrySilver> UpdateJewelryStaff(int id, UpdateJewelrySilverStaffDTO updateJewelry);
        Task<JewelrySilver> UpdateJewelryManager(int id, UpdateJewelrySilverManagerDTO updateJewelry);
        Task<JewelrySilver> DeleteJewelry(int id);
        Task<IEnumerable<JewelrySilver>> GetAuctionAndJewelrySilverByAccountIdAsync(int accountId);
    }   

}
