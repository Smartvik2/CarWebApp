using CarWebApp.Interface;

namespace CarWebApp.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _env;

        public ImageService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> SaveCarImageAsync(int carId, IFormFile file, CancellationToken ct = default)
        {
            // Save all images inside wwwroot/uploads/
            var uploads = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploads);

            var ext = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(uploads, fileName);

            await using var fs = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fs, ct);

            return $"/uploads/{fileName}";
        }

        public void DeleteImageFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return;

            var trimmed = filePath.TrimStart('/');
            var fullPath = Path.Combine(_env.WebRootPath, trimmed.Replace('/', Path.DirectorySeparatorChar));

            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }
}
