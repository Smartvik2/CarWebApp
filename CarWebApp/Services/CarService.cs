using CarWebApp.Data;
using CarWebApp.Models;
using CarWebApp.Interface;
using Microsoft.EntityFrameworkCore;


namespace CarWebApp.Services
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _db;
        public CarService(ApplicationDbContext db) => _db = db;

        public async Task<int> CreateAsync(Car car)
        {
            _db.Cars.Add(car);
            await _db.SaveChangesAsync();
            return car.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var c = await _db.Cars.Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == id);
            if (c == null) return;
            _db.CarImages.RemoveRange(c.Images);
            _db.Cars.Remove(c);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Car>> GetAllPublishedAsync()
        {
            return await _db.Cars.Where(c => c.IsPublished).Include(c => c.Images).OrderByDescending(c => c.CreatedAt).ToListAsync();
        }

        public async Task<Car?> GetByIdAsync(int id)
        {
            return await _db.Cars.Include(c => c.Images).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(Car car)
        {
            _db.Cars.Update(car);
            await _db.SaveChangesAsync();
        }
    }
}
