using BusinessObject.Models;
using DTOs.TypeService;

namespace Service.Interface
{
    public interface ITypeServiceService
    {
        Task CreateTypeService (CreateTypeServiceDto typeService);
        Task<IEnumerable<TypeService>> GetAllTypeService();
        Task<bool> UpdateTypeServiceName(UpdateTypeServiceDto updateTypeServiceDto);
        Task<bool> CheckExistTypeService(int TypeServiceId);
    }
}
