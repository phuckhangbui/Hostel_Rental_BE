using BusinessObject.Models;
using DTOs.RoomService;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        //        public async Task AddRoomServicesAsync(IEnumerable<RoomService> roomServices)
        //        {
        //            var context = new DataContext();
        //            foreach (var roomService in roomServices)
        //            {
        //                var existingRoomService = await context.RoomService
        //                    .FirstOrDefaultAsync(rs => rs.RoomId == roomService.RoomId && rs.ServiceId == roomService.ServiceId);

        //                if (existingRoomService == null)
        //                {
        //                    await context.RoomService.AddAsync(roomService);
        //                }
        //                else
        //                {
        //                    existingRoomService.Status = roomService.Status;
        //                    context.RoomService.Update(existingRoomService);
        //                }
        //            }

        //            await context.SaveChangesAsync();
        //        }

        public async Task RemoveRoomServiceAsync(int roomId, int serviceId)
        {
            var context = new DataContext();
            var roomService = await context.RoomService.FirstOrDefaultAsync(rs => rs.RoomId == roomId && rs.RoomServiceId == serviceId);
            if (roomService != null)
            {
                context.RoomService.Remove(roomService);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateRoomServicesIsSelectStatusAsync(int roomId, List<RoomServiceUpdateDto> roomServiceUpdates)
        {
            using (var context = new DataContext())
            {
                var roomServiceIds = roomServiceUpdates.Select(rs => rs.RoomServiceId).ToList();

                var roomServices = await context.RoomService
                    .Where(rs => rs.RoomId == roomId && roomServiceIds.Contains(rs.RoomServiceId))
                    .ToListAsync();

                foreach (var roomService in roomServices)
                {
                    var update = roomServiceUpdates.FirstOrDefault(rs => rs.RoomServiceId == roomService.RoomServiceId);
                    if (update != null)
                    {
                        roomService.IsSelected = update.IsSelected;
                    }
                }

                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<RoomService>> GetRoomServicesIsSelected(int roomId)
        {
            using (var context = new DataContext())
            {
                return await context.RoomService
                    .Include(rs => rs.TypeService)
                    .Where(rs => rs.RoomId == roomId && rs.IsSelected == true)
                    .ToListAsync();
            }
        }
    }
}
