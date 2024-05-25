using AutoMapper;
using BusinessObject.Models;
using DTOs;

namespace Repository.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        //CreateMap<SendMessageDto, Message>();
        CreateMap<Account, AccountDto>().ReverseMap();
    }
}