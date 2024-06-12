using BusinessObject.Models;
using DTOs.Enum;
using DTOs.RoomAppointment;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class RoomAppointmentDao: BaseDAO<RoomAppointment>
    {
        private static RoomAppointmentDao instance = null;
        private static readonly object instacelock = new object();

        private RoomAppointmentDao()
        {
        }

        public static RoomAppointmentDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoomAppointmentDao();
                }
                return instance;
            }
        }

        public async Task<List<GetAppointmentDto>> GetRoomAppointmentsAsync()
        {
            var context = new DataContext();
            var appointments = await context.RoomAppointments
                .Include(ra => ra.Room)
                .Include(ra => ra.Viewer)
                .Select(ra => new GetAppointmentDto
                {
                    ViewRoomAppointmentId = ra.ViewRoomAppointmentId,
                    RoomId = ra.RoomId,
                    RoomName = ra.Room.RoomName,
                    RoomFee = ra.Room.RoomFee,
                    ViewerId = ra.ViewerId,
                    ViewerName = ra.Viewer.Name,
                    ViewerPhone = ra.Viewer.Phone,
                    ViewerEmail = ra.Viewer.Email,
                    ViewerCitizenCard = ra.Viewer.CitizenCard,
                    AppointmentTime = ra.AppointmentTime,
                    Status = ra.Status
                })
                .ToListAsync();

            return appointments;
        }

        public async Task<GetAppointmentDto> GetRoomAppointmentByIdAsync(int id)
        {
            var context = new DataContext();
            var appointment = await context.RoomAppointments
                .Include(ra => ra.Room)
                .Include(ra => ra.Viewer)
                .Where(ra => ra.ViewRoomAppointmentId == id)
                .Select(ra => new GetAppointmentDto
                {
                    ViewRoomAppointmentId = ra.ViewRoomAppointmentId,
                    RoomId = ra.RoomId,
                    RoomName = ra.Room.RoomName,
                    RoomFee = ra.Room.RoomFee,
                    ViewerId = ra.ViewerId,
                    ViewerName = ra.Viewer.Name,
                    ViewerPhone = ra.Viewer.Phone,
                    ViewerEmail = ra.Viewer.Email,
                    ViewerCitizenCard = ra.Viewer.CitizenCard,
                    AppointmentTime = ra.AppointmentTime,
                    Status = ra.Status
                })
                .FirstOrDefaultAsync();

            return appointment;
        }

        public async Task<GetAppointmentDto> GetApppointmentToCreateContract(int roomID)
        {
            var context = new DataContext();
            var appointment = await context.RoomAppointments
                .Include(ra => ra.Room)
                .Include(ra => ra.Viewer)
                .Where(ra => ra.RoomId == roomID && ra.Status == (int)AppointmentStatus.View)
                .Select(ra => new GetAppointmentDto
                {
                    ViewRoomAppointmentId = ra.ViewRoomAppointmentId,
                    RoomId = ra.RoomId,
                    RoomName = ra.Room.RoomName,
                    RoomFee = ra.Room.RoomFee,
                    ViewerId = ra.ViewerId,
                    ViewerName = ra.Viewer.Name,
                    ViewerPhone = ra.Viewer.Phone,
                    ViewerEmail = ra.Viewer.Email,
                    ViewerCitizenCard = ra.Viewer.CitizenCard,
                    AppointmentTime = ra.AppointmentTime,
                    Status = ra.Status
                })
                .FirstOrDefaultAsync();

            return appointment;
        }

        public async Task UpdateAppointmentRoom(int? roomID)
        {
            var context = new DataContext();
            var appointment = await context.RoomAppointments
                .Where(ra => ra.RoomId == roomID && ra.Status == (int)AppointmentStatus.View)
                .FirstOrDefaultAsync();
            appointment.Status = (int)AppointmentStatus.Accept;
            await UpdateAsync(appointment);
        }
    }
}
