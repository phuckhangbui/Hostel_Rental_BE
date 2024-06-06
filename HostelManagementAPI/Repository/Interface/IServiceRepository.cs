using BusinessObject.Models;
using DTOs.Service;

namespace Repository.Interface
{
    public interface IServiceRepository
    {
        Task<bool> CreateService(Services service);
        Task<bool> UpdateService(Services service);
        Task<Services> GetServiceById(int id);
        Task<List<ServiceResponseDto>> GetServices();
        Task RemoveServiceAsync(int serviceId);

    }
}
