using System.Threading.Tasks;
using AppointmentBookingSystem.Models.ViewModels;

namespace AppointmentBookingSystem.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterViewModel model);
        Task<string> LoginAsync(LoginViewModel model);
    }
}
