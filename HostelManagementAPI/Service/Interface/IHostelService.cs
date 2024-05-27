using DTOs.Hostel;
using Microsoft.AspNetCore.Http;

namespace Service.Interface
{
    public interface IHostelService
    {
        Task CreateHostel(CreateHostelRequestDto createHostelRequestDto);
        Task<IEnumerable<HostelListResponseDto>> GetHostels();
        Task UpdateHostel(UpdateHostelRequestDto updateHostelRequestDto);
        Task ChangeHostelStatus(int hostelId, int status);
        Task<IEnumerable<HostelListResponseDto>> GetHostelsByOwner(int ownerId);
        Task UploadHostelThumbnail(int hostelId, IFormFile formFile);
    }
}
