using BusinessObject.Enum;
using BusinessObject.Models;
using DTOs.Membership;
using Repository.Interface;
using Service.Interface;

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
                Status = 0
            };
            await memberShipRepository.CreateMemberShip(memberShip);
        }

        public async Task<IEnumerable<GetMemberShipDto>> GetMembershipsActive()
        {
            return await memberShipRepository.GetMembershipsActive();
        }

        public async Task<IEnumerable<GetMemberShipDto>> GetMembershipsExpire()
        {
            return await memberShipRepository.GetMembershipExpire();
        }

        public Task<IEnumerable<GetMemberShipDto>> GetAllMemberships()
        {
            return memberShipRepository.GetAllMemberships();
        }

        public async Task<bool> DeactivateMembership(UpdateMembershipDto updateMembershipDto)
        {
            var memberShip = memberShipRepository.GetMembershipById(updateMembershipDto.MemberShipID);
            if (memberShip == null)
            {
                return false;
            }

            memberShip.Status = (int)MemberShipEnum.Expire;
            return await memberShipRepository.UpdateMembershipStatus(memberShip);
        }

        public async Task<bool> ActivateMembership(UpdateMembershipDto updateMembershipDto)
        {
            var memberShip = memberShipRepository.GetMembershipById(updateMembershipDto.MemberShipID);
            if (memberShip == null)
            {
                return false;
            }
            else
            {
                memberShip.Status = (int)MemberShipEnum.Active;
                return await memberShipRepository.UpdateMembershipStatus(memberShip);
            }
        }
    }
}