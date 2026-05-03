namespace eLearning.Models.Requests
{
    public class UserLogin
    {
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? Username { get; set; }
        public required string PasswordHash { get; set; }
    }
}
