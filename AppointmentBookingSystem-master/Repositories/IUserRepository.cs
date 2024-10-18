using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppointmentBookingSystem.Models;

namespace AppointmentBookingSystem.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(Guid userId);
        Task<User> GetUserByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid userId);
    }
}
