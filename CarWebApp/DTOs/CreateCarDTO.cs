namespace CarWebApp.DTOs
{
    public class CreateCarDTO
    {
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int? Year { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
    }
}
