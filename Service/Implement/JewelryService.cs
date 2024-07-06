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
        private readonly IJewelryGoldRepository _jewelryGoldRepository;
        private readonly IJewelrySilverRepository _jewelrySilverRepository;
        private readonly IJewelryGoldDiamondRepository _jewelryGoldDiaRepository;
        public JewelryService(IJewelryGoldRepository jewelryGoldRepository, IJewelrySilverRepository jewelrySilverRepository, IJewelryGoldDiamondRepository jewelryGoldDiaRepository)
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
        public (IEnumerable<JewelrySilver>, IEnumerable<JewelryGold>, IEnumerable<JewelryGoldDiamond>) GetUnVerified()
        {
            var silvers = _jewelrySilverRepository.GetUnVerified();
            var golds = _jewelryGoldRepository.GetUnVerified();
            var golddias = _jewelryGoldDiaRepository.GetUnVerified();
            return (silvers, golds, golddias);
        }
        public (IEnumerable<JewelrySilver>, IEnumerable<JewelryGold>, IEnumerable<JewelryGoldDiamond>) GetVerified()
        {
            var silvers = _jewelrySilverRepository.GetVerified();
            var golds = _jewelryGoldRepository.GetVerified();
            var golddias = _jewelryGoldDiaRepository.GetVerified();
            return (silvers, golds, golddias);
        }

    }

}

