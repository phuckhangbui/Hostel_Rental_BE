using BusinessObject.Models;
using DTOs;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class MemberShipService : IMemberShipService
    {
        public IMemberShipRepository memberShipRepository;

        public MemberShipService(IMemberShipRepository memberShipRepository)
        {
            this.memberShipRepository = memberShipRepository;
        }
        public async Task CreateMemberShip(CreateMemberShipDto createMemberShipDto)
        {
            MemberShip memberShip = new MemberShip
            {
                MemberShipName = createMemberShipDto.MemberShipName,
                CapacityHostel = createMemberShipDto.CapacityHostel,
                Month = createMemberShipDto.Month,
                MemberShipFee = createMemberShipDto.MemberShipFee,
            };
            await memberShipRepository.CreateMemberShip(memberShip);
        }
    }
}
