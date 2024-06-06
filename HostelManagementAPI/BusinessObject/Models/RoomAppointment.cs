using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models
{
    public class RoomAppointment
    {
        [Key]
        public int ViewRoomAppointmentId { get; set; }

        public Room? Room { get; set; }
        public int RoomId { get; set; }
        public Account? Viewer { get; set; }
        public int ViewerId { get; set; }
        public DateTime AppointmentTime { get; set; }
        public int Status { get; set; }
    }
}
