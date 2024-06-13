using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.RoomAppointment
{
    public class GetAppointmentOwner
    {
        public int ViewRoomAppointmentId { get; set; }
        public string RoomName { get; set; }
        public string ViewerName { get; set; }
        public string ViewerPhone { get; set; }
        public DateTime AppointmentTime { get; set; }
        public int Status { get; set; }
    }
}
