namespace ThreadsAppAPI.Models.Authentication;

public class UserLogin
{
    public string? UserAlias { get; set; }
    public string Password { get; set; }
    public string? Email { get; set; }
}