using ThreadsAppAPI.Models.Authentication;

namespace ThreadsAppAPI.Repositories;

public interface ICustomAuthenticationRepository
{
    Task<User> Login(UserLogin userLogin);
    
}