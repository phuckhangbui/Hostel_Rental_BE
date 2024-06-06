using DTOs.RoomService;
using System.ComponentModel.DataAnnotations;

namespace DTOs.Room
{
    public class CreateRoomRequestDto : UpdateRoomRequestDto
    {
        [Required]
        public List<CreateRoomServiceRequestDto> RoomServices { get; set; }
    }
}
