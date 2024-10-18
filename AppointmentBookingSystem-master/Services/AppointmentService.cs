using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppointmentBookingSystem.Models;
using AppointmentBookingSystem.Models.ViewModels;
using AppointmentBookingSystem.Repositories;

namespace AppointmentBookingSystem.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUserRepository _userRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository, IUserRepository userRepository)
        {
            _appointmentRepository = appointmentRepository;
            _userRepository = userRepository;
        }

        public async Task<string> CreateAppointmentAsync(Guid userId, AppointmentViewModel model)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return "User not found";
            }

            var appointment = new Appointment
            {
                AppointmentId = Guid.NewGuid(),
                Date = model.Date,
                Time = model.Time,
                Description = model.Description,
                UserId = userId
            };

            await _appointmentRepository.CreateAppointmentAsync(appointment);
            return "Appointment created successfully";
        }

        public async Task<IEnumerable<Appointment>> GetUserAppointmentsAsync(Guid userId)
        {
            return await _appointmentRepository.GetAppointmentsByUserIdAsync(userId);
        }

        public async Task<string> CancelAppointmentAsync(Guid appointmentId)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(appointmentId);
            if (appointment == null)
            {
                return "Appointment not found";
            }

            await _appointmentRepository.DeleteAppointmentAsync(appointmentId);
            return "Appointment canceled successfully";
        }
    }
}
