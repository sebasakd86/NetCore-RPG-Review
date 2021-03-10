using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Net_RPG.Data;
using Net_RPG.DTOs.Character;
using Net_RPG.DTOs.CharacterSkill;
using Net_RPG.Models;

namespace Net_RPG.Services.CharacterSkillService
{
    public class CharacterSkillService : ICharacterSkillService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly DataContext _context;
        public CharacterSkillService(DataContext context, IHttpContextAccessor httpContext, IMapper mapper)
        {
            this._context = context;
            this._httpContext = httpContext;
            this._mapper = mapper;

        }
        public async Task<ServiceResponse<GetCharacterDTO>> AddCharacterSkill(AddCharacterSkillDTO newSkill)
        {
            ServiceResponse<GetCharacterDTO> resp = new ServiceResponse<GetCharacterDTO>();
            try
            {
                Character c = await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.CharacterSkills)
                    .ThenInclude(cs => cs.Skills)
                    .FirstOrDefaultAsync(c => c.Id == newSkill.CharacterId &&
                    c.User.Id == int.Parse(_httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
                if(c == null)
                    throw new System.Exception("Character not found");
                Skills s = await _context.Skills
                    .FirstOrDefaultAsync(s => s.Id == newSkill.SkillId);
                if(s ==null)
                    throw new System.Exception("Skill not found");
                CharacterSkill cs = new CharacterSkill
                {
                    Character = c,
                    Skills = s
                };
                await _context.CharacterSkills.AddAsync(cs);
                await _context.SaveChangesAsync();

                resp.Data = _mapper.Map<GetCharacterDTO>(c);
            }
            catch (System.Exception ex)
            {
                resp.Success = false;
                resp.Message = ex.Message;
            }
            return resp;
        }
    }
}