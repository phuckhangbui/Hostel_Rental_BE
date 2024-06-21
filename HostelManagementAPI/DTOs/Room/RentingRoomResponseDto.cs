namespace DTOs.Room
{
    public class RentingRoomResponseDto
    {
        public int RoomID { get; set; }
        public string? RoomName { get; set; }
        public int? HostelID { get; set; }
        public string? HostelName { get; set; }
        public int Status { get; set; }
        public string? RoomThumbnail { get; set; }
        public int StudentAccountId { get; set; }
        public string? StudentName { get; set; }
        public int ContractId { get; set; }
    }
}
