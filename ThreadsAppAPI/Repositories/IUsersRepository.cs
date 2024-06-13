using ThreadsAppAPI.Models.Authentication;

namespace ThreadsAppAPI.Repositories;

public interface IUsersRepository
{
    Task<User?> GenerateSingleUser(UserSignUp userSignUp, string hashedPass);
    
    Task<User?> DoesUserExist(string? userName, string? email);

}