using BusinessObject.Models;
using DAO;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class MemberShipRepository : IMemberShipRepository
    {
        public async Task<bool> CreateMemberShip(MemberShip memberShip)
        {
            return await MemberShipDao.Instance.CreateAsync(memberShip);
        }
    }
}
