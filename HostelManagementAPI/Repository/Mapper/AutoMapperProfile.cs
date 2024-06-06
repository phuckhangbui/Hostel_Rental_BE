﻿using AutoMapper;
using BusinessObject.Models;
using DTOs.Account;
using DTOs.Complain;
using DTOs.Contract;
using DTOs.Enum;
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
        CreateMap<Account, AccountDto>();
        //.ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.AccountID));
        CreateMap<AccountDto, Account>();
        //.ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.AccountID));
        CreateMap<Account, AccountViewDetail>().ReverseMap();
        CreateMap<AccountViewDetail, AccountDto>().ReverseMap();
        CreateMap<Account, AccountMemberShipInformationDtos>().ReverseMap();
        CreateMap<AccountDto, AccountLoginDto>().ReverseMap();
        CreateMap<Room, RoomOfHostelAdminView>().ReverseMap();
        CreateMap<MemberShip, GetMemberShipDto>().ReverseMap();
        CreateMap<MemberShip, CreateMemberShipDto>().ReverseMap();
        CreateMap<TypeService, CreateTypeServiceDto>().ReverseMap();
        CreateMap<TypeService, UpdateTypeServiceDto>().ReverseMap();
        CreateMap<TypeService, ViewAllTypeServiceDto>().ReverseMap();
        CreateMap<Account, ViewMemberShipDto>().ReverseMap();
        CreateMap<Room, RoomListResponseDto>()
            .ForMember(dest => dest.RoomThumbnail, opt => opt.MapFrom(src => src.RoomImages.FirstOrDefault().RoomUrl));
        CreateMap<MemberShipRegisterTransaction, ViewTransactionMembership>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.OwnerAccount.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.OwnerAccount.Email));
        //CreateMap<Room, RoomDetailResponseDto>()
        //    .ForMember(dest => dest.RoomThumbnail, opt => opt.MapFrom(src => src.RoomImages.FirstOrDefault().RoomUrl))
        //    .ForMember(dest => dest.RoomImageUrls, opt => opt.Ignore())
        //    .ForMember(dest => dest.RoomServices, opt => opt.MapFrom(src => src.RoomServices.Select(rs => new RoomServiceResponseDto
        //    {
        //        ServiceID = rs.Service.ServiceID,
        //        ServiceName = rs.Service.ServiceName,
        //        ServicePrice = rs.Service.ServicePrice,
        //        Status = rs.Status,
        //        TypeServiceID = rs.Service.TypeServiceID
        //    })));
        CreateMap<Hostel, HostelResponseDto>()
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.OwnerAccount != null ? src.OwnerAccount.Name : string.Empty))
            .ForMember(dest => dest.NumOfAvailableRoom, opt => opt.MapFrom(src => src.Rooms != null ? src.Rooms.Count(r => r.Status == (int)RoomEnum.Available) : 0))
            .ForMember(dest => dest.NumOfTotalRoom, opt => opt.MapFrom(src => src.Rooms != null ? src.Rooms.Count() : 0))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images != null ? src.Images.Select(img => img.ImageURL).ToList() : new List<string>()));
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
        CreateMap<MemberShipRegisterTransaction, ViewHistoryMemberShipDtos>()
           .ForMember(dest => dest.MembershipName, opt => opt.MapFrom(src => src.MemberShip != null ? src.MemberShip.MemberShipName : string.Empty))
           .ForMember(dest => dest.CapacityHostel, opt => opt.MapFrom(src => src.MemberShip != null ? src.MemberShip.CapacityHostel : 0))
           .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.MemberShip != null ? src.MemberShip.Month : 0));
        CreateMap<Contract, UpdateContractDto>();
        CreateMap<Contract, GetContractDto>()
            .ForMember(dest => dest.OwnerAccountId, opt => opt.MapFrom(src => src.OwnerAccount.AccountID));
        CreateMap<Account, CustomerViewAccount>().ReverseMap();
        //CreateMap<Contract, GetContractDto>()
        //    .ForMember(dest => dest.ContractDetails, opt => opt.MapFrom(src => src.ContractDetails));

        // CreateMap<ContractDetail, GetContractDetailsDto>()
        //     .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.ServiceName))
        //     .ForMember(dest => dest.ServicePrice, opt => opt.MapFrom(src => src.Service.ServicePrice));
        // CreateMap<RoomService, RoomServiceResponseDto>()
        //     .ForMember(dest => dest.ServiceID, opt => opt.MapFrom(src => src.Service.ServiceID))
        //     .ForMember(dest => dest.TypeServiceID, opt => opt.MapFrom(src => src.Service.TypeServiceID))
        //     .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.ServiceName))
        //     .ForMember(dest => dest.ServicePrice, opt => opt.MapFrom(src => src.Service.ServicePrice))
        //     .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

    }
}