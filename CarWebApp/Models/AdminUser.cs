namespace CarWebApp.Models
{
    public class AdminUser
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
    }
}
