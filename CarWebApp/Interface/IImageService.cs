namespace CarWebApp.Interface
{
    public interface IImageService
    {
        Task<string> SaveCarImageAsync(int carId, IFormFile file, CancellationToken ct = default);
        void DeleteImageFile(string filePath);
    }
}
