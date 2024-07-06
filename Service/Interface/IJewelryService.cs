using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IJewelryService
    {
        (IEnumerable<JewelrySilver>, IEnumerable<JewelryGold>, IEnumerable<JewelryGoldDiamond>) GetAllJewelry();
        (IEnumerable<JewelrySilver>, IEnumerable<JewelryGold>, IEnumerable<JewelryGoldDiamond>) GetUnVerified();
        (IEnumerable<JewelrySilver>, IEnumerable<JewelryGold>, IEnumerable<JewelryGoldDiamond>) GetVerified();
    }
}
