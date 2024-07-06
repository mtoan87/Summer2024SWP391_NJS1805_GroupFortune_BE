using DAL.DTO.AuctionDTO;
using DAL.Enums;
using DAL.Models;
using Repository.Implement;
using Repository.Interface;


namespace Service.Implement
{
    public class AuctionService
    {
        private readonly AuctionRepository _auctionRepository;
        private readonly IJewelryGoldRepository _jewelryGoldRepository;
        private readonly JewelryGoldDiaRepository _jewelryDiaRepository;
        private readonly IJewelrySilverRepository _jewelrySilverRepository;
        public AuctionService(AuctionRepository auctionRepository, IJewelryGoldRepository jewelryGoldRepository, JewelryGoldDiaRepository jewelryGoldDiaRepository, IJewelrySilverRepository jewelrySilverRepository)
        {
            _auctionRepository = auctionRepository;
            _jewelryDiaRepository = jewelryGoldDiaRepository;
            _jewelryGoldRepository = jewelryGoldRepository;
            _jewelrySilverRepository = jewelrySilverRepository;
        }

        public async Task<IEnumerable<Auction>> GetAllAuctions()
        {
            return await _auctionRepository.GetAllAsync();
        }
        public IEnumerable<Auction> GetAllActiveAuctions()
        {
            return _auctionRepository.GetActiveAuctions();
        }
        public IEnumerable<Auction> GetAllUnActiveAuctions()
        {
            return _auctionRepository.GetUnActiveAuctions();
        }
        public async Task<Auction> GetAuctionById(int id)
        {
            return await _auctionRepository.GetByIdAsync(id);
        }
        public async Task<Auction> CreateJewelrySilverAuction(CreateSilverAuctionDTO createAuction)
        {
            bool isJewelryInAnyAuction = _auctionRepository.IsJewelryInAuctionSilver(createAuction.JewelrySilverId);        
            if (isJewelryInAnyAuction)
            {
                throw new Exception("The jewelry item is already in an auction.");
            }
            var newAuction = new Auction
                {
                    AccountId = createAuction.AccountId,
                    JewelrySilverId = createAuction.JewelrySilverId,
                    Starttime = createAuction.Starttime,
                    Endtime = createAuction.Endtime,
                    Status = AuctionStatus.UnActive.ToString(),
                };
                await _auctionRepository.AddAsync(newAuction);
                return newAuction;            
        }
        public async Task<Auction> CreateJewelryGoldAuction(CreateGoldAuctionDTO createAuction)
        {
            bool isJewelryInAnyAuction = _auctionRepository.IsJewelryInAuctionGold(createAuction.JewelryGoldId);
            if (isJewelryInAnyAuction)
            {
                throw new Exception("The jewelry item is already in an auction.");
            }
            var newAuction = new Auction
                {
                    AccountId = createAuction.AccountId,
                    JewelrySilverId = createAuction.JewelryGoldId,
                    Starttime = createAuction.Starttime,
                    Endtime = createAuction.Endtime,
                    Status = AuctionStatus.UnActive.ToString(),
                };
                await _auctionRepository.AddAsync(newAuction);

                return newAuction;
            
        }
        public async Task<Auction> CreateJewelryGoldDiamondAuction(CreateGoldDiamondAuctionDTO createAuction)
        {
            bool isJewelryInAnyAuction = _auctionRepository.IsJewelryInAuctionGoldDiamond(createAuction.JewelryGolddiaId);      
            if (isJewelryInAnyAuction)
            {
                throw new Exception("The jewelry item is already in an auction.");
            }
            var newAuction = new Auction
                {
                    AccountId = createAuction.AccountId,
                    JewelrySilverId = createAuction.JewelryGolddiaId,
                    Starttime = createAuction.Starttime,
                    Endtime = createAuction.Endtime,
                    Status = AuctionStatus.UnActive.ToString(),
                };
                await _auctionRepository.AddAsync(newAuction);
                return newAuction;
            
        }
        public async Task<Auction> UpdateSilverAuction(int id, UpdateSilverAuctionDTO updateAuction)
        {
            var auction = await _auctionRepository.GetByIdAsync(id);
            if (auction == null)
            {
                throw new Exception($"Auction with ID {id} not found.");
            }
            
            auction.JewelrySilverId = updateAuction.JewelrySilverId;
         
            auction.Starttime = updateAuction.Starttime;
            auction.Endtime = updateAuction.Endtime;
            auction.Status = updateAuction.Status;
            await _auctionRepository.UpdateAsync(auction);
            return auction;
        }
        public async Task<Auction> UpdateGoldAuction(int id, UpdateGoldAuctionDTO updateAuction)
        {
            var auction = await _auctionRepository.GetByIdAsync(id);
            if (auction == null)
            {
                throw new Exception($"Auction with ID {id} not found.");
            }

            auction.JewelryGoldId = updateAuction.JewelryGoldId;

            auction.Starttime = updateAuction.Starttime;
            auction.Endtime = updateAuction.Endtime;
            auction.Status = updateAuction.Status;
            await _auctionRepository.UpdateAsync(auction);
            return auction;
        }
        public async Task<Auction> UpdateGoldDiamondAuction(int id, UpdateGoldDiamondAuctionDTO updateAuction)
        {
            var auction = await _auctionRepository.GetByIdAsync(id);
            if (auction == null)
            {
                throw new Exception($"Auction with ID {id} not found.");
            }

            auction.JewelryGolddiaId = updateAuction.JewelryGolddiaId;

            auction.Starttime = updateAuction.Starttime;
            auction.Endtime = updateAuction.Endtime;
            auction.Status = updateAuction.Status;
            await _auctionRepository.UpdateAsync(auction);
            return auction;
        }
        public async Task<Auction> DeleteAuction(int id)
        {
            var auction = await _auctionRepository.GetByIdAsync(id);
            if (auction == null)
            {
                throw new Exception($"Auction with ID {id} not found.");
            }
            await _auctionRepository.RemoveAsync(auction);
            return auction;
        }

        public async Task<IEnumerable<Auction>> GetAuctionAndJewelryGoldByAccountIdAsync(int accountId)
        {
            return await _auctionRepository.GetAuctionAndJewelryGoldByAccountId(accountId);
        }
        public async Task<IEnumerable<Auction>> GetAuctionAndJewelryGoldDiamondByAccountIdAsync(int accountId)
        {
            return await _auctionRepository.GetAuctionAndJewelryGoldDiamondByAccountId(accountId);
        }
        public async Task<IEnumerable<Auction>> GetAuctionAndJewelrySilverByAccountIdAsync(int accountId)
        {
            return await _auctionRepository.GetAuctionAndJewelrySilverByAccountId(accountId);
        }
        public int GetAccountCountInAuction(int auctionId)
        {
            return _auctionRepository.GetAccountCountInAuction(auctionId);
        }
        public IEnumerable<Auction> GetJewelryActiveAuctions()
        {
            return  _auctionRepository.GetJewelryActiveAuctions();
        }
    }
}
