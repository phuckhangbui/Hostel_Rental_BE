using DTOs.TypeService;

namespace Service.Interface
{
    public interface ITypeServiceService
    {
        Task CreateTypeService (CreateTypeServiceDto typeService);
        Task<IEnumerable<ViewAllTypeServiceDto>> GetAllTypeService();
        Task<bool> UpdateTypeServiceName(UpdateTypeServiceDto updateTypeServiceDto);
        Task<bool> CheckExistTypeService(int TypeServiceId);
        Task<bool> CheckTypeServiceNameExist(string typeServiceName);
    }
}
