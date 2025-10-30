using CarWebApp.Interface;
using CarWebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarWebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login (GET) error: {ex.Message}");
                return RedirectToAction("Error", new { message = "An error occurred while loading the login page." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                if (!_adminService.ValidateAdminCredentials(username, password))
                {
                    ViewBag.Error = "Invalid username or password.";
                    return View();
                }

                var claims = new List<Claim> { new(ClaimTypes.Name, username) };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Upload");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login (POST) error: {ex.Message}");
                ViewBag.Error = "An unexpected error occurred while logging in.";
                return View();
            }
        }


        [Authorize]
        [HttpGet]
        public IActionResult Upload() => View();

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Upload(Car car, List<IFormFile>? images)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var formPairs = Request.Form.Keys
                        .Select(k => $"{k}='{Request.Form[k]}'")
                        .ToList();

                    ViewBag.DebugForm = string.Join(" | ", formPairs);

                    var stateDebug = ModelState
                        .Select(kvp => new {
                            Key = kvp.Key,
                            Attempted = kvp.Value.AttemptedValue,
                            Errors = string.Join(";", kvp.Value.Errors.Select(e => e.ErrorMessage))
                        })
                        .Select(x => $"{x.Key}: attempted='{x.Attempted}' errors='{x.Errors}'")
                        .ToList();

                    ViewBag.DebugModelState = string.Join(" | ", stateDebug);

                }

                var (success, message) = await _adminService.UploadCarAsync(car, images);

                if (success)
                    ViewBag.Success = message;
                else
                    ViewBag.Error = message;

                ModelState.Clear();
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(car);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Logout error: {ex.Message}");
                return RedirectToAction("Error", new { message = "An error occurred while logging out." });
            }
        }

    }
}
