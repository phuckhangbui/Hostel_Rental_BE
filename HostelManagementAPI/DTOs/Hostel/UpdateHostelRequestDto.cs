using System.ComponentModel.DataAnnotations;

namespace DTOs.Hostel
{
	public class UpdateHostelRequestDto : CreateHostelRequestDto
	{
		[Required]
		public int HostelId { get; set; }
	}
}
