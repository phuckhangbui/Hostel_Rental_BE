using BusinessObject.Models;
using DTOs.Enum;
using DTOs.Room;
using Microsoft.EntityFrameworkCore;

namespace DAO
{
    public class RoomDao : BaseDAO<Room>
    {
        private static RoomDao instance = null;
        private static readonly object instacelock = new object();

        private RoomDao()
        {

        }

        public static RoomDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoomDao();
                }
                return instance;
            }
        }

        public async Task<OwnerInfoDto> GetOwnerInfoByRoomId(int roomId)
        {
            using (var context = new DataContext())
            {
                // Query to get the owner's info based on roomId
                var ownerInfo = await (from room in context.Room
                                       join hostel in context.Hostel on room.HostelID equals hostel.HostelID
                                       join account in context.Account on hostel.AccountID equals account.AccountID
                                       where room.RoomID == roomId
                                       select new OwnerInfoDto
                                       {
                                           Name = account.Name,
                                           Phone = account.Phone,
                                           Email = account.Email
                                       }).FirstOrDefaultAsync();

                return ownerInfo;
            }
        }

        public async Task<Room> GetRoomById(int roomId)
        {
            var context = new DataContext();
            return await context.Room.Include(x => x.Hostel).ThenInclude(h => h.OwnerAccount).FirstOrDefaultAsync(r => r.RoomID == roomId);
        }

        public async Task<IEnumerable<Room>> GetRoomListByHostelId(int hostelId)
        {
            using (var context = new DataContext())
            {
                return await context.Room
                .Where(r => r.HostelID == hostelId)
                .Include(r => r.RoomImages)
                .Include(x => x.Hostel).ThenInclude(h => h.OwnerAccount)
                .ToListAsync();
            }
        }

        public async Task<Room> GetRoomDetailById(int roomId)
        {
            using (var context = new DataContext())
            {
                return await context.Room
                    .Include(r => r.RoomImages)
                    .Include(r => r.RoomServices)
                        .ThenInclude(rs => rs.TypeService)
                    .Include(x => x.Hostel).ThenInclude(h => h.OwnerAccount)
                    .FirstOrDefaultAsync(r => r.RoomID == roomId);
            }
        }

        public async Task<List<string>> GetRoomImagesByHostelId(int hostelId)
        {
            var context = new DataContext();
            return await context.Room
                                    .Where(r => r.HostelID == hostelId)
                                    .Join(context.RoomsImage,
                                          r => r.RoomID,
                                          ri => ri.RoomID,
                                          (r, ri) => ri.RoomUrl)
                                    .ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetHiringRoomForOwner(int ownerId)
        {
            using (var context = new DataContext())
            {
                return await context.Room
                    .Include(r => r.Hostel)
                    .Include(r => r.RoomImages)
                    .Include(r => r.RoomContract)
                        .ThenInclude(rc => rc.StudentLeadAccount)
                    .Where(r => r.Hostel.AccountID == ownerId && r.Status == (int)RoomEnum.Hiring)
                    .ToListAsync();
            }
        }

        public async Task<int> GetAvailableRoomCountByHostelId(int hostelId)
        {
            using (var context = new DataContext())
            {
                return await context.Room
                    .Where(r => r.HostelID == hostelId && r.Status == (int)RoomEnum.Available)
                    .CountAsync();
            }
        }
    }
}
