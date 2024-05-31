using AutoMapper;
using DTOs.Enum;
using BusinessObject.Models;
using DTOs.Account;
using DTOs.Complain;
using DTOs.Contract;
using DTOs.Hostel;
using DTOs.Membership;
using DTOs.MemberShipRegisterTransaction;
using DTOs.Room;
using DTOs.TypeService;

namespace Repository.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        //CreateMap<SendMessageDto, Message>();
        CreateMap<Account, AccountDto>().ReverseMap();
        CreateMap<Account, AccountViewDetail>().ReverseMap();
        CreateMap<AccountViewDetail, AccountDto>().ReverseMap();
        CreateMap<AccountDto, AccountLoginDto>().ReverseMap();
        CreateMap<Room, RoomOfHostelAdminView>().ReverseMap();
        CreateMap<MemberShip, GetMemberShipDto>().ReverseMap();
        CreateMap<MemberShip, CreateMemberShipDto>().ReverseMap();
        CreateMap<TypeService, CreateTypeServiceDto>().ReverseMap();
        CreateMap<TypeService, UpdateTypeServiceDto>().ReverseMap();
        CreateMap<TypeService, ViewAllTypeServiceDto>().ReverseMap();
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
        CreateMap<MemberShipRegisterTransaction, ViewMemberShipDetailDto>()
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.OwnerAccount != null ? src.OwnerAccount.Name : string.Empty))
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.OwnerAccount != null ? src.OwnerAccount.Email : string.Empty))
           .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.OwnerAccount != null ? src.OwnerAccount.Address : string.Empty))
           .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.OwnerAccount != null ? src.OwnerAccount.Phone : string.Empty))
           .ForMember(dest => dest.MembershipName, opt => opt.MapFrom(src => src.MemberShip != null ? src.MemberShip.MemberShipName : string.Empty))
           .ForMember(dest => dest.CapacityHostel, opt => opt.MapFrom(src => src.MemberShip != null ? src.MemberShip.CapacityHostel : 0))
           .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.MemberShip != null ? src.MemberShip.Month : 0));
        CreateMap<Contract, UpdateContractDto>();
        CreateMap<Contract, GetContractDto>()
            .ForMember(dest => dest.OwnerAccountId, opt => opt.MapFrom(src => src.OwnerAccount.AccountID));
        CreateMap<Account, CustomerViewAccount>().ReverseMap();

    }
}