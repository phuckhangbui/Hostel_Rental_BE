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
            var membershipTransactions = await _membershipRegisterRepository.GetAllMembershipPackageInAccount(accountID);
            return membershipTransactions.Where(t =>
                                            t.Status != (int)MembershipRegisterEnum.pending &&
                                            t.Status != (int)MembershipRegisterEnum.pending_extend &&
                                            t.Status != (int)MembershipRegisterEnum.pending_update).ToList();
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

            if (await _membershipRegisterRepository.GetCurrentActiveMembership(registerMemberShipDto.AccountId) != null)
            {
                throw new ServiceException("There is an active membership, cannot register new membership");
            }

            return await _membershipRegisterRepository.RegisterMembership(registerMemberShipDto.AccountId,
                registerMemberShipDto.MembershipId, (double)membership.MemberShipFee, (int)MembershipRegisterEnum.pending);
        }

        public async Task<MemberShipRegisterTransactionDto> ExtendMembership(RegisterMemberShipDto registerMemberShipDto)
        {
            var membership = await _memberShipRepository.GetMembershipById(registerMemberShipDto.MembershipId);

            if (membership == null || membership?.Status == (int)MemberShipEnum.Expire)
            {
                throw new ServiceException("this membership package now is not available");
            }

            if (await _membershipRegisterRepository.GetCurrentActiveMembership(registerMemberShipDto.AccountId) == null)
            {
                throw new ServiceException("There is no old membership to extend");
            }


            return await _membershipRegisterRepository.RegisterMembership(registerMemberShipDto.AccountId,
                registerMemberShipDto.MembershipId, (double)membership.MemberShipFee, (int)MembershipRegisterEnum.pending_extend);
        }

        public async Task<MemberShipRegisterTransactionDto> UpdateMembership(MembershipUpdatePackageDto membershipUpdatePackageDto)
        {
            var membership = await _memberShipRepository.GetMembershipById(membershipUpdatePackageDto.MembershipId);

            if (membership == null || membership?.Status == (int)MemberShipEnum.Expire)
            {
                throw new ServiceException("this membership package now is not available");
            }

            if (await _membershipRegisterRepository.GetCurrentActiveMembership(membershipUpdatePackageDto.AccountId) == null)
            {
                throw new ServiceException("There is no old membership to update");
            }


            return await _membershipRegisterRepository.RegisterMembership(membershipUpdatePackageDto.AccountId, membershipUpdatePackageDto.MembershipId, (double)membershipUpdatePackageDto.Fee, (int)MembershipRegisterEnum.pending_update);
        }

        public async Task<MemberShipRegisterTransactionDto> ConfirmTransaction(VnPayReturnUrlDto vnPayReturnUrlDto, int accountId)
        {
            var membershipTransaction = await _membershipRegisterRepository.GetMembershipTransactionBaseOnTnxRef(vnPayReturnUrlDto.TnxRef);

            if (vnPayReturnUrlDto == null)
            {
                throw new ServiceException("No transaction match");
            }
            var membership = await _memberShipRepository.GetMembershipById((int)membershipTransaction.MemberShipID);


            if (membershipTransaction.Status == (int)MembershipRegisterEnum.pending)
            {
                membershipTransaction.Status = (int)MembershipRegisterEnum.current;
                membershipTransaction.DateExpire = DateTime.Now.AddMonths((int)membership.Month);
                await _membershipRegisterRepository.UpdateMembership(membershipTransaction);
            }
            else
            {
                var oldMembershipTransaction = await _membershipRegisterRepository.GetCurrentActiveMembership(accountId);

                if (oldMembershipTransaction == null)
                {
                    throw new ServiceException("No old membership transaction match");
                }

                if (membershipTransaction.Status == (int)MembershipRegisterEnum.pending_extend)
                {
                    oldMembershipTransaction.Status = (int)MembershipRegisterEnum.extended;
                    await _membershipRegisterRepository.UpdateMembership(oldMembershipTransaction);

                    membershipTransaction.Status = (int)MembershipRegisterEnum.current;
                    membershipTransaction.DateExpire = oldMembershipTransaction.DateExpire.Value.AddMonths((int)membership.Month);
                    await _membershipRegisterRepository.UpdateMembership(membershipTransaction);
                }

                if (membershipTransaction.Status == (int)MembershipRegisterEnum.pending_update)
                {
                    oldMembershipTransaction.Status = (int)MembershipRegisterEnum.updated;
                    await _membershipRegisterRepository.UpdateMembership(oldMembershipTransaction);

                    membershipTransaction.Status = (int)MembershipRegisterEnum.current;
                    membershipTransaction.DateExpire = DateTime.Now.AddMonths((int)membership.Month);
                    await _membershipRegisterRepository.UpdateMembership(membershipTransaction);
                }
            }

            return membershipTransaction;
        }

        public async Task<MemberShipRegisterTransactionDto> GetCurrentActiveMembership(int accountId)
        {
            return await _membershipRegisterRepository.GetCurrentActiveMembership(accountId);
        }
    }
}
