using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ThreadsAppAPI.Models.Authentication;

namespace ThreadsAppAPI.Utilities;

public class JwtHandler:IJwtHandler
{
    private readonly IConfiguration _configuration;
    
    public JwtHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<string> Generate(UserLogin userLogin, String role)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userLogin.UserAlias),
            new Claim(ClaimTypes.Role, role)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}

public static class HashHandler
{
    public static string HashPass(string pass)
    {
        return BCrypt.Net.BCrypt.HashPassword(pass);
    }

    public static bool CheckPassToHash(string pass, string hashedPAss)
    {
        return BCrypt.Net.BCrypt.Verify(pass, hashedPAss);

    }
}