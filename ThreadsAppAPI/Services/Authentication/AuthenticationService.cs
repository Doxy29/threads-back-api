using Microsoft.AspNetCore.Mvc;
using ThreadsAppAPI.Models.Authentication;
using ThreadsAppAPI.Repositories;
using ThreadsAppAPI.Utilities;

namespace ThreadsAppAPI.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtHandler _jwtHandler;

    public AuthenticationService(IUsersRepository usersRepository, IJwtHandler jwtHandler){
        _usersRepository = usersRepository;
        _jwtHandler = jwtHandler;
    }
    
    public async Task<User?> Signup(UserSignUp userSignUp)
    {

        var searchUser = await _usersRepository.DoesUserExist(userSignUp.UserAlias, userSignUp.Email);
        
        if (searchUser == null)
        {
            var hashedPass = HashHandler.HashPass(userSignUp.Password1);
            var newUser = await _usersRepository.GenerateSingleUser(userSignUp, hashedPass);
            
            var token = await GenerateToken(
                new UserLogin { UserAlias = newUser.UserAlias, Email = newUser.Email, Password = userSignUp.Password1 }
                );
            
            newUser.Auth = new AutheticationData
            {
                Token = token,
                ResetToken = null,
                Role = "user"
            };

            return newUser;
        }

        return null;

    }

    public Task<string> GenerateToken(UserLogin userLogin)
    {
        return _jwtHandler.Generate(userLogin);
    }
}