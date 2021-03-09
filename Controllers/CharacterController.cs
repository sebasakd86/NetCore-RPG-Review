using Microsoft.AspNetCore.Mvc;
using Net_RPG.DTOs.Character;
using Net_RPG.Models;
using Net_RPG.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net_RPG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _service;
        public CharacterController(ICharacterService service)
        {
            this._service = service;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await this._service.GetCharacterById(id));
            // return BadRequest(knight);
            // return NotFound(knight);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllChars()
        {
            return Ok(await this._service.GetAllCharacters());
        }
        [HttpPost]
        public async Task<IActionResult> AddCharacter(AddCharacterDTO newChar)
        {
            return Ok(await this._service.AddCharacter(newChar));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCharacter(UpdateCharacterDTO updatedChar)
        {
            var response = await this._service.UpdateCharacter(updatedChar);
            if(response.Success)
                return Ok(response);
            return NotFound(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCharacter(int id)
        {
            var response = await this._service.DeleteCharacter(id);
            if(response.Success)
                return Ok(response);
            return NotFound(response);            
        }
    }
}