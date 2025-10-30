using CarWebApp.Models;

namespace CarWebApp.Interface
{
    public interface IAdminService
    {
        bool ValidateAdminCredentials(string username, string password);

        Task<(bool Success, string Message)> UploadCarAsync(Car model, List<IFormFile>? images);
    }
}
