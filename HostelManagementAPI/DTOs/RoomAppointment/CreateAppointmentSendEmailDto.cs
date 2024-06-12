namespace DTOs.RoomAppointment
{
    public class CreateAppointmentSendEmailDto
    {
        public int RoomId { get; set; }
        public int ViewerId { get; set; }
        public DateTime AppointmentTime { get; set; }
        public string RoomName { get; set; }
    }
}
