using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AppointmentBookingSystem.Models;

namespace AppointmentBookingSystem.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any users already in the database.
                if (context.Users.Any())
                {
                    return;   // Database has been seeded already
                }

                // Seed Users
                var user = new User
                {
                    UserId = Guid.NewGuid(),
                    Email = "testuser@example.com",
                    PasswordHash = "testpasswordhash" // You should hash this in a real implementation
                };

                context.Users.Add(user);
                context.SaveChanges();

                // Seed Appointments for the user
                context.Appointments.AddRange(
                    new Appointment
                    {
                        AppointmentId = Guid.NewGuid(),
                        Date = DateTime.Now.AddDays(1),
                        Time = TimeSpan.FromHours(14),
                        Description = "Test Appointment 1",
                        UserId = user.UserId
                    },
                    new Appointment
                    {
                        AppointmentId = Guid.NewGuid(),
                        Date = DateTime.Now.AddDays(2),
                        Time = TimeSpan.FromHours(16),
                        Description = "Test Appointment 2",
                        UserId = user.UserId
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
