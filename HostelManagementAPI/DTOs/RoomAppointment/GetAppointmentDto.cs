using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.RoomAppointment
{
    public class GetAppointmentDto
    {
        public int ViewRoomAppointmentId { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public double? RoomFee { get; set; }
        public int ViewerId { get; set; }
        public string ViewerName { get; set; }
        public string ViewerPhone { get; set; }
        public string ViewerEmail { get; set; }
        public string ViewerCitizenCard { get; set; }
        public DateTime AppointmentTime { get; set; }
        public int Status { get; set; }
    }
}
