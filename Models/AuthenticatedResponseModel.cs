namespace Todo_Assignment.API.Models
{
    public class AuthenticatedResponseModel
    {
        public string? AuthToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
