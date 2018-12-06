using AutoMapper;

using Users.BLL.Models;
using Users.DAL.Base;

namespace Users.BLL.Mappings
{
    public class AddressMappingProfile : Profile
    {
        public AddressMappingProfile()
        {
            CreateMap<AddressDal, Address>();
            CreateMap<AddressRequest, AddressDal>();
            CreateMap<AddressDal, AddressResponse>();
        }
    }
}