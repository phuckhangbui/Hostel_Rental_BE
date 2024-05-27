using DTOs.Complain;

namespace Service.Interface
{
    public interface IComplainService
    {
        Task CreateComplain(CreateComplainDto complainDto, int complainCreatorId);
        Task<DisplayComplainDto> GetComplainById(int id);
        Task<IEnumerable<DisplayComplainDto>> GetComplains();
        Task<IEnumerable<DisplayComplainDto>> GetComplainsByRoom(int id);
        Task<IEnumerable<DisplayComplainDto>> GetComplainsByAccountCreator(int id);
        Task UpdateComplainStatus(UpdateComplainStatusDto updateComplainRequest);
    }
}
