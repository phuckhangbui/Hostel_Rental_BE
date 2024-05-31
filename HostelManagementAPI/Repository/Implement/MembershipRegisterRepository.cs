using AutoMapper;
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
        public async Task<IEnumerable<ViewMemberShipDto>> GetAllMemberships()
        {
            return await MemberShipRegisterDao.Instance.GetAllMembership();
             
        }

        public async Task<ViewMemberShipDetailDto> GetDetailMemberShipRegister(int registerID)
        {
            var transaction = await MemberShipRegisterDao.Instance.GetDetailMemberShipRegister(registerID);
            return _mapper.Map<ViewMemberShipDetailDto>(transaction);
        }
    }
}
