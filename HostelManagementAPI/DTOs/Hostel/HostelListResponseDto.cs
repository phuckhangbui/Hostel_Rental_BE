namespace DTOs.Hostel
{
	public class HostelListResponseDto
	{
		public int HostelID { get; set; }
		public string? HostelName { get; set; }
		public string? HostelAddress { get; set; }
		public string? HostelDescription { get; set; }
		public int? AccountID { get; set; }
		public string? OwnerName { get; set; }
		public int? Status { get; set; }
		public int? NumOfAvailableRoom { get; set; }
		public string? Thumbnail {  get; set; }
		public int? NumOfTotalRoom { get; set; }
	}
}
