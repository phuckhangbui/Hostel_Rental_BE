using DTOs.Hostel;

namespace Service.Interface
{
    public interface IHostelService
    {
        Task CreateHostel(CreateHostelRequestDto createHostelRequestDto);
        Task<IEnumerable<HostelListResponseDto>> GetHostels();
        Task UpdateHostel(UpdateHostelRequestDto updateHostelRequestDto);
        Task ChangeHostelStatus(int hostelId, int status);
        Task<IEnumerable<HostelListResponseDto>> GetHostelsByOwner(int ownerId);

    }
}
