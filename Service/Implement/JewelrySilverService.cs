using DAL.DTO.JewelryDTO.Silver;
using DAL.Models;
using Repository.Implement;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Interface;
using Service.Interface;

namespace Service.Implement
{
    public class JewelrySilverService : IJewelrySilverService
    {
        private readonly IJewelrySilverRepository _jewelrySilverRepository;
        public JewelrySilverService(IJewelrySilverRepository jewelrySilverRepository)
        {
            _jewelrySilverRepository = jewelrySilverRepository;
        }
        public async Task<IEnumerable<JewelrySilver>> GetAllSilverJewelries()
        {
            return await _jewelrySilverRepository.GetAllAsync();
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
                Status = JewelryStatus.Unverified.ToString(),
                Shipment = JewelryShipment.Delivering.ToString(),
            };

            await _jewelrySilverRepository.AddAsync(newjewelry);
            
            return newjewelry;
        }

        public async Task<JewelrySilver> UpdateJewelryMember(int id, UpdateJewelrySilverDTO updateJewelry)
        {
            var updjewelry = await _jewelrySilverRepository.GetByIdAsync(id);
            if (updjewelry == null)
            {
                throw new Exception($"Jewelry with ID {id} not found.");
            }
            if (await _jewelrySilverRepository.JewelrySilverExistsInAuction(id))
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
            updjewelry.Purity = updateJewelry.Purity;
            updjewelry.Price = 0;
            updjewelry.Status = JewelryStatus.Unverified.ToString();
            updjewelry.Shipment = JewelryShipment.Delivering.ToString();
            await _jewelrySilverRepository.UpdateAsync(updjewelry);
            return updjewelry;
        }

        public async Task<JewelrySilver> UpdateJewelryStaff(int id, UpdateJewelrySilverStaffDTO updateJewelry)
        {
            var updjewelry = await _jewelrySilverRepository.GetByIdAsync(id);
            if (updjewelry == null)
            {
                throw new Exception($"Jewelry with ID {id} not found.");
            }
            updjewelry.Shipment = updateJewelry.Shipment; 
            updjewelry.Price = updateJewelry.Price;
            await _jewelrySilverRepository.UpdateAsync(updjewelry);
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
            await _jewelrySilverRepository.UpdateAsync(updjewelry);
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

