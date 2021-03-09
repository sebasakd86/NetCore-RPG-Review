using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Net_RPG.Data;
using Net_RPG.DTOs.Character;
using Net_RPG.Models;

namespace Net_RPG.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public CharacterService(IMapper mapper, DataContext context)
        {
            this._context = context;
            this._mapper = mapper;

        }
        public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newChar)
        {
            ServiceResponse<List<GetCharacterDTO>> serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            Character oChar = _mapper.Map<Character>(newChar);
            int id = 1;
            if(await _context.Characters.CountAsync() > 0)
                id = await _context.Characters.MaxAsync(c => c.Id) + 1;
            oChar.Id = id;
            await _context.Characters.AddAsync(oChar);
            await _context.SaveChangesAsync();
            serviceResponse.Data = (await _context.Characters.ToListAsync()).Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters()
        {
            ServiceResponse<List<GetCharacterDTO>> serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            List<Character> dbChars = await _context.Characters.ToListAsync();
            serviceResponse.Data = _mapper.Map<List<GetCharacterDTO>>(dbChars); //dbChars.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id)
        {
            ServiceResponse<GetCharacterDTO> serviceResponse = new ServiceResponse<GetCharacterDTO>();
            serviceResponse.Data = _mapper.Map<GetCharacterDTO>(await _context.Characters.FirstOrDefaultAsync(c => c.Id == id));
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updatedChar)
        {
            ServiceResponse<GetCharacterDTO> serviceResponse = new ServiceResponse<GetCharacterDTO>();

            Character c = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedChar.Id);
            if (c != null)
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

            Character c = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            if (c != null)
            {
                _context.Characters.Remove(c);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<List<GetCharacterDTO>>(_context.Characters);
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