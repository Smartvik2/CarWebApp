using CarWebApp.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarService _carService;

        public HomeController(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var cars = await _carService.GetAllPublishedAsync();
                return View(cars);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Index: {ex.Message}");
                return RedirectToAction("Error", new { message = "An error occurred while loading cars." });
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var car = await _carService.GetByIdAsync(id);
                if (car == null) return NotFound();
                return View(car);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Details: {ex.Message}");
                return RedirectToAction("Error", new { message = "An error occurred while loading car details." });
            }
        }


    }
}
