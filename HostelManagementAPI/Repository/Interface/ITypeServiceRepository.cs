using BusinessObject.Models;

namespace Repository.Interface
{
    public interface ITypeServiceRepository
    {
        Task<bool> CreateTypeService(TypeService typeService);
        Task<bool> UpdateTypeService(TypeService typeService);
        Task DeleteTypeService(int id);
        Task<TypeService> GetTypeServiceById(int id);
        Task<IEnumerable<TypeService>> GetTypeServices();
    }
}
