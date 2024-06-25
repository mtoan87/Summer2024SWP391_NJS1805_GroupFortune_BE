﻿using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class PaymentRepository : RepositoryGeneric<Payment>
    {
        public PaymentRepository(JewelryAuctionContext context) : base(context)
        {
            
        }
    }
}
