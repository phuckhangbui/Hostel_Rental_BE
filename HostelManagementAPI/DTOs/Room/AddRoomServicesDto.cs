using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Room
{
    public class AddRoomServicesDto
    {
        public int RoomId { get; set; }
        public List<int> ServiceId { get; set; }
        public int Status { get; set; } 
    }
}
