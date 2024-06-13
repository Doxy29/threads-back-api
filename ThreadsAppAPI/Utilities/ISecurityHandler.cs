using ThreadsAppAPI.Models.Authentication;

namespace ThreadsAppAPI.Utilities;

public interface IJwtHandler
{ 
    Task<string> Generate(UserLogin userLogin);
    
}