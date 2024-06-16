namespace DTOs.Hostel
{
    public class HostelResponseDto
	{
		public int HostelID { get; set; }
		public string? HostelName { get; set; }
		public string? HostelAddress { get; set; }
		public string? HostelDescription { get; set; }
		public int? AccountID { get; set; }
		public string? OwnerName { get; set; }
		public int? Status { get; set; }
		public int? NumOfAvailableRoom { get; set; }
		public List<string>? Images {  get; set; }
		public int? NumOfTotalRoom { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Phone { get; set; }
        public string? HostelType { get; set; }
        public double? LowestPrice { get; set; }
        public double? LowestArea { get; set; }

    }
}
