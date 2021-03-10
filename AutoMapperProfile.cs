using System.Linq;
using AutoMapper;
using Net_RPG.DTOs.Character;
using Net_RPG.DTOs.Skill;
using Net_RPG.DTOs.Weapon;
using Net_RPG.Models;

namespace Net_RPG
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDTO>()
                .ForMember(dto =>   dto.Skills, 
                                    c => c.MapFrom(c => 
                                                c.CharacterSkills.Select(cs => cs.Skills))).ReverseMap();
            CreateMap<AddCharacterDTO, Character>();
            CreateMap<Weapon, GetWeaponDTO>();
            CreateMap<Skills, GetSkillDTO>();
        }
    }
}