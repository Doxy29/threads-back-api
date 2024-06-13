using Microsoft.AspNetCore.Mvc;
using ThreadsAppAPI.Models.Authentication;

namespace ThreadsAppAPI.Services.Authentication;

public interface IAuthenticationService
{
    Task<User?> Signup(UserSignUp userSignUp);

    Task<string> GenerateToken(UserLogin userLogin);
}