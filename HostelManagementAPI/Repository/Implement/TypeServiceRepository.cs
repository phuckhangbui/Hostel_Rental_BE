using BusinessObject.Models;
using DAO;
using Repository.Interface;

namespace Repository.Implement
{
    public class TypeServiceRepository : ITypeServiceRepository
    {
        public async Task<bool> CreateTypeService(TypeService typeService)
        {
            return await TypeServiceDao.Instance.CreateAsync(typeService);
        }

        public Task DeleteTypeService(int id)
        {
            throw new NotImplementedException();
        }

        public TypeService GetTypeServiceById(int id)
        {
            return TypeServiceDao.Instance.GetTypeServiceById(id);
        }

        public async Task<IEnumerable<TypeService>> GetTypeServices()
        {
            return await TypeServiceDao.Instance.GetAllAsync();
        }

        public async Task<bool> UpdateTypeService(TypeService typeService)
        {
            return await TypeServiceDao.Instance.UpdateAsync(typeService);
        }
    }
}
