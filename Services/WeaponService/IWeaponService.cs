using System.Threading.Tasks;
using Net_RPG.DTOs.Character;
using Net_RPG.DTOs.Weapon;
using Net_RPG.Models;

namespace Net_RPG.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDTO>> AddWeapon(AddWeaponDTO newWeapon);
    }
}