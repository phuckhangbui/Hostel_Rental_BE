using BusinessObject.Models;
using DTOs.TypeService;

namespace Repository.Interface
{
    public interface ITypeServiceRepository
    {
        Task<bool> CreateTypeService(CreateTypeServiceDto createTypeServiceDto);
        Task<bool> UpdateTypeService(UpdateTypeServiceDto updateTypeServiceDto);
        Task DeleteTypeService(int id);
        Task<TypeService> GetTypeServiceById(int id);
        Task<IEnumerable<ViewAllTypeServiceDto>> GetTypeServices();
        Task<bool> CheckTypeServiceNameExist(string typeServiceName);
    }
}
