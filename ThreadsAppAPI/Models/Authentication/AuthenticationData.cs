namespace ThreadsAppAPI.Models.Authentication;

public class AutheticationData
{
    public string Token { get; set; }
    public string? ResetToken { get; set; }
    public string? Role { get; set; }
}