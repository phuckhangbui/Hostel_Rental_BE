using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.RoomAppointment
{
    public class AccountAppointment
    {
        public int ViewRoomAppointmentId { get; set; }
        public int ViewerId { get; set; }
        public string ViewerName { get; set; }
        public string ViewerPhone { get; set; }
        public string ViewerEmail { get; set; }
        public string ViewerCitizenCard { get; set; }
        public DateTime AppointmentTime { get; set; }
        public int Status { get; set; }
    }
}
