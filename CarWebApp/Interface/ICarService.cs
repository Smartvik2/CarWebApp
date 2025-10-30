using CarWebApp.Models;

namespace CarWebApp.Interface
{
    public interface ICarService
    {
        Task<Car?> GetByIdAsync(int id);
        Task<IEnumerable<Car>> GetAllPublishedAsync();
        Task<int> CreateAsync(Car car);
        Task UpdateAsync(Car car);
        Task DeleteAsync(int id);
    }
}
