using CarWebApp.Interface;
using CarWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarWebApp.Controllers
{
    public class InquiriesWebController : Controller
    {
        private readonly IInquiryService _inq;

        public InquiriesWebController(IInquiryService inq) => _inq = inq;

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GET Create: {ex.Message}");
                return RedirectToAction("Error", new { message = "An error occurred while loading the form." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Inquiry model)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);

                await _inq.CreateAsync(model);
                return RedirectToAction("Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in POST Create: {ex.Message}");
                return RedirectToAction("Error", new { message = "An error occurred while submitting your inquiry." });
            }
        }

        public IActionResult Success()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Success: {ex.Message}");
                return RedirectToAction("Error", new { message = "An error occurred while loading the success page." });
            }
        }

    }
}
