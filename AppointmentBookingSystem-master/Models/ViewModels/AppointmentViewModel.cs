using System;
using System.ComponentModel.DataAnnotations;

namespace AppointmentBookingSystem.Models.ViewModels
{
    public class AppointmentViewModel
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Time { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Description cannot be longer than 250 characters.")]
        public string Description { get; set; }
    }
}
