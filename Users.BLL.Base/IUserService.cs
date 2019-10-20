using System.Collections.Generic;
using System.Threading.Tasks;

using Users.BLL.Models;

namespace Users.BLL.Base
{
    public interface IUserService
    {
        Task<UserResponse> CreateUserAsync(UserRequest createRequest);

        Task<UserResponse> GetUserById(int userId);

        Task<List<UserResponse>> GetAllUsersAsync();

        Task UpdateUserAsync(int userId, UserRequest updateRequest);

        Task<List<AddressResponse>> GetUserAddressesAsync(int userId);

        Task DeleteUserByIdAsync(int userId);

        Task UpdateUserLastNameAsync(int userId, string lastName);

        Task<AddressResponse> AddNewAddressToUserAsync(int userId, AddressRequest addressRequest);

        Task<AddressResponse> GetUsersAddressByIdAsync(int userId, int addressId);

        Task UpdateUserAddressAsync(int userId, int addressId, AddressRequest addressRequest);
    }
}