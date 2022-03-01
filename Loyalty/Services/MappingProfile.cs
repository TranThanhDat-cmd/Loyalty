using AutoMapper;
using Loyalty.Data.Entities;
using Loyalty.Models.Dtos.Requests.Auth;
using Loyalty.Models.Dtos.Requests.Role;
using Loyalty.Models.Dtos.Requests.User;
using Loyalty.Models.Dtos.Responses.Role;
using Loyalty.Models.Dtos.Responses.User;

namespace Loyalty.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserRequest, User>();
            CreateMap<User, UserReponse>();
            CreateMap<AddRoleRequest, Role>();
            CreateMap<Role, RoleReponse>();
            CreateMap<UpdateRoleRequest, Role>();
            CreateMap<UserRegisterRequest, User>();
        }
    }
}
