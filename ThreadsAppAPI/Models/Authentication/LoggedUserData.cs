namespace ThreadsAppAPI.Models.Authentication;

public class LoggedUserData
{
    public int UserId { get; set; }
    public string UserAlias { get; set; }
    public string? UserName { get; set; }
    public string? UserSurname { get; set; }
    public string Email { get; set; }
    public AutheticationData? Auth { get; set; }
}