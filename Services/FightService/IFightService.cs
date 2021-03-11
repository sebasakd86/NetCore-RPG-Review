using System.Collections.Generic;
using System.Threading.Tasks;
using Net_RPG.DTOs.Fight;
using Net_RPG.Models;

namespace Net_RPG.Services.FightService
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultDTO>> WeaponAttack(WeaponAttackDTO request);
        Task<ServiceResponse<AttackResultDTO>> SkillAttack(SkillAttackDTO request);
        Task<ServiceResponse<FightResultDTO>> Fight(FightRequestDTO request);
        Task<ServiceResponse<List<HighscoreDTO>>> GetHighScores();
    }
}