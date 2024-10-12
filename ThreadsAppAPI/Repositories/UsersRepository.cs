using System.Data;
using Dapper;
using Npgsql;
using ThreadsAppAPI.Models.Authentication;

namespace ThreadsAppAPI.Repositories;

public class UsersRepository : IUsersRepository
{
    private string _connString;
    private IConfiguration _config;

    public UsersRepository(IConfiguration config)
    {
        _config = config;
        _connString = config.GetSection("ConnectionString:DefaultConnection").Value;
    }
    
    public async Task<LoggedUserData?> GenerateSingleUser(UserSignUp userSignUp, string hashedPass, string role)
    {

        using (IDbConnection cnn = new NpgsqlConnection(_connString))
        {
            var p = new
            {
                UserAlias = userSignUp.UserAlias,
                FirtsName = userSignUp.UserName,
                LastName = userSignUp.UserSurname,
                Email = userSignUp.Email,
                
            };
            
            var qr = @"INSERT INTO users (user_alias,user_name, user_surname, email)
                        VALUES (@UserAlias,@FirtsName, @LastName, @Email)
                        RETURNING user_id;";

            var userId = await cnn.QuerySingleAsync<int>(qr, p);

            var p2 = new
            {
                UserId = userId,
                HashedPass = hashedPass,
                Role = role,
            };
            
            var qr2 =  @"INSERT INTO users_auth(user_id, hashed_pass, verified_Email, role)
                        VALUES(@UserId,@HashedPass,false, @Role);";
            
            await cnn.ExecuteAsync(qr2, p2);
            
            var newUser = new LoggedUserData
            {
                UserId = userId,
                UserAlias = userSignUp.UserAlias,
                UserName = userSignUp.UserName,
                UserSurname = userSignUp.UserSurname,
                Email = userSignUp.Email,
            };
            
            return newUser;
        }
    }
    
    public async Task<User?> DoesUserExist(string? userAlias, string? email)
    {
        Console.WriteLine(_connString);
        using (IDbConnection cnn = new NpgsqlConnection(_connString))
        {
            var p = new
            {
                UserAlias = userAlias,
                Email = email
            };
            
            var qr = @"SELECT us.user_id, us.user_alias, us.email, au.hashed_pass
                        FROM users us
                        INNER JOIN users_auth au
                        ON us.user_id = au.user_id
                        WHERE us.user_alias = @UserAlias OR us.email = @Email";

            var user = await cnn.QueryFirstOrDefaultAsync<User>(qr, p);
            
            return user;
        }
        
    }
    
    public async Task<string?> GetUserRole(string? userAlias, string? email)
    {
        Console.WriteLine(_connString);
        using (IDbConnection cnn = new NpgsqlConnection(_connString))
        {
            var p = new
            {
                UserAlias = userAlias,
                Email = email
            };
            
            var qr = @"SELECT role
                        FROM users us
                        INNER JOIN users_auth au
                        ON us.user_id = au.user_id
                        WHERE us.user_alias = @UserAlias OR us.email = @Email";

            var role = await cnn.QueryFirstOrDefaultAsync<string>(qr, p);
            
            return role;
        }
        
    }
}