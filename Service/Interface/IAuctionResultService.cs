﻿using DAL.DTO.AuctionResultDTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAuctionResultService
    {
        Task<IEnumerable<AuctionResult>> GetAllAuctionResults();
        Task<AuctionResult> GetAuctionResultById(int id);
        Task<AuctionResult> CreateAuctionRs(CreateAuctionRsDTO createAuctionRs);

        Task<AuctionResult> UpdateAuctionRs(int id, UpdateAuctionRsDTO updateAuctionRs);

        Task<AuctionResult> DeleteAuction(int id);
    }
}
