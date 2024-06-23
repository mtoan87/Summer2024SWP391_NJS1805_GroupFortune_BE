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
            return _jewelryGoldRepository.GetAll();
        }
        public async Task<JewelryGold> GetJewelryById(int id)
        {
            return await _jewelryGoldRepository.GetById(id);
        }
        public async Task<JewelryGold> CreateJewelry(CreateJewelryGoldDTO createjew)
        {
            string imagePath = null;

            if (createjew.JewelryImg != null && createjew.JewelryImg.Length > 0)
            {
                // Define the path to save the image
                var uploads = Path.Combine("wwwroot", "assets");
                Directory.CreateDirectory(uploads); // Ensure the directory exists

                // Set the file name as the original file name
                var fileName = createjew.JewelryImg.FileName;
                imagePath = Path.Combine("assets", fileName); // Relative path to save in database

                // Save the file to the specified path
                var fullPath = Path.Combine(uploads, fileName);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await createjew.JewelryImg.CopyToAsync(fileStream);
                }
            }

            var newjewelry = new JewelryGold
            {
                AccountId = createjew.AccountId,
                JewelryImg = imagePath,
                Name = createjew.Name,
                Materials = createjew.Materials,
                Category = createjew.Category,
                Description = createjew.Description,
                Weight = createjew.Weight,
                GoldAge = createjew.GoldAge,
                Status = "UnVerified",
            };

            await _jewelryGoldRepository.AddAsync(newjewelry);
            await _jewelryGoldRepository.SaveChangesAsync();
            return newjewelry;
        }

        public async Task<JewelryGold> UpdateJewelryMember(int id, UpdateJewelryDTO updateJewelry)
        {
            var updjewelry = await _jewelryGoldRepository.GetByIdAsync(id);
            if (updjewelry == null)
            {
                throw new Exception($"Jewelry with ID {id} not found.");
            }
            // Keep the existing image if no new image is provided
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
            updjewelry.GoldAge = updateJewelry.Goldage;

      
            await _jewelryGoldRepository.UpdateJewelryAsync(updjewelry);
            return updjewelry;
        }
        public async Task<JewelryGold> UpdateJewelryStaff(int id, UpdateJewelryStaffDTO updateJewelry)
        {
            var updjewelry = await _jewelryGoldRepository.GetById(id);
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
            updjewelry.Weight = updateJewelry.Weight;
            updjewelry.GoldAge = updateJewelry.Goldage;
            updjewelry.Price = updateJewelry.Price;

            await _jewelryGoldRepository.UpdateJewelryAsync(updjewelry);
            return updjewelry;
        }

        public async Task<JewelryGold> UpdateJewelryManager(int id, UpdateJewelryManagerDTO updateJewelry)
        {
            var updjewelry = await _jewelryGoldRepository.GetById(id);
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
            updjewelry.Weight = updateJewelry.Weight;
            updjewelry.GoldAge = updateJewelry.Goldage;
            updjewelry.Price = updateJewelry.Price;
            updjewelry.Status = updateJewelry.Status;
            await _jewelryGoldRepository.UpdateJewelryAsync(updjewelry);
            return updjewelry;
        }
        public async Task<JewelryGold> DeleteJewelry(int id)
        {
            var account = await _jewelryGoldRepository.GetById(id);
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

