using AutoMapper;
using Loyalty.Data.Entities;
using Loyalty.Models.Dtos.Requests;

namespace Loyalty.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserRequest, User>();
        }
    }
}
