using ThreadsAppAPI.Models.Authentication;

namespace ThreadsAppAPI.Services.Users;

public interface IUsersService
{
    Task<User?> DoesUserExist(string? userName, string? email);
    Task<String?> GetUserRole(string? userName, string? email);
}