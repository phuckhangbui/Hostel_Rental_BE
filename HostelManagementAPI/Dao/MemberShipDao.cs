using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DAO
{
    public class MemberShipDao : BaseDAO<MemberShip>
    {
        private static MemberShipDao instance = null;
        private readonly DataContext dataContext;

        public MemberShipDao()
        {
            dataContext = new DataContext();
        }

        public static MemberShipDao Instance
        {
            get
            {
                    if (instance == null)
                    {
                        instance = new MemberShipDao();
                    }
                    return instance;
                
            }
        }

        public async Task<MemberShip> GetMemberShipById(int id)
        {
            MemberShip memberShip = null;
            memberShip = dataContext.Membership.FirstOrDefault(x => x.MemberShipID == id);
            return memberShip;
        }

        public async Task<IEnumerable<MemberShip>> GetAllPackagesTotalActiveAsync()
        {
            return await dataContext.Membership.Where(x => x.Status == 0)
                .ToListAsync();
        }
    }
}
