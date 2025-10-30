namespace CarWebApp.Models
{
    public class CarImage
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string FilePath { get; set; } = null!;
        public string? ContentType { get; set; }
        public Car? Car { get; set; }
    }
}
