using DTOs;
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

        public async Task<MemberShipRegisterTransactionDto> ConfirmTransaction(VnPayReturnUrlDto vnPayReturnUrlDto)
        {
            var membershipTransaction = await _membershipRegisterRepository.GetMembershipTransactionBaseOnTnxRef(vnPayReturnUrlDto.TnxRef);


            if (vnPayReturnUrlDto == null)
            {
                throw new ServiceException("No transaction match");
            }
            var membership = await _memberShipRepository.GetMembershipById((int)membershipTransaction.MemberShipID);

            membershipTransaction.Status = (int)MembershipRegisterEnum.Done;

            DateTime? expireDate = membershipTransaction.DateExpire;


            if (expireDate == null)
            {
                membershipTransaction.DateExpire = DateTime.Now.AddMonths((int)membership.Month);
            }
            else if (expireDate?.DayOfYear > DateTime.Now.DayOfYear)
            {
                membershipTransaction.DateExpire = expireDate?.AddMonths((int)membership.Month);
            }

            await _membershipRegisterRepository.UpdateMembership(membershipTransaction);

            return membershipTransaction;
        }
    }
}
