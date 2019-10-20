using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Users.BLL.Base;
using Users.BLL.Models;
using Users.BLL.Exceptions;
using Users.DAL.Base;

namespace Users.BLL
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<UserResponse>> GetAllUsersAsync()
        {
            var usersDb = await _repository.GetAllUsers().ToArrayAsync();
            var users = usersDb.Select(Mapper.Map<UserResponse>).ToList();

            return users;
        }

        public async Task<UserResponse> GetUserById(int userId)
        {
            var userDal = await _repository.GetUserByIdAsync(userId);
            var user = Mapper.Map<UserResponse>(userDal);

            return user;
        }

        public async Task<UserResponse> CreateUserAsync(UserRequest createRequest)
        {
            if (createRequest == null)
            {
                throw new ArgumentNullException(nameof(createRequest));
            }

            var userDal = Mapper.Map<UserDal>(createRequest);
            await _repository.AddNewUserAsync(userDal);

            return Mapper.Map<UserResponse>(userDal);
        }

        public async Task UpdateUserAsync(int userId, UserRequest updateRequest)
        {
            if (updateRequest == null)
            {
                throw new ArgumentNullException(nameof(updateRequest));
            }

            var userDal = Mapper.Map<UserDal>(updateRequest);
            await _repository.UpdateUserAsync(userId, userDal);
        }

        public async Task<List<AddressResponse>> GetUserAddressesAsync(int userId)
        {
            var userDal = await _repository.GetUserByIdAsync(userId);
            var user = Mapper.Map<User>(userDal);

            return user.Addresses.Select(Mapper.Map<AddressResponse>).ToList();
        }

        public async Task DeleteUserByIdAsync(int userId)
        {
            await _repository.DeleteUserAsync(userId);
        }

        public async Task UpdateUserLastNameAsync(int userId, string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException($"{nameof(lastName)} must not be null or empty.");
            }

            var user = await _repository.GetUserByIdAsync(userId);
            user.LastName = lastName;
            await _repository.UpdateUserAsync(userId, user);
        }

        public async Task<AddressResponse> AddNewAddressToUserAsync(int userId, AddressRequest addressRequest)
        {
            if (addressRequest == null)
            {
                throw new ArgumentNullException(nameof(addressRequest));
            }

            var user = await _repository.GetUserByIdAsync(userId);
            if (user.Addresses.Any(a => a.Description == addressRequest.Description))
            {
                throw new ResourceHasConflictException();
            }

            var address = Mapper.Map<AddressDal>(addressRequest);
            user.Addresses.Add(address);
            await _repository.UpdateUserAsync(userId, user);

            return Mapper.Map<AddressResponse>(address);
        }

        public async Task<AddressResponse> GetUsersAddressByIdAsync(int userId, int addressId)
        {
            var userDal = await _repository.GetUserByIdAsync(userId);
            var addressesDal = userDal.Addresses.Where(a => a.Id == addressId).ToArray();
            if (addressesDal.Length == 0)
            {
                throw new ResourceNotFoundException();
            }

            var addressDal = addressesDal.Single();
            var addressResponse = Mapper.Map<AddressResponse>(addressDal);

            return addressResponse;
        }

        public async Task UpdateUserAddressAsync(int userId, int addressId, AddressRequest addressRequest)
        {
            if (addressRequest == null)
            {
                throw new ArgumentNullException(nameof(addressRequest));
            }

            var userDal = await _repository.GetUserByIdAsync(userId);
            if (userDal.Addresses.All(a => a.Id != addressId))
            {
                throw new ResourceNotFoundException();
            }

            if (userDal.Addresses.Any(a => a.Description == addressRequest.Description))
            {
                throw new ResourceHasConflictException("description");
            }

            var addressDal = Mapper.Map<AddressDal>(addressRequest);
            addressDal.Id = addressId;
            addressDal.User = userDal;

            await _repository.UpdateAddressAsync(addressId, addressDal);
        }
    }
}
