using System.ComponentModel.DataAnnotations;

namespace DTOs.Room
{
    public class CreateRoomRequestDto
    {
        [Required]
        public string RoomName { get; set; }
		[Required]
		public int Capacity { get; set; }
		[Required]
		public double Length { get; set; }
		[Required]
		public double Width { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public double RoomFee { get; set; }
		[Required]
		public int HostelID { get; set; }
    }
}
