using ThreadsAppAPI.Models.Authentication;

namespace ThreadsAppAPI.Repositories;

public class CustomAuthenticationRepository : ICustomAuthenticationRepository
{
    private string _connString;

    public CustomAuthenticationRepository(IConfiguration config)
    {
        _connString = config.GetConnectionString("DefaultConnection");
    }
    
    public Task<User> Login(UserLogin userLogin)
    {
        throw new NotImplementedException();
    }
    
}

