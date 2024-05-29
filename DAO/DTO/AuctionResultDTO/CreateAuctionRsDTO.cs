using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.AuctionResultDTO
{
    public class CreateAuctionRsDTO
    {
        public int? JoinauctionId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = null!;
        public double Price { get; set; }
        public int? AccountId { get; set; }

    }
}
