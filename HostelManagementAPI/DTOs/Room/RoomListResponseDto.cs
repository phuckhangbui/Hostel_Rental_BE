namespace DTOs.Room
{
    public class RoomListResponseDto
    {
        public int RoomID { get; set; }
        public string? RoomName { get; set; }
        public int? Capacity { get; set; }
        public double? RoomFee { get; set; }
        public int? HostelID { get; set; }
        public int Status { get; set; }
        public string? RoomThumbnail { get; set; }
        public double? Area { get; set; }

        public string? HostelName { get; set; }
        public int? OwnerID { get; set; }
        public string? OwnerName { get; set; }
        public string? HostelAddress { get; set; }

    }
}
