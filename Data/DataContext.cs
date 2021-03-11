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
                .HasKey(cs => new { cs.CharacterId, cs.SkillsId });
            //sets the default value for a field    
            modelBuilder.Entity<User>().Property(u => u.Role).HasDefaultValue("Player");
            //seed the data
            modelBuilder.Entity<Skills>().HasData(
                new Skills { Id = 1, Name = "Fiberall", Damage = 10},
                new Skills { Id = 2, Name = "Thunderbolt", Damage = 25},
                new Skills { Id = 3, Name = "Blizzard", Damage = 50}
            );

            Utils.CreatePasswordHash("Moni10", out byte[] hash, out byte[] salt);

            modelBuilder.Entity<User>().HasData(
                new User { Id=1, UserName="sebasakd86", PasswordHash=hash, PasswordSalt = salt},
                new User { Id=2, UserName="pablo", PasswordHash=hash, PasswordSalt = salt},
                new User { Id=3, UserName="admin", Role="Admin", PasswordHash=hash, PasswordSalt = salt}
            );

            modelBuilder.Entity<Character>().HasData(
                new Character 
                {
                    Id = 1,
                    Name = "Frodo",
                    Strength = 10,
                    UserId = 1
                },
                new Character 
                {
                    Id = 2,
                    Name = "Gandalf",
                    Strength = 15,
                    Intelligence = 25,
                    Defense = 15,
                    UserId = 1
                },
                new Character 
                {
                    Id = 3,
                    Name = "Sauron",
                    Class = RPGClass.Mage,
                    Strength = 100,
                    Intelligence = 50,
                    Defense = 1,
                    UserId = 2
                }
            );

            modelBuilder.Entity<Weapon>().HasData(
                new Weapon { Id = 1, Name = "The One Ring", Damage=50, CharacterId = 1},
                new Weapon { Id = 2, Name = "Nazgul", Damage=50, CharacterId = 3} 
            );
            modelBuilder.Entity<CharacterSkill>().HasData(
                new CharacterSkill { CharacterId = 2, SkillsId = 1},
                new CharacterSkill { CharacterId = 2, SkillsId = 2},
                new CharacterSkill { CharacterId = 2, SkillsId = 3},
                new CharacterSkill { CharacterId = 3, SkillsId = 1}
            );
        }
    }
}