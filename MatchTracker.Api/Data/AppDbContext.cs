using MatchTracker.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace MatchTracker.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Partida> Partidas { get; set; }
    }
}
