using System.ComponentModel.DataAnnotations;

namespace DTOs.Room
{
	public class RoomRequestDto
	{
        [Required]
        public string RoomName { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than zero.")]
        public int Capacity { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Length must be greater than zero.")]
        public double Length { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Width must be greater than zero.")]
        public double Width { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Room fee must be greater than zero.")]
        public double RoomFee { get; set; }

        [Required]
        public int HostelID { get; set; }
    }
}
