using DTOs.Hostel;
using Microsoft.AspNetCore.Http;

namespace Service.Interface
{
    public interface IHostelService
    {
        Task<CreateHostelResponseDto> CreateHostel(CreateHostelRequestDto createHostelRequestDto);
        Task<IEnumerable<HostelResponseDto>> GetHostels();
        Task<IEnumerable<HostelsAdminView>> GetHostelsAdminView();
        Task<HostelDetailAdminView> GetHostelDetailAdminView(int hostelID);
        Task UpdateHostel(int hostelId, UpdateHostelRequestDto updateHostelRequestDto);
        Task ChangeHostelStatus(int hostelId, int status);
        Task<IEnumerable<HostelResponseDto>> GetHostelsByOwner(int ownerId);
        Task UploadHostelImages(int hostelId, IFormFileCollection files);
        Task<HostelResponseDto> GetHostelDetail(int hostelID);
        Task<HostelResponseDto> GetHostelDetailForOwner(int hostelID);
    }
}
