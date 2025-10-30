using CarWebApp.Data;
using CarWebApp.Models;
using CarWebApp.Interface;

namespace CarWebApp.Services
{
    public class InquiryService : IInquiryService
    {
        private readonly ApplicationDbContext _db;
        public InquiryService(ApplicationDbContext db) => _db = db;
        public async Task<int> CreateAsync(Inquiry inquiry)
        {
            _db.Inquiries.Add(inquiry);
            await _db.SaveChangesAsync();
            return inquiry.Id;
        }
    }
}
