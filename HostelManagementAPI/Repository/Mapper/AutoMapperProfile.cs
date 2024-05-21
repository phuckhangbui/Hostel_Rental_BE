using AutoMapper;
using BusinessObject.Models;

namespace Repository.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<SendMessageDto, Message>();
            CreateMap<Account, DTOs.AccountDto>().ReverseMap();
        }
    }
}
