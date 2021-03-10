using Microsoft.EntityFrameworkCore;
using Net_RPG.Models;

namespace Net_RPG.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {
            
        }
        public DbSet<Character> Characters { get; set; }
        public DbSet<User> Users {get;set;}
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<CharacterSkill> CharacterSkills {get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //create the composite key
            modelBuilder.Entity<CharacterSkill>()
                .HasKey(cs => new { cs.CharacterId, cs.SkillId });
        }
    }
}