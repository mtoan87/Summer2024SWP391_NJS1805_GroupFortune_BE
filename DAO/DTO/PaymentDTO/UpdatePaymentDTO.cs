using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.PaymentDTO
{
    public class UpdatePaymentDTO
    {
        public int? AccountId { get; set; }
        public int? AuctionResultId { get; set; }
        public string Status { get; set; } = null!;
        public string Paymentmethod { get; set; } = null!;
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public double Totalprice { get; set; }
        public double Fee { get; set; }
    }
}
