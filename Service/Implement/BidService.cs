using Repository.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class BidService
    {
        private readonly BidRepository _bidRepository;
        public BidService(BidRepository bidRepository)
        {
            _bidRepository = bidRepository;
        }
    }
}
