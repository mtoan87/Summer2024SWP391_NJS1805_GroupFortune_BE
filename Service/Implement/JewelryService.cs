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
        public JewelryService(JewelryGoldRepository jewelryGoldRepository, JewelrySilverRepository jewelrySilverRepository)
        {
            _jewelryGoldRepository = jewelryGoldRepository;
            _jewelrySilverRepository = jewelrySilverRepository;
        }

        public (IEnumerable<JewelrySilver>, IEnumerable<JewelryGold>) GetAllJewelry()
        {
            var silvers = _jewelrySilverRepository.GetAll();
            var golds = _jewelryGoldRepository.GetAll();
            return (silvers, golds);
        }
    }

}

