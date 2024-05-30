using AutoMapper;
using BusinessObject.Enum;
using DTOs.Membership;
using Repository.Interface;
using Service.Interface;

namespace Service.Implement
{
    public class MemberShipService : IMemberShipService
    {
        public IMemberShipRepository memberShipRepository;
        private readonly IMapper _mapper;

        public MemberShipService(IMemberShipRepository memberShipRepository, IMapper mapper)
        {
            this.memberShipRepository = memberShipRepository;
            _mapper = mapper;
        }

        public async Task CreateMemberShip(CreateMemberShipDto createMemberShipDto)
        {
            await memberShipRepository.CreateMemberShip(createMemberShipDto);
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
            var memberShip = await memberShipRepository.GetMembershipById(updateMembershipDto.MemberShipID);
            if (memberShip == null)
            {
                return false;
            }

            memberShip.Status = (int)MemberShipEnum.Expire;
            return await memberShipRepository.UpdateMembershipStatus(memberShip);
        }

        public async Task<bool> ActivateMembership(UpdateMembershipDto updateMembershipDto)
        {
            var memberShip = await memberShipRepository.GetMembershipById(updateMembershipDto.MemberShipID);
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

        public async Task<GetMemberShipDto> GetDetailMemberShip(int packageID)
        {
            var package = await memberShipRepository.GetMembershipById(packageID);
            return _mapper.Map<GetMemberShipDto>(package);
        }

        public async Task UpdateMemberShip(UpdateMemberShipAdminDto updateMemberShipAdmin)
        {
            var memberShip = await memberShipRepository.GetMembershipById(updateMemberShipAdmin.MemberShipID);
            if (memberShip == null)
            {
                
            }
            memberShip.MemberShipFee = updateMemberShipAdmin.MemberShipFee;
            memberShip.MemberShipName = updateMemberShipAdmin.MemberShipName;
            memberShip.Month = updateMemberShipAdmin.Month;
            memberShip.CapacityHostel = updateMemberShipAdmin.CapacityHostel;
            await memberShipRepository.UpdateMemberShip(memberShip);
        }

        public async Task<bool> CheckMembershipNameExist(string memberShipName)
        {
            return await memberShipRepository.CheckMembershipNameExist(memberShipName);
        }
    }
}