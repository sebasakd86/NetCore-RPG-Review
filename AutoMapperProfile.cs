using AutoMapper;
using Net_RPG.DTOs.Character;
using Net_RPG.DTOs.Weapon;
using Net_RPG.Models;

namespace Net_RPG
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDTO>();
            CreateMap<AddCharacterDTO, Character>();
            CreateMap<Weapon, GetWeaponDTO>();
        }
    }
}