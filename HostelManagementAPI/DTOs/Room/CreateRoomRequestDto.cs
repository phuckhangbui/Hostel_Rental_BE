using DTOs.RoomService;
using System.ComponentModel.DataAnnotations;

namespace DTOs.Room
{
    public class CreateRoomRequestDto : RoomRequestDto
    {
        [Required]
        public List<CreateRoomServiceRequestDto> RoomServices { get; set; }
    }
}
