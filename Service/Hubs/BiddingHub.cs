using Microsoft.AspNetCore.SignalR;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service.Hubs
{
    public class BiddingHub : Hub
    {
        private readonly IBidService _bidRecordService;
        private readonly IHubContext<BiddingHub> _biddingHub;

        public BiddingHub(IBidService bidRecordService, IHubContext<BiddingHub> biddingHub)
        {
            _bidRecordService = bidRecordService;
            _biddingHub = biddingHub;
        }

        public async Task GetHighestPrice()
        {
            var bids = await _bidRecordService.GetAllBids();

            await _biddingHub.Clients.All.SendAsync("HighestPrice", bids);
        }
        public async Task GetBidStep()
        {
            var bids = await _bidRecordService.GetAllBids();

            await _biddingHub.Clients.All.SendAsync("BidStep", bids);
        }
        //public Task JoinRoom(string roomName)
        //{
        //    return Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        //}
        public async Task JoinRoom(string roomName, int auctionId)
        {
            var isAuctionActive = await _bidRecordService.IsAuctionActive(auctionId);

            if (isAuctionActive)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            }
            else
            {
                await Clients.Caller.SendAsync("AuctionInactive", "The auction is no longer active.");
            }
        }

        public async Task CheckAuctionStatus(int auctionId)
        {
            var isAuctionActive = await _bidRecordService.IsAuctionActive(auctionId);

            if (!isAuctionActive)
            {
                await Clients.Group(auctionId.ToString()).SendAsync("AuctionInactive", "The auction is no longer active.");
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, auctionId.ToString());
            }
        }
    }
}
