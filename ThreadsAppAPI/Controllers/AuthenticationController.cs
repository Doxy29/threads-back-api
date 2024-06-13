using System.Net;
using Amazon.Runtime;
using Microsoft.AspNetCore.Mvc;
using ThreadsAppAPI.Models.Authentication;
using ThreadsAppAPI.Models.AWS;
using ThreadsAppAPI.Services.Authentication;
using ThreadsAppAPI.Services.AWS;
using ThreadsAppAPI.Services.Users;
using ThreadsAppAPI.Utilities;

namespace ThreadsAppAPI.Controllers;

[Route("api/[controller]")] // api/authentication
[ApiController]

public class AuthenticationController : ControllerBase
{
    
    private readonly IAuthenticationService _authenticationService;
    private readonly IUsersService _usersService;
    private readonly IStorageService _storageService;
    private readonly IConfiguration _configuration;
    

    public AuthenticationController(
        IAuthenticationService authenticationService, 
        IUsersService usersService,
        IStorageService storageService,
        IConfiguration configuration
    ){
        _authenticationService = authenticationService;
        _usersService = usersService;
        _storageService = storageService;
        _configuration = configuration;
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<string> SignIn(UserLogin userLogin)
    {

        if (userLogin.UserAlias == null && userLogin.Email == null) throw new WebException("Insert User name or email");
        
        var userFound = await _usersService.DoesUserExist(userLogin.UserAlias, userLogin.Email);
        
        if (userFound != null && userFound.HashedPass != null  && HashHandler.CheckPassToHash(userLogin.Password, userFound.HashedPass))
        {
            return await _authenticationService.GenerateToken(new UserLogin {UserAlias = userFound.UserAlias, Email = userFound.Email, Password = userLogin.Password});
        }
        
        throw new WebException("Wrong password");
    }
    
    [HttpPost]
    [Route("signup")]
    public async Task<User?> SignUp(UserSignUp userSignUp)
    {
        //TODO uncomment on deploy
        //if (!UtilityFunctions.IsEmailValid(userSignUp.Email) ) throw new WebException("Invalid email");
        //if (!UtilityFunctions.IsPasswordValid(userSignUp.Password1)) throw new WebException("Password invalid password");
        if (userSignUp.Password1 != userSignUp.Password2) throw new WebException("Passwords don't match");
        
        var result = await _authenticationService.Signup(userSignUp);
        if(result == null) throw new WebException("User already exists");
        return result;
    }
    
    [HttpPost]
    [Route("aws")]
    public async Task<ActionResult<S3ResponseDto>> StorageService(IFormFile file)
    {
        var s3Obj = new S3Object()
        {
            BucketName = "threads-app-store",
            InputStream = file.OpenReadStream(),
            Name = file.FileName
        };

        

        var result = await _storageService.UploadFileAsync(s3Obj);
        
        return Ok(result);
    }
    
}