namespace DTOs.Room
{
    public class MemberRoomRentedResponse
    {
        public int? RoomID { get; set; }
        public string? RoomName { get; set; }
        public int? HostelID { get; set; }
        public string? HostelName { get; set; }
        public int? OwnerId { get; set; }
        public string? OwnerName { get; set; }
        public int Status { get; set; }
        public string? RoomThumbnail { get; set; }
        public int StudentAccountId { get; set; }
        public int ContractId { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public Double? RoomFee { get; set; }

    }
}
