using BusinessObject.Models;
using DAO;
using Repository.Interface;

namespace Repository.Implement
{
    public class ComplainRepository : IComplainRepository
    {
        public async Task CreateComplain(Complain complain)
        {
            await ComplainDao.Instance.CreateAsync(complain);
        }

        public async Task<Complain> GetComplainById(int id)
        {
            return await ComplainDao.Instance.GetComplainById(id);
        }

        public async Task<IEnumerable<Complain>> GetComplains()
        {
            return await ComplainDao.Instance.GetAllAsync();
        }

        public Task UpdateComplain(Complain complain)
        {
            return ComplainDao.Instance.UpdateAsync(complain);
        }
    }
}
