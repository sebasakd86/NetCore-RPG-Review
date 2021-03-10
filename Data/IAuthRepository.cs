using System.Threading.Tasks;
using Net_RPG.Models;

public interface IAuthRepository
{
    Task<ServiceResponse<int>> Register(User user, string password);
    Task<ServiceResponse<string>> Login(string userNanem, string password);
    Task<bool> UserExists(string username);
}