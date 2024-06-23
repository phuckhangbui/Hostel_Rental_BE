namespace DTOs.RoomAppointment
{
    public class GetAppointmentMember
    {
        public int ViewRoomAppointmentId { get; set; }
        public string RoomName { get; set; }
        public int RoomId { get; set; }
        public DateTime AppointmentTime { get; set; }
        public int Status { get; set; }
    }
}
