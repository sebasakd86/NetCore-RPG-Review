using System.Threading.Tasks;
using Net_RPG.DTOs.Character;
using Net_RPG.Models;
using Net_RPG.DTOs.CharacterSkill;

namespace Net_RPG.Services.CharacterSkillService
{
    public interface ICharacterSkillService
    {
        Task<ServiceResponse<GetCharacterDTO>> AddCharacterSkill(AddCharacterSkillDTO newSkill);
    }
}