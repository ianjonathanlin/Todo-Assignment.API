namespace Todo_Assignment.API.Models
{
    public class AuthenticatedResponseModel
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
