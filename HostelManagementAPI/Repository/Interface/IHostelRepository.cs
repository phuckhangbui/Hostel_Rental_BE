using BusinessObject.Models;

namespace Repository.Interface
{
    public interface IHostelRepository
    {
        Task<bool> CreateHostel(Hostel hostel);
        Task<Hostel> GetHostelById(int id);
        Task<IEnumerable<Hostel>> GetAllHostels();
        Task UpdateHostel(Hostel hostel);
        Task<IEnumerable<Hostel>> GetOwnerHostels(int ownerId);
    }
}
