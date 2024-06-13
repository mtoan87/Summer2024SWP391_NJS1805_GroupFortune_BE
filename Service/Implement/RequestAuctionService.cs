//using DAL.DTO.AuctionDTO;
//using DAL.DTO.JewelryDTO;
//using DAL.Enums;
//using DAL.Models;
//using Microsoft.EntityFrameworkCore;
//using Repository.Implement;
//using Repository.Interface;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Service.Implement
//{
//    public class RequestAuctionService
//    {
       
//        private readonly RequestAuctionRepository _repo;
//        private readonly JewelryRepository _jewelryRepository;
//        private readonly AuctionRepository _auctionRepository;

//        public RequestAuctionService(RequestAuctionRepository repo, JewelryRepository jewelryRepository, AuctionRepository auctionRepository)
//        {
//            _repo = repo;
//            _jewelryRepository = jewelryRepository;
//            _auctionRepository = auctionRepository;

//        }
//        public async Task<Auction> CreateAuctionAsync(RequestAuctionDTO requestAuction)
//        {
           
                
                    
//                    var newJewelry = new Jewelry
//                    {
//                        AccountId = requestAuction.AccountId,
//                        Name = requestAuction.Name,
//                        Materials = requestAuction.Materials,
//                        Description = requestAuction.Description,
//                        Weight = requestAuction.Weight,
//                        Goldage = requestAuction.Goldage,
//                        Collection = requestAuction.Collection
//                    };

//                    await _jewelryRepository.AddAsync(newJewelry);
//                    await _jewelryRepository.SaveChangesAsync();

                   
//                    var newAuction = new Auction
//                    {
//                        AccountId = requestAuction.AccountId,
//                        JewelryId = newJewelry.JewelryId,
//                        Starttime = requestAuction.Starttime,
//                        Endtime = requestAuction.Endtime,
//                        Status = "UnActive"
//                    };

//                    await _auctionRepository.AddAsync(newAuction);
//                    await _auctionRepository.SaveChangesAsync();

                    

//                    return newAuction;
//                }
                
//            }
//        }

    

