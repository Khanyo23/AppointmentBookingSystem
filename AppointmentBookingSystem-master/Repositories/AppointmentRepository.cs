using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentBookingSystem.Data;
using AppointmentBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBookingSystem.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Appointment> GetAppointmentByIdAsync(Guid appointmentId)
        {
            return await _context.Appointments
                                 .Include(a => a.User)
                                 .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByUserIdAsync(Guid userId)
        {
            return await _context.Appointments
                                 .Where(a => a.UserId == userId)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _context.Appointments.ToListAsync();
        }

        public async Task CreateAppointmentAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAppointmentAsync(Guid appointmentId)
        {
            var appointment = await GetAppointmentByIdAsync(appointmentId);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
