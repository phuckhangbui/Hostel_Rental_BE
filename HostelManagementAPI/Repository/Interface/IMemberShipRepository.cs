using BusinessObject.Models;

namespace Repository.Interface
{
    public interface IMemberShipRepository
    {
        Task<bool> CreateMemberShip(MemberShip memberShip);
    }
}
