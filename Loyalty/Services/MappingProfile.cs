using AutoMapper;
using Loyalty.Data.Entities;
using Loyalty.Models.Dtos.Requests;
using Loyalty.Models.Dtos.Responses;

namespace Loyalty.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserRequest, User>();
            CreateMap<User, GetUserReponse>();
            CreateMap<AddRoleRequest, Role>();

            CreateMap<Role, GetRoleReponse>();
            CreateMap<UpdateRoleRequest, Role>();


        }
    }
}
