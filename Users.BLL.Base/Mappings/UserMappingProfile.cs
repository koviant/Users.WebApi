using AutoMapper;

using Users.BLL.Models;
using Users.DAL.Base;

namespace Users.BLL.Mappings 
{

    public sealed class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserDal, User>();
            CreateMap<UserRequest, UserDal>();
        }
    }
}
