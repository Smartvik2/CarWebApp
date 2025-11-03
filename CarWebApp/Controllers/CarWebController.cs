using CarWebApp.Interface;
using CarWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarWebController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarWebController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("published")]
        public async Task<IActionResult> GetAllPublishedAsync()
        {
            try
            {
                var cars = await _carService.GetAllPublishedAsync();
                return Ok(cars);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving cars.", error = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var car = await _carService.GetByIdAsync(id);
                if (car == null)
                    return NotFound(new { message = $"Car with ID {id} not found." });

                return Ok(car);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the car.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Car car)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var id = await _carService.CreateAsync(car);
                return CreatedAtAction(nameof(GetByIdAsync), new { id }, car);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the car.", error = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Car car)
        {
            try
            {
                if (id != car.Id)
                    return BadRequest(new { message = "Car ID mismatch." });

                var existingCar = await _carService.GetByIdAsync(id);
                if (existingCar == null)
                    return NotFound(new { message = $"Car with ID {id} not found." });

                await _carService.UpdateAsync(car);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the car.", error = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var existingCar = await _carService.GetByIdAsync(id);
                if (existingCar == null)
                    return NotFound(new { message = $"Car with ID {id} not found." });

                await _carService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the car.", error = ex.Message });
            }
        }
    }
}
