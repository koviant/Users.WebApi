using System;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

using Users.BLL.Exceptions;
using Users.DAL.Base;

namespace Users.DAL.EF
{
    public sealed class EFUserRepository : IUserRepository
    {
        private readonly UsersContext _context;

        public EFUserRepository(UsersContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task UpdateAddressAsync(int addressId, AddressDal addressRequest)
        {
            var addresses = await _context.Addresses.Where(a => a.Id == addressId).ToArrayAsync();
            if (addresses.Length == 0)
            {
                throw new ResourceNotFoundException();
            }

            var address = addresses.Single();
            _context.Entry(address).CurrentValues.SetValues(addressRequest);

            await _context.SaveChangesAsync();
        }

        public async Task<UserDal> GetUserByIdAsync(int userId)
        {
            var users = await _context.Users.Where(u => u.Id == userId).ToArrayAsync();
            if (users.Length == 0)
            {
                throw new ResourceNotFoundException();
            }

            return users.Single();
        }

        public async Task AddNewUserAsync(UserDal createRequest)
        {
            if (createRequest == null)
            {
                throw new ArgumentNullException(nameof(createRequest));
            }

            var users = await _context.Users.Where(u => u.LoginName == createRequest.LoginName).ToArrayAsync();
            if (users.Length > 0)
            {
                throw new ResourceHasConflictException("LoginName");
            }

            _context.Users.Add(createRequest);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(int userId, UserDal updateRequest)
        {
            // Checking if there are other users with the same LoginName.
            var users = await _context.Users.Where(u => u.LoginName == updateRequest.LoginName && u.Id != userId).ToArrayAsync();
            if (users.Length > 0)
            {
                throw new ResourceHasConflictException("LoginName");
            }

            users = await _context.Users.Where(u => u.Id == userId).ToArrayAsync();
            if (users.Length == 0)
            {
                throw new ResourceNotFoundException();
            }

            var user = users.Single();
            updateRequest.Id = userId;
            _context.Entry(user).CurrentValues.SetValues(updateRequest);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var users = await _context.Users.Where(u => u.Id == userId).ToArrayAsync();
            if (users.Length == 0)
            {
                throw new ResourceNotFoundException();
            }

            var user = users.Single();
            _context.Entry(user).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public IQueryable<UserDal> GetAllUsers()
        {
            return _context.Users.Include("Addresses").AsQueryable();
        }
    }
}
