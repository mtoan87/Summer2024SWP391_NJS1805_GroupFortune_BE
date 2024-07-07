using Microsoft.AspNetCore.SignalR;
using Service.Implement;
using Service.Interface;

namespace jewelryauction.Hub
{
    public class BiddingHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly IBidService _bidRecordService;
        private readonly IHubContext<BiddingHub> _biddingHub;

        public BiddingHub(IBidService bidRecordService, IHubContext<BiddingHub> biddingHub)
        {
            _bidRecordService = bidRecordService;
            _biddingHub = biddingHub;
        }

        public async Task SendAllBids()
        {
            var bids = _bidRecordService.GetAllBids();
            await _biddingHub.Clients.All.SendAsync("ReceiveAllBids", bids);
        }
    }
}
