using AutoMapper;
using BusinessObject.Enum;
using BusinessObject.Models;
using DTOs.Account;
using DTOs.Complain;
using DTOs.Hostel;
using DTOs.Membership;
using DTOs.Room;

namespace Repository.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        //CreateMap<SendMessageDto, Message>();
        CreateMap<Account, AccountDto>().ReverseMap();
        CreateMap<Account, AccountViewDetail>().ReverseMap();
        CreateMap<Room, RoomOfHostelAdminView>().ReverseMap();
        CreateMap<MemberShip, GetMemberShipDto>().ReverseMap();
        CreateMap<Room, RoomListResponseDto>()
            .ForMember(dest => dest.RoomThumbnail, opt => opt.MapFrom(src => src.RoomImages.FirstOrDefault().RoomUrl));
        CreateMap<Room, RoomDetailResponseDto>()
            .ForMember(dest => dest.RoomThumbnail, opt => opt.MapFrom(src => src.RoomImages.FirstOrDefault().RoomUrl))
            .ForMember(dest => dest.RoomImageUrls, opt => opt.Ignore());
        CreateMap<Hostel, HostelResponseDto>()
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.OwnerAccount != null ? src.OwnerAccount.Name : string.Empty))
            .ForMember(dest => dest.NumOfAvailableRoom, opt => opt.MapFrom(src => src.Rooms != null ? src.Rooms.Count(r => r.Status == (int)RoomEnum.Available) : 0))
			.ForMember(dest => dest.NumOfTotalRoom, opt => opt.MapFrom(src => src.Rooms != null ? src.Rooms.Count() : 0));
        CreateMap<Hostel, HostelsAdminView>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.OwnerAccount != null ? src.OwnerAccount.Name : string.Empty))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.OwnerAccount != null ? src.OwnerAccount.Email : string.Empty));
        CreateMap<Hostel, HostelDetailAdminView>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.OwnerAccount != null ? src.OwnerAccount.Name : string.Empty))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.OwnerAccount != null ? src.OwnerAccount.Email : string.Empty))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.OwnerAccount != null ? src.OwnerAccount.Phone : string.Empty))
            .ForMember(dest => dest.NumberOfRoom, opt => opt.MapFrom(src => src.Rooms != null ? src.Rooms.Count() : 0));
        CreateMap<Complain, CreateComplainDto>().ReverseMap();
        CreateMap<Complain, DisplayComplainDto>()
            .ForMember(dest => dest.AccountComplainName, opt => opt.MapFrom(src => src.ComplainAccount != null ? src.ComplainAccount.Name : string.Empty))
            .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room != null ? src.Room.RoomName : string.Empty));


    }
}