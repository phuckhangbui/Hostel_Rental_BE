using AutoMapper;
using BusinessObject.Models;
using DAO;
using DTOs.Enum;
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

        public async Task<IEnumerable<ViewTransactionMembership>> GetAllTransactionInAdmin()
        {
            var transactions = await MemberShipRegisterDao.Instance.GetAllTransactionMembership();
            return _mapper.Map<IEnumerable<ViewTransactionMembership>>(transactions);
        }


        public async Task<MemberShipRegisterTransactionDto> RegisterMembership(int accountId, int membershipId, double membershipFee)
        {

            var membershipTransaction = new MemberShipRegisterTransaction
            {
                AccountID = accountId,
                MemberShipID = membershipId,
                Status = (int)MembershipRegisterEnum.Pending,
                DateRegister = DateTime.Now,
                TnxRef = DateTime.Now.Ticks.ToString(),
                PackageFee = membershipFee

            };

            await MemberShipRegisterDao.Instance.CreateAsync(membershipTransaction);

            return _mapper.Map<MemberShipRegisterTransactionDto>(membershipTransaction);
        }

        public async Task<MemberShipRegisterTransactionDto> GetMembershipTransactionBaseOnTnxRef(string tnxRef)
        {
            var membershipTransaction = await MemberShipRegisterDao.Instance.GetMembershipTnxRef(tnxRef);

            return _mapper.Map<MemberShipRegisterTransactionDto>(membershipTransaction);

        }
        public async Task UpdateMembership(MemberShipRegisterTransactionDto memberShipRegisterTransactionDto)
        {
            var membershipTransaction = _mapper.Map<MemberShipRegisterTransaction>(memberShipRegisterTransactionDto);

            await MemberShipRegisterDao.Instance.UpdateAsync(membershipTransaction);
        }
    }
}
