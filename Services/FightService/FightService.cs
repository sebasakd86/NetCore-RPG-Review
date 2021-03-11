using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Net_RPG.Data;
using Net_RPG.DTOs.Fight;
using Net_RPG.Models;

namespace Net_RPG.Services.FightService
{
    public class FightService : IFightService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public FightService(DataContext context, IMapper mapper)
        {
            this._mapper = mapper;
            this._context = context;
        }

        public async Task<ServiceResponse<FightResultDTO>> Fight(FightRequestDTO request)
        {
            ServiceResponse<FightResultDTO> response = new ServiceResponse<FightResultDTO>(new FightResultDTO());
            try
            {
                var chars = await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.CharacterSkills).ThenInclude(cs => cs.Skills)
                    .Where(c => request.CharacterIds.Contains(c.Id)).ToListAsync();
                bool defeated = false;
                while (!defeated)
                {
                    foreach (var attacker in chars)
                    {
                        List<Character> enemies = chars.Where(c => c.Id != attacker.Id).ToList();
                        Character enemy = enemies[new Random().Next(enemies.Count())];

                        int dmg = 0;
                        string attackUsed = string.Empty;

                        bool useWeapon = new Random().Next(2) == 0;
                        if (attacker.Weapon != null && attacker.CharacterSkills.Count == 0)
                            useWeapon = true;
                        else if (attacker.Weapon == null && attacker.CharacterSkills.Count > 0)
                            useWeapon = false;

                        if (useWeapon)
                        {
                            attackUsed = attacker.Weapon.Name;
                            dmg = WeaponAttackDamage(attacker, enemy);
                        }
                        else
                        {
                            int randomSkill = new Random().Next(attacker.CharacterSkills.Count);
                            attackUsed = attacker.CharacterSkills[randomSkill].Skills.Name;
                            dmg = SkillsAttackDamage(attacker, enemy, attacker.CharacterSkills[randomSkill]);
                        }
                        response.Data.Log.Add($"{attacker.Name} attacks {enemy.Name} with {(dmg > 0 ? dmg : 0)} damage.");
                        if (enemy.HitPoints <= 0)
                        {
                            defeated = true;
                            attacker.Victories++;
                            enemy.Defeats++;
                            response.Data.Log.Add($"{attacker.Name} defeats {enemy.Name}");
                            break;
                        }
                    }
                }
                chars.ForEach(c =>
                {
                    c.Fights++;
                    c.HitPoints = 100;
                });
                _context.Characters.UpdateRange(chars);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<ServiceResponse<AttackResultDTO>> SkillAttack(SkillAttackDTO request)
        {
            var response = new ServiceResponse<AttackResultDTO>();
            try
            {
                var c = await _context.Characters
                    .Include(c => c.CharacterSkills).ThenInclude(cs => cs.Skills)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                if (c == null)
                    throw new System.Exception("Attacking character not found");
                var enemy = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == request.EnemyId);
                if (enemy == null)
                    throw new System.Exception("Enemy character not found");

                var cSkill = c.CharacterSkills.FirstOrDefault(cs => cs.Skills.Id == request.SkillId);
                if (cSkill == null)
                    throw new Exception($"Skill not found on character {c.Name}");
                int dmg = SkillsAttackDamage(c, enemy, cSkill);
                if (enemy.HitPoints <= 0)
                    response.Message = $"{enemy.Name} has been defeated";
                _context.Characters.Update(enemy);
                await _context.SaveChangesAsync();
                response.Data = new AttackResultDTO
                {
                    Attacker = c.Name,
                    AttackerHP = c.HitPoints,
                    Enemy = enemy.Name,
                    EnemyHp = enemy.HitPoints,
                    Damage = dmg
                };
            }
            catch (System.Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
        private static int SkillsAttackDamage(Character c, Character enemy, CharacterSkill cSkill)
        {
            int dmg = cSkill.Skills.Damage + (new Random().Next(c.Intelligence));
            dmg -= new Random().Next(enemy.Defense);
            if (dmg > 0)
                enemy.HitPoints -= dmg;
            return dmg;
        }
        public async Task<ServiceResponse<AttackResultDTO>> WeaponAttack(WeaponAttackDTO request)
        {
            var response = new ServiceResponse<AttackResultDTO>();
            try
            {
                var c = await _context.Characters
                    .Include(c => c.Weapon)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                if (c == null)
                    throw new System.Exception("Attacking character not found");
                var enemy = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == request.EnemyId);
                if (enemy == null)
                    throw new System.Exception("Oponnent character not found");
                int dmg = WeaponAttackDamage(c, enemy);
                if (enemy.HitPoints <= 0)
                    response.Message = $"{enemy.Name} has been defeated";
                _context.Characters.Update(enemy);
                await _context.SaveChangesAsync();
                response.Data = new AttackResultDTO
                {
                    Attacker = c.Name,
                    AttackerHP = c.HitPoints,
                    Enemy = enemy.Name,
                    EnemyHp = enemy.HitPoints,
                    Damage = dmg
                };
            }
            catch (System.Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
        private static int WeaponAttackDamage(Character c, Character enemy)
        {
            int dmg = c.Weapon.Damage + (new Random().Next(c.Strength));
            dmg -= new Random().Next(enemy.Defense);
            if (dmg > 0)
                enemy.HitPoints -= dmg;
            return dmg;
        }
        public async Task<ServiceResponse<List<HighscoreDTO>>> GetHighScores()
        {
            var chars = await _context.Characters
                .Where(c => c.Fights > 0)
                .OrderByDescending(c => c.Victories)
                .ThenBy(c => c.Defeats)
                .ToListAsync();
            var response = new ServiceResponse<List<HighscoreDTO>>()
            {
                Data = _mapper.Map<List<HighscoreDTO>>(chars)
            };
            return response;
        }
    }
}