using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using rest_api_dotnetcore.Config;
using rest_api_dotnetcore.DTOs;
using rest_api_dotnetcore.Models;

namespace rest_api_dotnetcore.Repositories
{
    public interface IUsersRepository
    {
        Task<string> Login(LoginDTO login);
        Task<User> Register(RegisterDTO user);
    }

    public class UsersRepository : IUsersRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly IOptions<JwtCertificate> _jwtOptions;

        public UsersRepository(DatabaseContext dbContext, IOptions<JwtCertificate> jwtOptions)
        {
            _dbContext = dbContext;
            _jwtOptions = jwtOptions;
        }

        public async Task<string> Login(LoginDTO login)
        {
            var user = await Authenticate(login.Username, login.Password);

            if (user != null)
                return BuildJwt(user);

            return null;
        }

        public async Task<User> Register(RegisterDTO register)
        {
            if (await UserExists(register.Username))
                throw new Exception();

            var user = new User()
            {
                Name = register.Name,
                Username = register.Username,
                Email = register.Email,
                Password = BcryptPassword(register.Password)
            };

            await _dbContext.User.InsertOneAsync(user);

            return user;
        }

        // check to see if user exists
        private async Task<bool> UserExists(string username)
        {
            var exists = await _dbContext.User.FindAsync(Builders<User>.Filter.Eq(c => c.Username, username));

            if (await exists.AnyAsync())
                return true;

            return false;
        }

        // hash and salt passwords using bcrypt
        private String BcryptPassword(String password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // check to see if passwords match
        private bool PasswordMatched(User userInDb, string password)
        {
            if (BCrypt.Net.BCrypt.Verify(password, userInDb.Password))
                return true;

            return false;
        }

        // authenticate user
        private async Task<User> Authenticate(string username, string password)
        {
            var userCursor = await _dbContext.User.FindAsync(Builders<User>.Filter.Eq(u => u.Username, username));
            var user = await userCursor.FirstOrDefaultAsync();
            if (PasswordMatched(user, password))
                return user;
            return null;
        }

        // build jwt
        private string BuildJwt(User user)
        {
            // retrieve the secret from server
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.Secret));
            // hash the secret using SHA512 to make credentials for jwt
            var creds = new SigningCredentials(secret, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Value.Issuer,
                audience: _jwtOptions.Value.Audience,
                claims: new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Sid, user.Id),
                    new Claim(ClaimTypes.Role, "CSA"),
                    new Claim(ClaimTypes.Role, "Admin")
                },
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}