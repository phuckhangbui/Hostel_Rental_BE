﻿using DTOs.MemberShipRegisterTransaction;

namespace Repository.Interface
{
    public interface IMembershipRegisterRepository
    {
        Task<IEnumerable<ViewHistoryMemberShipDtos>> GetAllMembershipPackageInAccount(int accountID);
        Task<IEnumerable<ViewTransactionMembership>> GetAllTransactionInAdmin();
        Task<MemberShipRegisterTransactionDto> RegisterMembership(int accountId, int membershipId, double membershipFee);
        Task<MemberShipRegisterTransactionDto> GetMembershipTransactionBaseOnTnxRef(string tnxRef);
        Task UpdateMembership(MemberShipRegisterTransactionDto memberShipRegisterTransactionDto);
        Task<MemberShipRegisterTransactionDto> GetCurrentActiveMembership(int accountId);
    }
}
