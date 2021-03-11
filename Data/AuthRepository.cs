using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Net_RPG.Models;

namespace Net_RPG.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;
        public AuthRepository(DataContext context, IConfiguration config)
        {
            this._config = config;
            this._context = context;
        }
        public async Task<ServiceResponse<string>> Login(string userName, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            User u = await _context.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower());
            if (u == null)
            {
                response.Success = false;
                response.Message = "User not found or password not correct.";
            }
            else if (!VerifyPassword(password, u.PasswordHash, u.PasswordSalt))
            {
                response.Success = false;
                response.Message = "User not found or password not correct";
            }
            else
            {
                response.Data = CreateToken(u);
            }
            return response;

        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            ServiceResponse<int> response = new ServiceResponse<int>(user.Id);
            Utils.CreatePasswordHash(password, out byte[] hash, out byte[] salt);
            if (await UserExists(user.UserName))
            {
                response.Success = false;
                response.Message = $"User {user.UserName} already exists";
                return response;
            }
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            response.Data = user.Id;

            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(u => u.UserName.ToLower() == username.ToLower());
        }
        private bool VerifyPassword(string password, byte[] hash, byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(salt))
            {
                var compute = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < compute.Length; i++)
                {
                    if (compute[i] != hash[i])
                        return false;
                }
            }
            return true;
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role) //to add the role to the JWT
            };
            SymmetricSecurityKey k = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value)
            );

            SigningCredentials creds = new SigningCredentials(k, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(100), //Todo Change to 1 if ever in production, just to store the token in postman
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}