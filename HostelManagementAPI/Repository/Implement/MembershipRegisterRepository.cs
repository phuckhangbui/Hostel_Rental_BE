﻿using AutoMapper;
using BusinessObject.Models;
using DAO;
using DTOs.MemberShipRegisterTransaction;
using Repository.Interface;

namespace Repository.Implement
{
    public class MembershipRegisterRepository : IMembershipRegisterRepository
    {
        private readonly IMapper _mapper;

        public MembershipRegisterRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<ViewHistoryMemberShipDtos>> GetAllMembershipPackageInAccount(int accountID)
        {
            var transaction = await MemberShipRegisterDao.Instance.GetAllMembershipPackageInAccount(accountID);
            return _mapper.Map<IEnumerable<ViewHistoryMemberShipDtos>>(transaction);
        }
    }
}
