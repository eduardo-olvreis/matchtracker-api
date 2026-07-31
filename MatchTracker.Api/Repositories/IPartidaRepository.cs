using MatchTracker.Api.Entities;

namespace MatchTracker.Api.Repositories
{
    public interface IPartidaRepository
    {
        Task<IEnumerable<Partida>> GetAllAsync();
        Task<Partida> GetByIdAsync(int id);
        Task<Partida> AddAsync(Partida partida);
        Task<Partida> UpdateAsync(Partida partida);
        Task<bool> DeleteAsync(int id);
    }
}
