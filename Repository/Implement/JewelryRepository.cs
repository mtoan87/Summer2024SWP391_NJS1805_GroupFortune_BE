//using DAL.Models;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Repository.Implement
//{
//    public class JewelryRepository : RepositoryGeneric<Jewelry>
//    {
//        public JewelryRepository(JewelryAuctionContext context) : base(context)
//        {

//        }

//        public IEnumerable<Jewelry> GetAllJewelries()
//        {
//            return _context.Jewelries.ToList();
//        }
//        public async Task<bool> UpdateJewelryAsync(Jewelry jewelry)
//        {
//            try
//            {
//                _context.Jewelries.Update(jewelry);
//                await _context.SaveChangesAsync();
//                return true;
//            }
//            catch
//            {
//                return false;
//            }
//        }
        

//    }
//}
