using CarWebApp.Models;

namespace CarWebApp.Interface
{
    public interface IInquiryService
    {
        Task<int> CreateAsync(Inquiry inquiry);
    }
}
