using BookLibrary.Infrastructure.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Infrastructure.AbstractServices
{
    public interface IUserService
    {
        Task<UserResponse> GetByIdAsync(int id);
        Task<IEnumerable<UserResponse>> BrowseAllAsync(string username = null);
        Task<UserResponse> CreateAsync(UserCreate categoryCreate);
        Task<UserResponse> UpdateAsync(int id, UserUpdate categoryUpdate);
        Task DeleteAsync(int id);
    }
}
