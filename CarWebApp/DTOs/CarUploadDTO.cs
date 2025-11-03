using CarWebApp.Models;

namespace CarWebApp.DTOs
{
    public class CarUploadDTO
    {
        public Car Car { get; set; } = default!;
        public List<IFormFile>? Images { get; set; }
    }
}
