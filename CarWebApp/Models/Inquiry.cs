namespace CarWebApp.Models
{
    public class Inquiry
    {
        public int Id { get; set; }
        public int? CarId { get; set; } // optional
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string Message { get; set; } = null!;
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}
