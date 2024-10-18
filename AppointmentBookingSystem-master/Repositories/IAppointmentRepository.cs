using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppointmentBookingSystem.Models;

namespace AppointmentBookingSystem.Repositories
{
    public interface IAppointmentRepository
    {
        Task<Appointment> GetAppointmentByIdAsync(Guid appointmentId);
        Task<IEnumerable<Appointment>> GetAppointmentsByUserIdAsync(Guid userId);
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task CreateAppointmentAsync(Appointment appointment);
        Task UpdateAppointmentAsync(Appointment appointment);
        Task DeleteAppointmentAsync(Guid appointmentId);
    }
}
