using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DAO
{
    public class RoomServiceDao : BaseDAO<RoomService>
    {
        private static RoomServiceDao instance = null;
        private static readonly object instacelock = new object();


        private RoomServiceDao()
        {
        }

        public static RoomServiceDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoomServiceDao();
                }
                return instance;
            }
        }

        public async Task AddRoomServicesAsync(IEnumerable<RoomService> roomServices)
        {
            var context = new DataContext();
            foreach (var roomService in roomServices)
            {
                var existingRoomService = await context.RoomService
                    .FirstOrDefaultAsync(rs => rs.RoomId == roomService.RoomId && rs.ServiceId == roomService.ServiceId);

                if (existingRoomService == null)
                {
                    await context.RoomService.AddAsync(roomService);
                }
                else
                {
                    existingRoomService.Status = roomService.Status;
                    context.RoomService.Update(existingRoomService);
                }
            }

            await context.SaveChangesAsync();
        }

        public async Task RemoveRoomServiceAsync(int roomId, int serviceId)
        {
            var context = new DataContext();
            var roomService = await context.RoomService.FirstOrDefaultAsync(rs => rs.RoomId == roomId && rs.ServiceId == serviceId);
            if (roomService != null)
            {
                context.RoomService.Remove(roomService);
                await context.SaveChangesAsync();
            }
        }
    }
}
