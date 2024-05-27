using BusinessObject.Enum;
using BusinessObject.Models;
using DAO;
using DTOs.Membership;
using Microsoft.EntityFrameworkCore;
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
        public MemberShip GetMembershipById(int memberShipID)
        {
            return MemberShipDao.Instance.GetMemberShipById(memberShipID);
        }

        public async Task<IEnumerable<GetMemberShipDto>> GetMembershipsActive()
        {
            var memberShips = await MemberShipDao.Instance.GetAllAsync();
            var activeMemberShips = memberShips.Where(x => x.Status == 0);

            var result = activeMemberShips.Select(x => new GetMemberShipDto
            {
                MemberShipID = x.MemberShipID,
                MemberShipName = x.MemberShipName,
                CapacityHostel = x.CapacityHostel,
                Month = x.Month,
                MemberShipFee = x.MemberShipFee,
                Status = x.Status
            }); ;

            result = result.OrderBy(x => x.MemberShipFee);
            return result.ToList();
        }

        public async Task<IEnumerable<GetMemberShipDto>> GetMembershipExpire()
        {
            var memberShips = await MemberShipDao.Instance.GetAllAsync();
            var activeMemberShips = memberShips.Where(x => x.Status == 1);

            var result = activeMemberShips.Select(x => new GetMemberShipDto
            {
                MemberShipID = x.MemberShipID,
                MemberShipName = x.MemberShipName,
                CapacityHostel = x.CapacityHostel,
                Month = x.Month,
                MemberShipFee = x.MemberShipFee,
                Status = x.Status
            }); ;

            result = result.OrderBy(x => x.MemberShipFee);
            return result.ToList();
        }

        public async Task<IEnumerable<GetMemberShipDto>> GetAllMemberships()
        {
            var memberShips = await MemberShipDao.Instance.GetAllAsync();

            var result = memberShips.Select(x => new GetMemberShipDto
            {
                MemberShipID = x.MemberShipID,
                MemberShipName = x.MemberShipName,
                CapacityHostel = x.CapacityHostel,
                Month = x.Month,
                MemberShipFee = x.MemberShipFee,
                Status = x.Status
            }); ;

            result = result.OrderBy(x => x.MemberShipFee);
            return result.ToList();
        }

        public async Task<bool> UpdateMembershipStatus(MemberShip memberShip)
        {
            if (memberShip == null)
            {
                return false;
            }
            else
            {
                try
                {
                    await MemberShipDao.Instance.UpdateAsync(memberShip);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
            }
        }
    }
}
