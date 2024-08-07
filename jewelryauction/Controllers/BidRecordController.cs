﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implement;
using Service.Interface;

namespace jewelryauction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidRecordController : ControllerBase
    {
        private readonly IBidRecordService _bidRecordService;
        public BidRecordController(IBidRecordService bidRecordService)
        {
            _bidRecordService = bidRecordService;
        }

        [HttpGet]
        [Route("GetBidRecords")]
        public async Task<IActionResult> GetBidRecords()
        {
            var rs = await _bidRecordService.GetBidRecords();
            return Ok(rs);
        }
        [HttpGet]
        [Route("GetBidRecordByBidId")]
        public async Task<IActionResult> GetBidRecordByBidId(int bidId)
        {
            var rs = await _bidRecordService.GetBidRecordByBidId(bidId);
            return Ok(rs);
        }
        [HttpGet]
        [Route("GetBidRecordByAccountIdAndBidId")]
        public IActionResult GetBidRecordByAccountIdAndBidId(int accountId, int bidId)
        {
            var rs =  _bidRecordService.GetBidRecordByAccountAndBidId(accountId,bidId);
            return Ok(rs);
        }
        [HttpGet]
        [Route("GetBidRecordByAccountId")]
        public async Task<IActionResult> GetBidRecordByAccountId(int AccountId)
        {
            var rs = await _bidRecordService.GetBidRecordByAccountId(AccountId);
            return Ok(rs);
        }

        [HttpGet]
        [Route("GetBidRecordAndBidByAccountId")]
        public async Task<IActionResult> GetBidRecordAndBidAccountId(int AccountId)
        {
            var rs = await _bidRecordService.GetBidRecordAndBidByAccountId(AccountId);
            return Ok(rs);
        }
    }
}
