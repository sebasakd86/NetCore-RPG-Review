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
    }
}