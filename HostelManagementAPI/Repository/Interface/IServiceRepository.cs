using BusinessObject.Models;

namespace Repository.Interface
{
    public interface IServiceRepository
    {
        Task CreateService(Service service);
        Task UpdateService(Service service);
        Task DeleteService(int id);
        Task<Service> GetServiceById(int id);
        Task<List<Service>> GetServices();
    }
}
