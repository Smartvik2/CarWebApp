using CarWebApp.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarWebApp.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var cars = await _carService.GetAllPublishedAsync();
                return View(cars);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Index error: {ex.Message}");
                return RedirectToAction("Error", new { message = "An error occurred while loading cars." });
            }
        }

        [HttpGet]
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
                Console.WriteLine($"Details error: {ex.Message}");
                return RedirectToAction("Error", new { message = "An error occurred while loading car details." });
            }
        }

    }
}
