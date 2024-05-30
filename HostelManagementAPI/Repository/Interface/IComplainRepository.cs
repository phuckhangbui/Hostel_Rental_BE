using BusinessObject.Models;

namespace Repository.Interface
{
    public interface IComplainRepository
    {
        Task CreateComplain(Complain complain);
        Task<Complain> GetComplainById(int id);
        Task<IEnumerable<Complain>> GetComplains();
        Task UpdateComplain(Complain complain);
    }
}
