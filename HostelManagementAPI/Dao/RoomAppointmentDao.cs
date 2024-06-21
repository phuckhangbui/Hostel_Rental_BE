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

        public async Task<GetAppointmentContract> GetApppointmentToCreateContract(int roomID)
        {
            var context = new DataContext();
            var appointment = await context.RoomAppointments
                .Include(ra => ra.Room)
                .Include(ra => ra.Viewer)
                .Where(ra => ra.RoomId == roomID && (ra.Status == (int)AppointmentStatus.View || ra.Status == (int)AppointmentStatus.Hire_Directly))
                .Select(ra => new AccountAppointment
                {
                    ViewerId = ra.ViewerId,
                    ViewerName = ra.Viewer.Name,
                    ViewerPhone = ra.Viewer.Phone,
                    ViewerEmail = ra.Viewer.Email,
                    ViewerCitizenCard = ra.Viewer.CitizenCard,
                    AppointmentTime = ra.AppointmentTime,
                    Status = ra.Status
                })
                .ToListAsync();

            var room = await context.RoomAppointments
                .Include(ra => ra.Room)
                .Include(ra => ra.Viewer)
                .Where(ra => ra.RoomId == roomID)
                .Select(ra => new GetAppointmentContract
                {
                    RoomId = ra.RoomId,
                    RoomFee = ra.Room.RoomFee,
                    RoomName = ra.Room.RoomName,
                    Capacity = (int)ra.Room.Capacity,
                    AccountAppointments = appointment
                }).FirstOrDefaultAsync();

            return room;
        }

        public async Task<List<int>> UpdateAppointmentRoom(int? roomID, int accountID)
        {
            var context = new DataContext();
            var appointment = await context.RoomAppointments
                .Where(ra => ra.RoomId == roomID && ra.ViewerId == accountID && (ra.Status == (int)AppointmentStatus.View || ra.Status == (int)AppointmentStatus.Hire_Directly))
                .FirstOrDefaultAsync();
            appointment.Status = (int)AppointmentStatus.Accept;
            await UpdateAsync(appointment);

            var accouuntList = new List<int>();
            var appointments = await context.RoomAppointments
                .Where(ra => ra.RoomId == roomID && (ra.Status == (int)AppointmentStatus.View || ra.Status == (int)AppointmentStatus.Hire_Directly))
                .ToListAsync();

            foreach(var item in appointments)
            {
                item.Status = (int)AppointmentStatus.Cancel;
                await UpdateAsync(item);
                accouuntList.Add(item.ViewerId);
            }
            return accouuntList;
        }

        public async Task<IEnumerable<GetAppointmentOwner>> GetRoomAppointmentListByOwner(int hostelID)
        {
            var context = new DataContext();
            var appointments = await context.RoomAppointments
                .Include(ra => ra.Room)
                .ThenInclude(x => x.Hostel)
                .Include(ra => ra.Viewer)
                .Where(ra => ra.Room.Hostel.HostelID == hostelID)
                .Select(ra => new GetAppointmentOwner
                {
                    ViewRoomAppointmentId = ra.ViewRoomAppointmentId,
                    RoomName = ra.Room.RoomName,
                    ViewerName = ra.Viewer.Name,
                    ViewerPhone = ra.Viewer.Phone,
                    AppointmentTime = ra.AppointmentTime,
                    Status = ra.Status
                })
                .OrderByDescending(x => x.AppointmentTime)
                .ToListAsync();

            return appointments;
        }

        public async Task CancelAppointmentRoom(int appointmentID)
        {
            var context = new DataContext();
            var appointment = await context.RoomAppointments.FirstOrDefaultAsync(x => x.ViewRoomAppointmentId == appointmentID);
            appointment.Status = (int)AppointmentStatus.Cancel;
            await UpdateAsync(appointment);
        }
    }
}
