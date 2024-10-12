using Microsoft.AspNetCore.Mvc;
using ThreadsAppAPI.Models.Authentication;

namespace ThreadsAppAPI.Services.Authentication;

public interface IAuthenticationService
{
    Task<LoggedUserData?> Signup(UserSignUp userSignUp, string role);

    Task<string> GenerateToken(UserLogin userLogin, string role);
}