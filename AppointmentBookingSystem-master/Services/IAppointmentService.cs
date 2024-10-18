using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppointmentBookingSystem.Models;
using AppointmentBookingSystem.Models.ViewModels;

namespace AppointmentBookingSystem.Services
{
    public interface IAppointmentService
    {
        Task<string> CreateAppointmentAsync(Guid userId, AppointmentViewModel model);
        Task<IEnumerable<Appointment>> GetUserAppointmentsAsync(Guid userId);
        Task<string> CancelAppointmentAsync(Guid appointmentId);
    }
}
