using System;
using System.Threading.Tasks;
using AppointmentBookingSystem.Models.ViewModels;
using AppointmentBookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingSystem.Controllers
{
    [Authorize] // Ensure that the user is authenticated
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        // GET: /appointment
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Assuming the user's ID is stored in the JWT token (retrieved from claims)
            var userId = Guid.Parse(User.FindFirst("sub")?.Value);
            var appointments = await _appointmentService.GetUserAppointmentsAsync(userId);
            return View(appointments);
        }

        // GET: /appointment/create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /appointment/create
        [HttpPost]
        public async Task<IActionResult> Create(AppointmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = Guid.Parse(User.FindFirst("sub")?.Value);
            var result = await _appointmentService.CreateAppointmentAsync(userId, model);

            if (result != "Appointment created successfully")
            {
                ModelState.AddModelError(string.Empty, result);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        // POST: /appointment/cancel/{id}
        [HttpPost]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var result = await _appointmentService.CancelAppointmentAsync(id);
            if (result != "Appointment canceled successfully")
            {
                // Handle error case if needed
            }

            return RedirectToAction("Index");
        }
    }
}
