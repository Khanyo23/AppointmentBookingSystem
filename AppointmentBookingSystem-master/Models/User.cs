using System;
using System.Collections.Generic;

namespace AppointmentBookingSystem.Models
{
    public class User
    {
        public Guid UserId { get; set; }         
        public string Email { get; set; }        
        public string PasswordHash { get; set; } 

        public ICollection<Appointment> Appointments { get; set; }
    }
}
