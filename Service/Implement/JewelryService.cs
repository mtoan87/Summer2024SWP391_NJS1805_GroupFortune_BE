using DAL.DTO.AccountDTO;
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
    public class JewelryService
    {
        private readonly JewelryRepository _jewelryrepository;
        public JewelryService(JewelryRepository jewelryRepository)
        {
            _jewelryrepository = jewelryRepository;
        }

        public IEnumerable<Jewelry> GetAllJewelries()
        {
            return _jewelryrepository.GetAllJewelries();
        }
        public async Task<Jewelry> GetJewelryById(int id)
        {
            return await _jewelryrepository.GetByIdAsync(id);
        }
        public async Task<Jewelry> CreateJewelry(CreateJewelryDTO createjew)
        {


            var newjewelry = new Jewelry
            {
                AccountId = createjew.AccountId,
                JewelryImg = createjew.JewelryImg,
                Name = createjew.Name,
                Materials = createjew.Materials,
                Description = createjew.Description,
                Weight = createjew.Weight,
                Goldage = createjew.Goldage,
                Collection = createjew.Collection,
                Price = createjew.Price,
            };
          
             await _jewelryrepository.AddAsync(newjewelry);
            
             await _jewelryrepository.SaveChangesAsync();
            return newjewelry;
        }
        public async Task<Jewelry> UpdateJewelry(int id, UpdateJewelryDTO updateJewelry)
        {
            var updjewelry = await _jewelryrepository.GetByIdAsync(id);
            if (updjewelry == null)
            {
                throw new Exception($"Jewelry with ID {id} not found.");
            }
            updjewelry.AccountId = updateJewelry.AccountId;
            updateJewelry.JewelryImg = updateJewelry.JewelryImg;
            updjewelry.Name = updateJewelry.Name;
            updjewelry.Materials = updateJewelry.Materials;
            updjewelry.Description = updateJewelry.Description;
            updjewelry.Weight = updateJewelry.Weight;
            updjewelry.Goldage = updateJewelry.Goldage;
            updjewelry.Collection = updateJewelry.Collection;
            updjewelry.Price = updateJewelry.Price;
            await _jewelryrepository.UpdateJewelryAsync(updjewelry);
            return updjewelry;
        }
        public async Task<Jewelry> DeleteJewelry(int id)
        {
            var account = await _jewelryrepository.GetByIdAsync(id);
            if (account == null)
            {
                throw new Exception($"Account with ID {id} not found.");
            }
            await _jewelryrepository.RemoveAsync(account);
            return account;
        }
        public async Task<IEnumerable<Jewelry>> GetAuctionAndJewelryByAccountIdAsync(int accountId)
        {
            return await _jewelryrepository.GetAuctionAndJewelryByAccountId(accountId);
        }
    }

    }

