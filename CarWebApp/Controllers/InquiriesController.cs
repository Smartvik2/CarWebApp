using CarWebApp.Interface;
using CarWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InquiriesController : ControllerBase
    {
        private readonly IInquiryService _inq;
        public InquiriesController(IInquiryService inq) => _inq = inq;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Inquiry dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var id = await _inq.CreateAsync(dto);
                return CreatedAtAction(nameof(Create), new { id }, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Create: {ex.Message}");
                return StatusCode(500, "An error occurred while creating the inquiry.");
            }
        }

    }
}
