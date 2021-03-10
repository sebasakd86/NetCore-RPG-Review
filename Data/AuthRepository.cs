using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Net_RPG.Models;

namespace Net_RPG.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            this._context = context;
        }
        public async Task<ServiceResponse<string>> Login(string userName, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            User u = await _context.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower());
            if(u == null) {
                response.Success = false;
                response.Message = "User not found or password not correct.";
            }
            else if(!VerifyPassword(password, u.PasswordHash, u.PasswordSalt))
            {
                response.Success = false;
                response.Message = "User not found or password not correct";
            }
            else{
                response.Data = u.Id.ToString();
            }
            return response;

        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            ServiceResponse<int> response = new ServiceResponse<int>(user.Id);
            CreatePasswordHash(password, out byte[] hash, out byte[] salt);
            if( await UserExists(user.UserName)){
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

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPassword(string password, byte[] hash, byte[] salt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(salt))
            {
                var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i < compute.Length; i++){
                    if(compute[i] != hash[i])
                        return false;
                }                
            }
            return true;
        }
    }
}