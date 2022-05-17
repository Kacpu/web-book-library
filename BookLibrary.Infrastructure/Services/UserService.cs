using BookLibrary.Core.Domain;
using BookLibrary.Core.Repositories;
using BookLibrary.Infrastructure.AbstractServices;
using BookLibrary.Infrastructure.DTO;
using BookLibrary.Infrastructure.DTO.UserDTO;
using BookLibrary.Infrastructure.Exceptions;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userReposiotry)
        {
            _userRepository = userReposiotry;
        }

        public async Task<UserResponse> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
            {
                throw new NotFoundException("user not found");
            }

            return await Task.FromResult(user.ToResponse());
        }

        public async Task<IEnumerable<UserResponse>> BrowseAllAsync(string username)
        {
            Expression<Func<User, bool>> filter = PredicateBuilder.New<User>(true);

            if (!string.IsNullOrEmpty(username))
            {
                filter = filter.And(u => u.Username.Contains(username));
            }

            var us = await _userRepository.BrowseAllAsync(filter);
            return await Task.FromResult(us.Select(x => x.ToResponse()));
        }

        public async Task<UserResponse> CreateAsync(UserCreate userCreate)
        {
            var checkU = await _userRepository
                .BrowseAllAsync(x => x.Username == userCreate.Username);

            if (checkU.Any())
            {
                throw new BadRequestException("user already exists");
            }

            var u = await _userRepository.CreateAsync(userCreate.ToDomain());
            return await Task.FromResult(u.ToResponse());
        }

        public async Task<UserResponse> UpdateAsync(int id, UserUpdate userUpdate)
        {
            var u = await _userRepository.GetByIdAsync(id);

            if (u is null)
            {
                throw new NotFoundException("user not found");
            }

            if (!string.IsNullOrEmpty(userUpdate.Username))
            {
                var checkU = await _userRepository
                    .BrowseAllAsync(x => x.Username == userUpdate.Username);

                if (checkU.Any())
                {
                    throw new BadRequestException("user already exists");
                }

                u.Username = userUpdate.Username;
                u = await _userRepository.UpdateAsync(u);
            }

            return await Task.FromResult(u.ToResponse());
        }

        public async Task DeleteAsync(int id)
        {
            var u = await _userRepository.GetByIdAsync(id);

            if (u is null)
            {
                throw new NotFoundException("user not found");
            }

            await _userRepository.DeleteAsync(id);
        }
    }
}
