using System.Collections.Generic;
using System.Threading.Tasks;
using Net_RPG.DTOs.Character;
using Net_RPG.Models;

namespace Net_RPG.Services
{
    public interface ICharacterService
    {
         Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters(int userId);
         Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id);
         Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newChar);
         Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updatedChar);
         Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int id);
    }
}