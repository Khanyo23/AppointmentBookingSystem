using System.ComponentModel.DataAnnotations;

namespace AppointmentBookingSystem.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password and confirmation do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
