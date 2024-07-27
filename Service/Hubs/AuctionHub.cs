using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Hubs
{
    public class AuctionHub : Hub
    {
        public async Task SendRemainingTimeUpdate(object update)
        {
            await Clients.All.SendAsync("TimeRemaining", update);
        }
    }
}
