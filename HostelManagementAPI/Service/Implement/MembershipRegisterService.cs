using DAO;
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
        public async Task<IEnumerable<ViewHistoryMemberShipDtos>> GetAllMembershipPackageInAccount(int accountID)
        {
            return await membershipRegisterRepository.GetAllMembershipPackageInAccount(accountID);
        }
    }
}
