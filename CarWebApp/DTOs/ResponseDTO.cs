namespace CarWebApp.DTOs
{
    public class ResponseDTO
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public string? Error { get; set; }
        public object? Data { get; set; }
    }
}
