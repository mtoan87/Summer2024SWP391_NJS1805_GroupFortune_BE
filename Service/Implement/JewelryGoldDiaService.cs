using DAL.DTO.JewelryDTO.GoldDiamond;
using DAL.Models;
using DAL.Enums;
using Repository.Implement;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Interface;

namespace Service.Implement
{
    public class JewelryGoldDiaService : IJewelryGoldDiamondService
    {
        private readonly IJewelryGoldDiamondRepository _jewelryGoldDiaRepository;
        public JewelryGoldDiaService(IJewelryGoldDiamondRepository jewelryGoldDiaRepository)
        {
            _jewelryGoldDiaRepository = jewelryGoldDiaRepository;
        }
        public async Task<IEnumerable<JewelryGoldDiamond>> GetAllGoldDiaJewelries()
        {
            return await _jewelryGoldDiaRepository.GetAllAsync();
        }
        public async Task<JewelryGoldDiamond> GetJewelryById(int id)
        {
            return await _jewelryGoldDiaRepository.GetByIdAsync(id);
        }
        public async Task<JewelryGoldDiamond> CreateJewelry(CreateJewelryGoldDiamondDTO createjew)
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

            var newjewelry = new JewelryGoldDiamond
            {
                AccountId = createjew.AccountId,
                JewelryImg = imagePath,
                Name = createjew.Name,
                Materials = createjew.Materials,
                Category = createjew.Category,
                Description = createjew.Description,
                Weight = createjew.Weight,
                GoldAge = createjew.GoldAge,
                Clarity = createjew.Clarity,
                Carat  = createjew.Carat,
                Status = JewelryStatus.Unverified.ToString(),
                Shipment=JewelryShipment.Delivering.ToString()
            };

            await _jewelryGoldDiaRepository.AddAsync(newjewelry);
            
            return newjewelry;
        }
        public async Task<JewelryGoldDiamond> UpdateJewelryMember(int id, UpdateJewelryGoldDiaDTO updateJewelry)
        {
            var updjewelry = await _jewelryGoldDiaRepository.GetByIdAsync(id);
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
            updjewelry.Clarity = updjewelry.Clarity; 
            updjewelry.Carat = updateJewelry.Carat;
            updjewelry.Weight = updateJewelry.Weight;
            updjewelry.GoldAge = updateJewelry.GoldAge;
            await _jewelryGoldDiaRepository.UpdateAsync(updjewelry);
            return updjewelry;
        }
        public async Task<JewelryGoldDiamond> UpdateJewelryStaff(int id, UpdateJewelryGoldDiamondStaffDTO updateJewelry)
        {
            var updjewelry = await _jewelryGoldDiaRepository.GetByIdAsync(id);
            if (updjewelry == null)
            {
                throw new Exception($"Jewelry with ID {id} not found.");
            }

            updjewelry.Shipment = updateJewelry.Shipment;
            updjewelry.Price = updateJewelry.Price;
            await _jewelryGoldDiaRepository.UpdateAsync(updjewelry);
            return updjewelry;
        }

        public async Task<JewelryGoldDiamond> UpdateJewelryManager(int id, UpdateJewelryGoldDiamondManagerDTO updateJewelry)
        {
            var updjewelry = await _jewelryGoldDiaRepository.GetByIdAsync(id);
            if (updjewelry == null)
            {
                throw new Exception($"Jewelry with ID {id} not found.");
            }

            
            updjewelry.Price = updateJewelry.Price;
            updjewelry.Status = updateJewelry.Status;
            await _jewelryGoldDiaRepository.UpdateAsync(updjewelry);
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
