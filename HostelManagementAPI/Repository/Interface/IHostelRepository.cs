using DTOs.Hostel;
using DTOs.HostelService;

namespace Repository.Interface
{
    public interface IHostelRepository
    {
        Task<int> CreateHostel(CreateHostelRequestDto createHostelRequestDto);
        Task<HostelResponseDto> GetHostelById(int id);
        Task<IEnumerable<HostelResponseDto>> GetAllHostels();
        Task UpdateHostel(int hostelId, UpdateHostelRequestDto updateHostelRequestDto);
        Task<IEnumerable<HostelResponseDto>> GetOwnerHostels(int ownerId);
        Task UpdateHostelStatus(int hostelId, int status);
        Task UpdateHostelImage(int hostelId, string imageUrl);
        Task<HostelDetailAdminView> GetHostelDetailAdminView(int id);
        Task<IEnumerable<HostelsAdminView>> GetHostelsAdminView();
        Task <IEnumerable<HostelServiceResponseDto>> GetHostelServices(int id);
        Task AddHostelServices (int hostelId, HostelServiceRequestDto hostelServiceRequestDto);
    }
}
