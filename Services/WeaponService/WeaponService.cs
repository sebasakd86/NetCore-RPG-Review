using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Net_RPG.Data;
using Net_RPG.DTOs.Character;
using Net_RPG.DTOs.Weapon;
using Net_RPG.Models;

namespace Net_RPG.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _dbContext;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;
        public WeaponService(DataContext dbContext, IHttpContextAccessor httpContext, IMapper mapper)
        {
            this._mapper = mapper;
            this._httpContext = httpContext;
            this._dbContext = dbContext;

        }
        public async Task<ServiceResponse<GetCharacterDTO>> AddWeapon(AddWeaponDTO newWeapon)
        {
            ServiceResponse<GetCharacterDTO> response = new ServiceResponse<GetCharacterDTO>();
            try
            {
                Character c = await _dbContext.Characters
                    .FirstOrDefaultAsync(c => 
                    c.Id == newWeapon.CharacterId && 
                    c.User.Id == int.Parse(_httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
                if(c == null)
                    throw new System.Exception("Character not found");
                Weapon w = new Weapon
                {
                    Name = newWeapon.Name,
                    Damage = newWeapon.Damage,
                    Character = c
                };
                await _dbContext.Weapons.AddAsync(w);
                await _dbContext.SaveChangesAsync();
                response.Data = _mapper.Map<GetCharacterDTO>(c);

            }
            catch (System.Exception ex)
            {
                 response.Success = false;
                 response.Message = ex.Message;
            }
            return response;
        }
    }
}