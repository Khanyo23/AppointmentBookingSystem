using System.Threading.Tasks;
using AppointmentBookingSystem.Models.ViewModels;
using AppointmentBookingSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingSystem.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // GET: /auth/register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /auth/register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _authService.RegisterAsync(model);
            if (result != "Registration successful")
            {
                ModelState.AddModelError(string.Empty, result);
                return View(model);
            }

            return RedirectToAction("Login");
        }

        // GET: /auth/login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /auth/login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var token = await _authService.LoginAsync(model);
            if (token == "Invalid login attempt")
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password");
                return View(model);
            }

            // Save the JWT token in the session or local storage (handled in frontend).
            HttpContext.Session.SetString("JWToken", token);

            return RedirectToAction("Index", "Home"); // Redirect to home or dashboard after login
        }

        // GET: /auth/logout
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWToken"); // Remove the token from the session
            return RedirectToAction("Login");
        }
    }
}
