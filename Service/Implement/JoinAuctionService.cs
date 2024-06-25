﻿using DAL.DTO.AuctionDTO;
using DAL.DTO.JewelryDTO;
using DAL.DTO.JoinAuctionDTO;
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
    public class JoinAuctionService
    {
        private readonly JoinAuctionRepository _joinAuctionRepository;
        private readonly AuctionRepository _auctionRepository;
        private readonly JewelryGoldRepository _jewelryGoldRepository;
        private readonly JewelrySilverRepository _jewelrySilverRepository;
        private readonly JewelryGoldDiaRepository _jewelryGoldDiaRepository;
        private readonly AccountWalletRepository _accountWalletRepository;

        public JoinAuctionService(
            JoinAuctionRepository joinAuctionRepository,
            AuctionRepository auctionRepository,
            JewelryGoldRepository jewelryGoldRepository,
            JewelrySilverRepository jewelrySilverRepository,
            JewelryGoldDiaRepository jewelryGoldDiaRepository,
            AccountWalletRepository accountWalletRepository)
        {
            _joinAuctionRepository = joinAuctionRepository;
            _auctionRepository = auctionRepository;
            _jewelryGoldRepository = jewelryGoldRepository;
            _jewelrySilverRepository = jewelrySilverRepository;
            _jewelryGoldDiaRepository = jewelryGoldDiaRepository;
            _accountWalletRepository = accountWalletRepository;
        }

        public async Task<IEnumerable<JoinAuction>> GetAllJoinAuctions()
        {
            return await _joinAuctionRepository.GetAllAsync();
        }

        public async Task<JoinAuction> GetJoinAuctionById(int id)
        {
            return await _joinAuctionRepository.GetByIdAsync(id);
        }

        public async Task<JoinAuction> CreateJoinAuction(CreateJoinAuctionDTO createJoinAuction)
        {
            var newJoinAuction = new JoinAuction
            {
                AccountId = createJoinAuction.AccountId,
                BidId = createJoinAuction.BidId,
                AuctionId = createJoinAuction.AuctionId,
                Joindate = DateTime.Now,
            };

            if (await CanJoinAuction(newJoinAuction.AccountId.Value, newJoinAuction.AuctionId.Value))
            {
                await _joinAuctionRepository.AddAsync(newJoinAuction);
                return newJoinAuction;
            }
            else
            {
                throw new Exception("Insufficient budget to join the auction.");
            }
        }

        public async Task<bool> CanJoinAuction(int accountId, int auctionId)
        {
            var accountWallet = await _accountWalletRepository.GetByIdAsync(accountId);
            if (accountWallet == null)
            {
                throw new Exception("Account wallet not found.");
            }

            var auction = await _auctionRepository.GetByIdAsync(auctionId);
            if (auction == null)
            {
                throw new Exception("Auction not found.");
            }

            double? jewelryPrice = null;

            if (auction.JewelryGoldId != null)
            {
                var jewelryGold = await _jewelryGoldRepository.GetByIdAsync(auction.JewelryGoldId.Value);
                jewelryPrice = jewelryGold?.Price;
            }
            else if (auction.JewelryGolddiaId != null)
            {
                var jewelryGoldDiamond = await _jewelryGoldDiaRepository.GetByIdAsync(auction.JewelryGolddiaId.Value);
                jewelryPrice = jewelryGoldDiamond?.Price;
            }
            else if (auction.JewelrySilverId != null)
            {
                var jewelrySilver = await _jewelrySilverRepository.GetByIdAsync(auction.JewelrySilverId.Value);
                jewelryPrice = jewelrySilver?.Price;
            }
            else
            {
                throw new Exception("Invalid auction configuration: No associated jewelry.");
            }

            if (jewelryPrice == null)
            {
                throw new Exception("Jewelry not found or invalid price.");
            }

            return accountWallet.Budget > jewelryPrice;
        }

        public async Task<JoinAuction> UpdateJoinAuction(int id, UpdateJoinAuctionDTO updateJoinAuction)
        {
            var auction = await _joinAuctionRepository.GetByIdAsync(id);
            if (auction == null)
            {
                throw new Exception($"JoinAuction with ID {id} not found.");
            }

            auction.AuctionId = updateJoinAuction.AuctionId;
            auction.AccountId = updateJoinAuction.AccountId;
            auction.BidId = updateJoinAuction.BidId;
            await _joinAuctionRepository.UpdateAsync(auction);
            return auction;
        }

        public async Task<JoinAuction> DeleteJoinAuction(int id)
        {
            var auction = await _joinAuctionRepository.GetByIdAsync(id);
            if (auction == null)
            {
                throw new Exception($"JoinAuction with ID {id} not found.");
            }
            await _joinAuctionRepository.RemoveAsync(auction);
            return auction;
        }
    }
}

