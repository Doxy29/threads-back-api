using System.Net;
using Microsoft.AspNetCore.Authorization;
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

    public AuthenticationController(
        IAuthenticationService authenticationService, 
        IUsersService usersService,
        IStorageService storageService
    ){
        _authenticationService = authenticationService;
        _usersService = usersService;
        _storageService = storageService;
    }
    
    [HttpPost]
    [Route("signIn")]
    public async Task<ActionResult<LoggedUserData>> SignIn(UserLogin userLogin)
    {

        if (userLogin.UserAlias == null && userLogin.Email == null) return BadRequest("Insert User alias or email");
        
        var userFound = await _usersService.DoesUserExist(userLogin.UserAlias, userLogin.Email);
        
        if (userFound != null && userFound.HashedPass != null  && HashHandler.CheckPassToHash(userLogin.Password, userFound.HashedPass))
        {
            var role = await _usersService.GetUserRole(userLogin.UserAlias, userLogin.Email);
            
            var token = await _authenticationService.GenerateToken(new UserLogin {UserAlias = userFound.UserAlias, Email = userFound.Email, Password = userLogin.Password}, role);
            
            LoggedUserData data = new LoggedUserData()
            {
                UserId = userFound.UserId,
                UserAlias = userFound.UserAlias,
                UserName = userFound.UserName,
                UserSurname = userFound.UserSurname,
                Auth = new AutheticationData()
                {
                    Token = token,
                    Role = role
                }
            };
            
            return Ok(data);
        }

        return BadRequest("Wrong credentials");
    }
    
    [HttpPost]
    [Route("signup")]
    public async Task<ActionResult<LoggedUserData>> SignUp(UserSignUp userSignUp)
    {
        //TODO uncomment on deploy
        //if (!UtilityFunctions.IsEmailValid(userSignUp.Email) ) throw new WebException("Invalid email");
        //if (!UtilityFunctions.IsPasswordValid(userSignUp.Password1)) throw new WebException("Password invalid password");
        if (userSignUp.Password1 != userSignUp.Password2) throw new WebException("Passwords don't match");
        
        var result = await _authenticationService.Signup(userSignUp, "User");
        if(result == null) return BadRequest("Insert User alias or email");
        return result;
    }
    
    [Authorize]
    [HttpGet]
    [Route("auth")]
    public async Task<IActionResult> Auttt()
    {
        return Ok();
    }
    
    [HttpPost]
    [Route("aws")]
    public async Task<IActionResult> StorageService(IFormFile file)
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
