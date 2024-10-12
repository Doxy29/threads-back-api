using ThreadsAppAPI.Models.Authentication;

namespace ThreadsAppAPI.Repositories;

public interface IUsersRepository
{
    Task<LoggedUserData?> GenerateSingleUser(UserSignUp userSignUp, string hashedPass, string role);
    
    Task<User?> DoesUserExist(string? userName, string? email);
    Task<string?> GetUserRole(string? userName, string? email);

}