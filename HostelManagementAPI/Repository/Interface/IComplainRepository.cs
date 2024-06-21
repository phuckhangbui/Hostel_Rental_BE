using BusinessObject.Models;
using DTOs.Complain;

namespace Repository.Interface
{
    public interface IComplainRepository
    {
        Task CreateComplain(Complain complain);
        Task<Complain> GetComplainById(int id);
        Task<IEnumerable<ComplainDto>> GetComplains();
        Task UpdateComplain(Complain complain);
    }
}
