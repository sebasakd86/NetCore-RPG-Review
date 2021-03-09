using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Net_RPG.DTOs.Character;
using Net_RPG.Models;

namespace Net_RPG.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        public CharacterService(IMapper mapper)
        {
            this._mapper = mapper;

        }
        private static Character knight = new Character();
        private static List<Character> chars = new List<Character>
        {
            new Character(),
            new Character { Id = 1, Name = "Sam"}
        };

        public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newChar)
        {
            ServiceResponse<List<GetCharacterDTO>> serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            Character oChar = _mapper.Map<Character>(newChar);
            oChar.Id = chars.Max(c => c.Id) + 1;
            chars.Add(oChar);
            serviceResponse.Data = chars.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters()
        {
            ServiceResponse<List<GetCharacterDTO>> serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            serviceResponse.Data = chars.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id)
        {
            ServiceResponse<GetCharacterDTO> serviceResponse = new ServiceResponse<GetCharacterDTO>();
            serviceResponse.Data = _mapper.Map<GetCharacterDTO>(chars.FirstOrDefault(c => c.Id == id));
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updatedChar)
        {
            ServiceResponse<GetCharacterDTO> serviceResponse = new ServiceResponse<GetCharacterDTO>();

            Character c = chars.FirstOrDefault(c => c.Id == updatedChar.Id);
            if(c != null)
            {
                c.Name = updatedChar.Name;
                c.Class = updatedChar.Class;
                c.Defense = updatedChar.Defense;
                c.HitPoints = updatedChar.HitPoints;
                c.Intelligence = updatedChar.Intelligence;
                c.Strength = updatedChar.Strength;                
                serviceResponse.Data = _mapper.Map<GetCharacterDTO>(c);
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found";
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDTO>> serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();

            Character c = chars.FirstOrDefault(c => c.Id == id);
            if(c != null)
            {
                chars.Remove(c);
                serviceResponse.Data = _mapper.Map<List<GetCharacterDTO>>(chars);
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found";
            }
            return serviceResponse;
        }
    }
}