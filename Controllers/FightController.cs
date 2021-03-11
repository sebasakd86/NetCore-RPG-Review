using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Net_RPG.DTOs.Fight;
using Net_RPG.Services.FightService;

namespace Name.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FightController : ControllerBase
    {
        private readonly IFightService _service;
        public FightController(IFightService service)
        {
            this._service = service;
        }
        [HttpPost("Weapon")]
        public async Task<IActionResult> WeaponAttack(WeaponAttackDTO request)
        {
            return Ok(await _service.WeaponAttack(request));
        }
        [HttpPost("Skill")]
        public async Task<IActionResult> SkillAttack(SkillAttackDTO request)
        {
            return Ok(await _service.SkillAttack(request));
        }        
        [HttpPost]
        public async Task<IActionResult> Fight(FightRequestDTO request)
        {
            return Ok(await _service.Fight(request));
        }   
        [HttpGet]
        public async Task<ActionResult> GetHighScore()
        {
            return Ok(await _service.GetHighScores());
        }
    }
}