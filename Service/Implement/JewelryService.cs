using DAL.DTO.AccountDTO;
using DAL.DTO.JewelryDTO;
using DAL.Models;
using Repository.Implement;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class JewelryService
    {
        private readonly JewelryGoldRepository _jewelryGoldRepository;
        private readonly JewelrySilverRepository _jewelrySilverRepository;
        private readonly JewelryGoldDiaRepository _jewelryGoldDiaRepository;
        public JewelryService(JewelryGoldRepository jewelryGoldRepository, JewelrySilverRepository jewelrySilverRepository, JewelryGoldDiaRepository jewelryGoldDiaRepository)
        {
            _jewelryGoldRepository = jewelryGoldRepository;
            _jewelrySilverRepository = jewelrySilverRepository;
            _jewelryGoldDiaRepository = jewelryGoldDiaRepository;   
        }

        public (IEnumerable<JewelrySilver>, IEnumerable<JewelryGold>, IEnumerable<JewelryGoldDiamond>) GetAllJewelry()
        {
            var silvers = _jewelrySilverRepository.GetAll();
            var golds = _jewelryGoldRepository.GetAll();
            var golddias = _jewelryGoldDiaRepository.GetAll();
            return (silvers, golds, golddias);
        }
    }

}

