using DAL.DTO.JewelryDTO;
using DAL.Models;
using Repository.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class JewelryGoldDiaService
    {
        private readonly JewelryGoldDiaRepository _jewelryGoldDiaRepository;
        public JewelryGoldDiaService(JewelryGoldDiaRepository jewelryGoldDiaRepository)
        {
            _jewelryGoldDiaRepository = jewelryGoldDiaRepository;
        }
        public IEnumerable<JewelryGoldDiamond> GetAllGoldDiaJewelries()
        {
            return _jewelryGoldDiaRepository.GetAllJewelries();
        }
        public async Task<JewelryGoldDiamond> GetJewelryById(int id)
        {
            return await _jewelryGoldDiaRepository.GetByIdAsync(id);
        }
        public async Task<JewelryGoldDiamond> CreateJewelry(CreateJewelryGoldDiamondDTO createjew)
        {


            var newjewelry = new JewelryGoldDiamond
            {
                AccountId = createjew.AccountId,
                JewelryImg = createjew.JewelryImg,
                Name = createjew.Name,
                Materials = createjew.Materials,
                Category = createjew.Category,
                Description = createjew.Description,
                Clarity = createjew.Clarity,
                Carat = createjew.Carat,
                Weight = createjew.Weight,
                GoldAge = createjew.GoldAge,
                Status = "UnVerified",
            };
            await _jewelryGoldDiaRepository.AddAsync(newjewelry);

            await _jewelryGoldDiaRepository.SaveChangesAsync();
            return newjewelry;
        }
        public async Task<JewelryGoldDiamond> UpdateJewelryMember(int id, UpdateJewelryGoldDiaDTO updateJewelry)
        {
            var updjewelry = await _jewelryGoldDiaRepository.GetByIdAsync(id);
            if (updjewelry == null)
            {
                throw new Exception($"Jewelry with ID {id} not found.");
            }

            updjewelry.AccountId = updateJewelry.AccountId;
            updjewelry.JewelryImg = updateJewelry.JewelryImg;
            updjewelry.Name = updateJewelry.Name;
            updjewelry.Materials = updateJewelry.Materials;
            updjewelry.Description = updateJewelry.Description;
            updjewelry.Category = updateJewelry.Category;
            updjewelry.Clarity = updjewelry.Clarity; 
            updjewelry.Carat = updateJewelry.Carat;
            updjewelry.Weight = updateJewelry.Weight;
            updjewelry.GoldAge = updateJewelry.GoldAge;
            await _jewelryGoldDiaRepository.UpdateJewelryAsync(updjewelry);
            return updjewelry;
        }
        public async Task<JewelryGoldDiamond> UpdateJewelryStaff(int id, UpdateJewelryGoldDiamondStaffDTO updateJewelry)
        {
            var updjewelry = await _jewelryGoldDiaRepository.GetByIdAsync(id);
            if (updjewelry == null)
            {
                throw new Exception($"Jewelry with ID {id} not found.");
            }

            updjewelry.AccountId = updateJewelry.AccountId;
            updjewelry.JewelryImg = updateJewelry.JewelryImg;
            updjewelry.Name = updateJewelry.Name;
            updjewelry.Materials = updateJewelry.Materials;
            updjewelry.Description = updateJewelry.Description;
            updjewelry.Category = updateJewelry.Category;
            updjewelry.Carat = updjewelry.Carat;
            updjewelry.Clarity= updateJewelry.Clarity;
            updjewelry.Weight = updateJewelry.Weight;
            updjewelry.GoldAge = updateJewelry.GoldAge;
            updjewelry.Price = updateJewelry.Price;
            await _jewelryGoldDiaRepository.UpdateJewelryAsync(updjewelry);
            return updjewelry;
        }

        public async Task<JewelryGoldDiamond> UpdateJewelryManager(int id, UpdateJewelryGoldDiamondManagerDTO updateJewelry)
        {
            var updjewelry = await _jewelryGoldDiaRepository.GetByIdAsync(id);
            if (updjewelry == null)
            {
                throw new Exception($"Jewelry with ID {id} not found.");
            }

            updjewelry.AccountId = updateJewelry.AccountId;
            updjewelry.JewelryImg = updateJewelry.JewelryImg;
            updjewelry.Name = updateJewelry.Name;
            updjewelry.Materials = updateJewelry.Materials;
            updjewelry.Description = updateJewelry.Description;
            updjewelry.Category = updateJewelry.Category;
            updjewelry.Carat = updjewelry.Carat;
            updjewelry.Clarity = updateJewelry.Clarity;
            updjewelry.Weight = updateJewelry.Weight;
            updjewelry.GoldAge = updateJewelry.GoldAge;
            updjewelry.Price = updateJewelry.Price;
            updjewelry.Status = updateJewelry.Status;
            await _jewelryGoldDiaRepository.UpdateJewelryAsync(updjewelry);
            return updjewelry;
        }
        public async Task<JewelryGoldDiamond> DeleteJewelry(int id)
        {
            var account = await _jewelryGoldDiaRepository.GetByIdAsync(id);
            if (account == null)
            {
                throw new Exception($"Account with ID {id} not found.");
            }
            await _jewelryGoldDiaRepository.RemoveAsync(account);
            return account;
        }
        public async Task<IEnumerable<JewelryGoldDiamond>> GetAuctionAndJewelryGoldDiaByAccountIdAsync(int accountId)
        {
            return await _jewelryGoldDiaRepository.GetAuctionAndJewelryGoldDiamondByAccountId(accountId);
        }
    }
}
