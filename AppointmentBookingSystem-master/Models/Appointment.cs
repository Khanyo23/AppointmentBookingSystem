using System;

namespace AppointmentBookingSystem.Models
{
    public class Appointment
    {
        public Guid AppointmentId { get; set; }  
        public DateTime Date { get; set; }       
        public TimeSpan Time { get; set; }       
        public string Description { get; set; }  

        
        public Guid UserId { get; set; }         
        public User User { get; set; }           
    }
}
