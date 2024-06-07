using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.RoomAppointment
{
    public class CreateRoomAppointmentDto
    {
        public int RoomId { get; set; }
        public int ViewerId { get; set; }
        public DateTime AppointmentTime { get; set; }
        public int Status { get; set; }
    }
}
