using AutoMapper;
using BusinessObject.Models;
using DTOs.Account;
using DTOs.Room;

namespace Repository.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<SendMessageDto, Message>();
            CreateMap<Account, AccountDto>().ReverseMap();
			CreateMap<Room, RoomListResponseDto>()
			    .ForMember(dest => dest.RoomThumbnail, opt => opt.MapFrom(src => src.RoomImages.FirstOrDefault().RoomUrl));
			CreateMap<Room, RoomDetailResponseDto>()
			    .ForMember(dest => dest.RoomThumbnail, opt => opt.MapFrom(src => src.RoomImages.FirstOrDefault().RoomUrl))
			    .ForMember(dest => dest.RoomImageUrls, opt => opt.Ignore());
		}
    }
}
