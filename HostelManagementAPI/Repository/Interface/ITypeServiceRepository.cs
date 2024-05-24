using BusinessObject.Models;

namespace Repository.Interface
{
    public interface ITypeServiceRepository
    {
        Task CreateTypeService(TypeService typeService);
        Task UpdateTypeService(TypeService typeService);
        Task DeleteTypeService(int id);
        Task<TypeService> GetTypeServiceById(int id);
        Task<List<TypeService>> GetTypeServices();
    }
}
