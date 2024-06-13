using DAL.DTO.JewelryDTO;
using DAL.Models;
using Repository.Implement;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class JewelryGoldService
    {
        private readonly JewelryGoldRepository _jewelryGoldRepository;
        public JewelryGoldService(JewelryGoldRepository jewelryGoldRepository)
        {
            _jewelryGoldRepository = jewelryGoldRepository;
        }
        public IEnumerable<JewelryGold> GetAllGoldJewelries()
        {
            return _jewelryGoldRepository.GetAllJewelries();
        }
        public async Task<JewelryGold> GetJewelryById(int id)
        {
            return await _jewelryGoldRepository.GetByIdAsync(id);
        }
        public async Task<JewelryGold> CreateJewelry(CreateJewelryGoldDTO createjew)
        {


            var newjewelry = new JewelryGold
            {
                AccountId = createjew.AccountId,
                JewelryImg = createjew.JewelryImg,
                Name = createjew.Name,
                Materials = createjew.Materials,
                Category = createjew.Category,  
                Description = createjew.Description,
                Weight = createjew.Weight,
                GoldAge = createjew.GoldAge,
                Price = createjew.Price,
            };
            await _jewelryGoldRepository.AddAsync(newjewelry);

            await _jewelryGoldRepository.SaveChangesAsync();
            return newjewelry;
        }
        public async Task<JewelryGold> UpdateJewelry(int id, UpdateJewelryDTO updateJewelry)
        {
            var updjewelry = await _jewelryGoldRepository.GetByIdAsync(id);
            if (updjewelry == null)
            {
                throw new Exception($"Jewelry with ID {id} not found.");
            }

            updjewelry.AccountId = updateJewelry.AccountId;
            updjewelry.JewelryImg = updateJewelry.JewelryImg ?? updjewelry.JewelryImg; // Update only if new image is provided
            updjewelry.Name = updateJewelry.Name;
            updjewelry.Materials = updateJewelry.Materials;
            updjewelry.Description = updateJewelry.Description;
            updjewelry.Category = updateJewelry.Category;
            updjewelry.Weight = updateJewelry.Weight;
            updjewelry.GoldAge = updateJewelry.Goldage;
            updjewelry.Price = updateJewelry.Price;

            await _jewelryGoldRepository.UpdateJewelryAsync(updjewelry);
            return updjewelry;
        }
        public async Task<JewelryGold> DeleteJewelry(int id)
        {
            var account = await _jewelryGoldRepository.GetByIdAsync(id);
            if (account == null)
            {
                throw new Exception($"Account with ID {id} not found.");
            }
            await _jewelryGoldRepository.RemoveAsync(account);
            return account;
        }
        public async Task<IEnumerable<JewelryGold>> GetAuctionAndJewelryGoldByAccountIdAsync(int accountId)
        {
            return await _jewelryGoldRepository.GetAuctionAndJewelryGoldByAccountId(accountId);
        }
    }
}

