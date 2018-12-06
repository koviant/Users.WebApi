using System.Threading.Tasks;

namespace Users.DAL.Base
{
    using System.Linq;

    public interface IUserRepository
    {
        Task AddNewUserAsync(UserDal createRequest);

        Task UpdateUserAsync(int userId, UserDal updateRequest);

        Task UpdateAddressAsync(int addressId, AddressDal addressRequest);

        Task<UserDal> GetUserByIdAsync(int userId);

        Task DeleteUserAsync(int userId);

        IQueryable<UserDal> GetAllUsers();
    }
}
