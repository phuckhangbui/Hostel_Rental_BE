namespace DTOs.Room
{
    public class CreateRoomRequestDto
    {
        public string RoomName { get; set; }
        public int Capacity { get; set; }
        public double Lenght { get; set; }
        public double Width { get; set; }
        public string Description { get; set; }
        public double RoomFee { get; set; }
        public int HostelID { get; set; }
    }
}
