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
	}
}
