﻿using DAL.DTO.BidDTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBidService
    {
        //Task<IEnumerable<Bid>> GetBidByAccountIdAsync(int accountId);
        Task<IEnumerable<Bid>> GetAllBids();
        //Task<IEnumerable<Bid>> GetBidRecordByAccountId(int accountId);
        Task<Bid> CreateBid(CreateBidDTO createBid);
        Task<Bid> UpdateBid(int id, UpdateBidDTO updateBid);
        Task<bool> PlaceBid(BiddingDTO bidDto);
    }
}
