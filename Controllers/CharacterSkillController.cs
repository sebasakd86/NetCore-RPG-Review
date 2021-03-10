using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net_RPG.DTOs.CharacterSkill;
using Net_RPG.Services.CharacterSkillService;

namespace Net_RPG.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CharacterSkillController : ControllerBase
    {
        private readonly ICharacterSkillService _csService;
        public CharacterSkillController(ICharacterSkillService csService)
        {
            this._csService = csService;
        }
        [HttpPost]
        public async Task<IActionResult> AddCharacterSkill(AddCharacterSkillDTO newCS)
        {
            return Ok(await _csService.AddCharacterSkill(newCS));
        }
    }
}