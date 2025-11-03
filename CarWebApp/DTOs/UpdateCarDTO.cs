namespace CarWebApp.DTOs
{
    public class UpdateCarDTO : CreateCarDTO
    {
        public int Id { get; set; }
        public bool IsPublished { get; set; }
    }
}
