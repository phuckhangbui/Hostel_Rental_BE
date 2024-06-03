using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class RoomServiceDao: BaseDAO<RoomService>
    {
        private static RoomServiceDao instance = null;
        private readonly DataContext dataContext;

        private RoomServiceDao()
        {
            dataContext = new DataContext();
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
            foreach (var roomService in roomServices)
            {
                var existingRoomService = await dataContext.RoomService
                    .FirstOrDefaultAsync(rs => rs.RoomId == roomService.RoomId && rs.ServiceId == roomService.ServiceId);

                if (existingRoomService == null)
                {
                    await dataContext.RoomService.AddAsync(roomService);
                }
                else
                {
                    existingRoomService.Status = roomService.Status;
                    dataContext.RoomService.Update(existingRoomService);
                }
            }

            await dataContext.SaveChangesAsync();
        }
    }
}
