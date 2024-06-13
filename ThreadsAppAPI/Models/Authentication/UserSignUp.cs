namespace ThreadsAppAPI.Models.Authentication;

public class UserSignUp
{
    public string UserAlias { get; set; }
    public string UserName { get; set; }
    public string UserSurname { get; set; }
    public string Password1 { get; set; }
    public string Password2 { get; set; }
    public string Email { get; set; }
    //public int? PhoneNr { get; set; }
    
}