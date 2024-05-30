using AutoMapper;
using DTOs.MemberShipRegisterTransaction;
using Repository.Interface;
using Service.Interface;

namespace Service.Implement
{
    public class MembershipRegisterService : IMembershipRegisterService
    {
        public IMembershipRegisterRepository membershipRegisterRepository;
        private readonly IMapper _mapper;
        public MembershipRegisterService(IMembershipRegisterRepository membershipRegisterRepository, IMapper mapper)
        {
            this.membershipRegisterRepository = membershipRegisterRepository;
            _mapper = mapper;
        }
        public Task<IEnumerable<ViewMemberShipDto>> GetAllMemberships()
        {
            return membershipRegisterRepository.GetAllMemberships();
        }

        public async Task<ViewMemberShipDetailDto> GetDetailMemberShipRegister(int registerID)
        {
            var register = await membershipRegisterRepository.GetDetailMemberShipRegister(registerID);
            return _mapper.Map<ViewMemberShipDetailDto>(register);
        }
    }
}
