using DAO;
using DTOs.MemberShipRegisterTransaction;
using Repository.Interface;

namespace Repository.Implement
{
    public class MembershipRegisterRepository : IMembershipRegisterRepository
    {
        public async Task<IEnumerable<ViewMemberShipDto>> GetAllMemberships()
        {
            return await MemberShipRegisterDao.Instance.GetAllMembership();
             
        }
    }
}
