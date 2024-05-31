using DTOs.MemberShipRegisterTransaction;
using Repository.Interface;
using Service.Interface;

namespace Service.Implement
{
    public class MembershipRegisterService : IMembershipRegisterService
    {
        public IMembershipRegisterRepository membershipRegisterRepository;
        public MembershipRegisterService(IMembershipRegisterRepository membershipRegisterRepository)
        {
            this.membershipRegisterRepository = membershipRegisterRepository;
        }
        public Task<IEnumerable<ViewMemberShipDto>> GetAllMemberships()
        {
            return membershipRegisterRepository.GetAllMemberships();
        }

        public async Task<ViewMemberShipDetailDto> GetDetailMemberShipRegister(int registerID)
        {
            return await membershipRegisterRepository.GetDetailMemberShipRegister(registerID);
        }
    }
}
