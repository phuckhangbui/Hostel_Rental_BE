using System.ComponentModel.DataAnnotations;

namespace DTOs.Hostel
{
	public class CreateHostelRequestDto
	{
		[Required]
		public string HostelName { get; set; }
		[Required]
		public string HostelAddress { get; set; }
		[Required]
		public string HostelDescription { get; set; }
		[Required]
		public int AccountID { get; set; }
		[Required]
		public string HostelType { get; set; }
	}
}
