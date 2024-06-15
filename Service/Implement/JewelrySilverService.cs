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
    public class JewelrySilverService
    {
        private readonly JewelrySilverRepository _jewelrySilverRepository;
        public JewelrySilverService(JewelrySilverRepository jewelrySilverRepository)
        {
            _jewelrySilverRepository = jewelrySilverRepository;
        }
        public IEnumerable<JewelrySilver> GetAllSilverJewelries()
        {
            return _jewelrySilverRepository.GetAllJewelries();
        }
        public async Task<JewelrySilver> GetJewelryById(int id)
        {
            return await _jewelrySilverRepository.GetByIdAsync(id);
        }
        public async Task<JewelrySilver> CreateJewelry(CreateJewelrySilverDTO createjew)
        {


            var newjewelry = new JewelrySilver
            {
                AccountId = createjew.AccountId,
                JewelryImg = createjew.JewelryImg,
                Name = createjew.Name,
                Materials = createjew.Materials,
                Category = createjew.Category,
                Description = createjew.Description,
                Weight = createjew.Weight,
                Purity = createjew.Purity,
                Price = createjew.Price,
            };
            await _jewelrySilverRepository.AddAsync(newjewelry);

            await _jewelrySilverRepository.SaveChangesAsync();
            return newjewelry;
        }
        public async Task<JewelrySilver> UpdateJewelry(int id, UpdateJewelrySilverDTO updateJewelry)
        {
            var updjewelry = await _jewelrySilverRepository.GetByIdAsync(id);
            if (updjewelry == null)
            {
                throw new Exception($"Jewelry with ID {id} not found.");
            }
            updjewelry.AccountId = updateJewelry.AccountId;
            updjewelry.JewelryImg = updateJewelry.JewelryImg; // Update only if new image is provided
            
            updjewelry.Name = updateJewelry.Name;
            updjewelry.Materials = updateJewelry.Materials;
            updjewelry.Description = updateJewelry.Description;
            updjewelry.Category = updateJewelry.Category;
            updjewelry.Weight = updateJewelry.Weight;
            updjewelry.Purity = updateJewelry.Purity;
            updjewelry.Price = updateJewelry.Price;
            await _jewelrySilverRepository.UpdateJewelryAsync(updjewelry);
            return updjewelry;
        }
        public async Task<JewelrySilver> DeleteJewelry(int id)
        {
            var account = await _jewelrySilverRepository.GetByIdAsync(id);
            if (account == null)
            {
                throw new Exception($"Account with ID {id} not found.");
            }
            await _jewelrySilverRepository.RemoveAsync(account);
            return account;
        }
        public async Task<IEnumerable<JewelrySilver>> GetAuctionAndJewelrySilverByAccountIdAsync(int accountId)
        {
            return await _jewelrySilverRepository.GetAuctionAndJewelrySilverByAccountId(accountId);
        }
    }
}

