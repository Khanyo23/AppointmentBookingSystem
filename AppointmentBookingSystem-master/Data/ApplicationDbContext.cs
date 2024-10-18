using Microsoft.EntityFrameworkCore;
using AppointmentBookingSystem.Models;

namespace AppointmentBookingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet for Users and Appointments
        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        // Override OnModelCreating to define relationships and constraints
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API configuration for User and Appointment relationship
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.User)                     // One User has many Appointments
                .WithMany(u => u.Appointments)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);       // Cascade delete: delete user's appointments if user is deleted
        }
    }
}
