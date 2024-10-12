using ThreadsAppAPI.Models.Authentication;
using ThreadsAppAPI.Repositories;

namespace ThreadsAppAPI.Services.Users;

public class UsersService: IUsersService
{
    private readonly IUsersRepository _usersRepository;

    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    
    public Task<User?> DoesUserExist(string? userName, string? email)
    {
        return _usersRepository.DoesUserExist(userName, email);
    } public Task<string?> GetUserRole(string? userName, string? email)
    {
        return _usersRepository.GetUserRole(userName, email);
    }
}