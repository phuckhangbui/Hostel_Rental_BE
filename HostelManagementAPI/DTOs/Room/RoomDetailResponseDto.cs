using DTOs.RoomService;

namespace DTOs.Room
{
	public class RoomDetailResponseDto : RoomListResponseDto
	{
		public double? Lenght { get; set; }
		public double? Width { get; set; }
		public string? Description { get; set; }
		public IList<string>? RoomImageUrls { get; set; }
		public string? RenterName { get; set; }
		public List<RoomServiceResponseDto> RoomServices { get; set; }
	}
}
