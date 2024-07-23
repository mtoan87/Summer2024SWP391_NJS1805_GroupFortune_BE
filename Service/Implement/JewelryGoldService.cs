using DAL.DTO.JewelryDTO.Gold;
using DAL.Enums;
using DAL.Models;
using Repository.Implement;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class JewelryGoldService : IJewelryGoldService
    {
        private readonly IJewelryGoldRepository _jewelryGoldRepository;
        public JewelryGoldService(IJewelryGoldRepository jewelryGoldRepository)
        {
            _jewelryGoldRepository = jewelryGoldRepository;
        }
        public async Task<IEnumerable<JewelryGold>> GetAllGoldJewelries()
        {
            return await _jewelryGoldRepository.GetAllAsync();
        }
        public async Task<JewelryGold> GetJewelryById(int id)
        {
            return await _jewelryGoldRepository.GetByIdAsync(id);
        }
        public async Task<JewelryGold> CreateJewelry(CreateJewelryGoldDTO createjew)
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
                Status = JewelryStatus.Unverified.ToString(),
                Shipment =JewelryShipment.Delivering.ToString(),
            };

            await _jewelryGoldRepository.AddAsync(newjewelry);
          
            return newjewelry;
        }

        public async Task<JewelryGold> UpdateJewelryMember(int id, UpdateJewelryDTO updateJewelry)
        {
            var updjewelry = await _jewelryGoldRepository.GetByIdAsync(id);
            if (updjewelry == null)
            {
                throw new Exception($"Jewelry with ID {id} not found.");
            }
            if (await _jewelryGoldRepository.JewelryGoldExistsInAuction(id))
            {
                throw new Exception("$Jewelry is register in auction can not update!");
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
            updjewelry.GoldAge = updateJewelry.Goldage;
            updjewelry.Price = 0;
            updjewelry.Status = JewelryStatus.Unverified.ToString();
            updjewelry.Shipment = JewelryShipment.Delivering.ToString();
            await _jewelryGoldRepository.UpdateAsync(updjewelry);
            return updjewelry;
        }
        public async Task<JewelryGold> UpdateJewelryStaff(int id, UpdateJewelryStaffDTO updateJewelry)
        {
            var updjewelry = await _jewelryGoldRepository.GetByIdAsync(id);
            if (updjewelry == null)
            {
                throw new Exception($"Jewelry with ID {id} not found.");
            }
            updjewelry.Shipment = updateJewelry.Shipment;
            updjewelry.Price = updateJewelry.Price;
            await _jewelryGoldRepository.UpdateAsync(updjewelry);
            return updjewelry;
        }

        public async Task<JewelryGold> UpdateJewelryManager(int id, UpdateJewelryManagerDTO updateJewelry)
        {
            var updjewelry = await _jewelryGoldRepository.GetByIdAsync(id);
            if (updjewelry == null)
            {
                throw new Exception($"Jewelry with ID {id} not found.");
            }           
            updjewelry.Price = updateJewelry.Price;
            updjewelry.Status = updateJewelry.Status;
            await _jewelryGoldRepository.UpdateAsync(updjewelry);
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

