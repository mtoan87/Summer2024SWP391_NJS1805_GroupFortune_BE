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
            string imagePath = null;

            if (createjew.JewelryImg != null && createjew.JewelryImg.Length > 0)
            {
                var uploads = Path.Combine("wwwroot", "assets");
                Directory.CreateDirectory(uploads);                 
                var fileName = createjew.JewelryImg.FileName;
                imagePath = Path.Combine("assets", fileName); 
                var fullPath = Path.Combine(uploads, fileName);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await createjew.JewelryImg.CopyToAsync(fileStream);
                }
            }
            var newjewelry = new JewelrySilver
            {
                AccountId = createjew.AccountId,
                JewelryImg = imagePath, 
                Name = createjew.Name,
                Materials = createjew.Materials,
                Category = createjew.Category,
                Description = createjew.Description,
                Weight = createjew.Weight,
                Purity = createjew.Purity,
                Status = "UnVerified",
            };

            await _jewelrySilverRepository.AddAsync(newjewelry);
            await _jewelrySilverRepository.SaveChangesAsync();
            return newjewelry;
        }

        public async Task<JewelrySilver> UpdateJewelryMember(int id, UpdateJewelrySilverDTO updateJewelry)
        {
            var updjewelry = await _jewelrySilverRepository.GetByIdAsync(id);
            if (updjewelry == null)
            {
                throw new Exception($"Jewelry with ID {id} not found.");
            }          
            if (updateJewelry.JewelryImg == null)
            {
                updateJewelry.JewelryImg = updjewelry.JewelryImg;
            }         
            updjewelry.AccountId = updateJewelry.AccountId;
            updjewelry.JewelryImg = updateJewelry.JewelryImg;
            updjewelry.Name = updateJewelry.Name;
            updjewelry.Materials = updateJewelry.Materials;
            updjewelry.Description = updateJewelry.Description;
            updjewelry.Category = updateJewelry.Category;
            updjewelry.Weight = updateJewelry.Weight;
            updjewelry.Purity = updateJewelry.Purity;

            await _jewelrySilverRepository.UpdateJewelryAsync(updjewelry);
            return updjewelry;
        }

        public async Task<JewelrySilver> UpdateJewelryStaff(int id, UpdateJewelrySilverStaffDTO updateJewelry)
        {
            var updjewelry = await _jewelrySilverRepository.GetByIdAsync(id);
            if (updjewelry == null)
            {
                throw new Exception($"Jewelry with ID {id} not found.");
            }
            
            updjewelry.Price = updateJewelry.Price;
            await _jewelrySilverRepository.UpdateJewelryAsync(updjewelry);
            return updjewelry;
        }
        public async Task<JewelrySilver> UpdateJewelryManager(int id, UpdateJewelrySilverManagerDTO updateJewelry)
        {
            var updjewelry = await _jewelrySilverRepository.GetByIdAsync(id);
            if (updjewelry == null)
            {
                throw new Exception($"Jewelry with ID {id} not found.");
            }
            
            updjewelry.Price = updateJewelry.Price;
            updjewelry.Status = updateJewelry.Status;
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

