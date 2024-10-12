using ThreadsAppAPI.Models.Authentication;

namespace ThreadsAppAPI.Repositories;

public class CustomAuthenticationRepository : ICustomAuthenticationRepository
{
    private string _connString;

    public CustomAuthenticationRepository(IConfiguration config)
    {
        _connString = config.GetSection("ConnectionString:DefaultConnection").Value;
    }
    
    public Task<User> Login(UserLogin userLogin)
    {
        throw new NotImplementedException();
    }
    
}

