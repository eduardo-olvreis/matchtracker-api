using MatchTracker.Api.Data;
using MatchTracker.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace MatchTracker.Api.Repositories
{
    public class SqlPartidaRepository : IPartidaRepository
    {
        private readonly AppDbContext _context;
        public SqlPartidaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Partida>> GetAllAsync()
        {
            return await _context.Partidas.AsNoTracking().ToListAsync();
        }

        public async Task<Partida?> GetByIdAsync(int id)
        {
            return await _context.Partidas.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Partida> AddAsync(Partida partida)
        {
            await _context.Partidas.AddAsync(partida);
            await _context.SaveChangesAsync();
            return partida;
        }

        public async Task<Partida?> UpdateAsync(Partida partida)
        {
            var partidaEncontada = await GetByIdAsync(partida.Id);
            if (partidaEncontada == null) { return null; }
            _context.Update(partida);
            await _context.SaveChangesAsync();
            return partida;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var partidaEncontrada = await GetByIdAsync(id);
            if(partidaEncontrada == null) { return false; }
            _context.Remove(partidaEncontrada);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
