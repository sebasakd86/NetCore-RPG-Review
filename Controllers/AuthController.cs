using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Net_RPG.DTOs.User;
using Net_RPG.Models;

namespace Net_RPG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            this._authRepo = authRepo;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDTO user)
        {
            ServiceResponse<int> response = await _authRepo.Register(
                new Models.User { UserName = user.UserName}, user.Password
            );
            if(response.Success)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpPost("{Login}")]
        public async Task<IActionResult> Login(UserLoginDTO user)
        {
            ServiceResponse<string> response = await _authRepo.Login(user.UserName, user.Password);
            if(response.Success)
                return Ok(response);
            return BadRequest(response);            
        }
    }
}