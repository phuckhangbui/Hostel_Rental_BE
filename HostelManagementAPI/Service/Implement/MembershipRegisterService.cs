using DTOs.Enum;
using DTOs.Membership;
using DTOs.MemberShipRegisterTransaction;
using Repository.Interface;
using Service.Exceptions;
using Service.Interface;

namespace Service.Implement
{
    public class MembershipRegisterService : IMembershipRegisterService
    {
        private readonly IMembershipRegisterRepository _membershipRegisterRepository;
        private readonly IMemberShipRepository _memberShipRepository;

        public MembershipRegisterService(IMembershipRegisterRepository membershipRegisterRepository, IMemberShipRepository memberShipRepository)
        {
            _membershipRegisterRepository = membershipRegisterRepository;
            _memberShipRepository = memberShipRepository;
        }
        public async Task<IEnumerable<ViewHistoryMemberShipDtos>> GetAllMembershipPackageInAccount(int accountID)
        {
            return await _membershipRegisterRepository.GetAllMembershipPackageInAccount(accountID);
        }

        public async Task<IEnumerable<ViewTransactionMembership>> GetAllTransactionInAdmin()
        {
            return await _membershipRegisterRepository.GetAllTransactionInAdmin();
        }

        public async Task<MemberShipRegisterTransactionDto> RegisterMembership(RegisterMemberShipDto registerMemberShipDto)
        {
            var membership = await _memberShipRepository.GetMembershipById(registerMemberShipDto.MembershipId);

            if (membership == null || membership?.Status == (int)MemberShipEnum.Expire)
            {
                throw new ServiceException("this membership package now is not available");
            }
            return await _membershipRegisterRepository.RegisterMembership(registerMemberShipDto.AccountId, registerMemberShipDto.MembershipId, (double)membership.MemberShipFee);
        }

    }
}
