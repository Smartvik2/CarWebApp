using CarWebApp.Data;
using CarWebApp.Interface;
using CarWebApp.Models;

namespace CarWebApp.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICarService _carService;
        private readonly IImageService _imageService;

        public AdminService(ApplicationDbContext context, ICarService carService, IImageService imageService)
        {
            _context = context;
            _carService = carService;
            _imageService = imageService;
        }

        public bool ValidateAdminCredentials(string username, string password)
        {
            var admin = _context.AdminUsers.FirstOrDefault(a => a.UserName == username);
            if (admin == null)
                return false;

            return BCrypt.Net.BCrypt.Verify(password, admin.PasswordHash);
        }

        public async Task<(bool Success, string Message)> UploadCarAsync(Car model, List<IFormFile>? images)
        {
            model.IsPublished = true;
            model.CreatedAt = DateTime.UtcNow;

            var id = await _carService.CreateAsync(model);
            bool anySuccess = false;
            bool anyFailed = false;

            if (images?.Any() == true)
            {
                foreach (var file in images)
                {
                    try
                    {
                        var path = await _imageService.SaveCarImageAsync(id, file);
                        _context.CarImages.Add(new CarImage
                        {
                            CarId = id,
                            FilePath = path,
                            ContentType = file.ContentType
                        });
                        anySuccess = true;
                    }
                    catch
                    {
                        anyFailed = true;
                    }
                }

                await _context.SaveChangesAsync();
            }

            if (anySuccess && anyFailed)
                return (true, "Some images uploaded successfully, but a few failed.");
            else if (anySuccess)
                return (true, "Car and images uploaded successfully!");
            else if (images?.Any() != true)
                return (true, "Car uploaded but no images selected.");
            else
                return (false, "Image upload failed.");
        }
    }
}
