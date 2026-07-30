using MatchTracker.Api.DTOs.Partidas;

namespace MatchTracker.Api.Services
{
    public interface IPartidaService
    {
        public Task<IEnumerable<PartidaResponseDto>> GetAllAsync();
        public Task<PartidaResponseDto?> GetByIdAsync(int id);
        public Task<PartidaResponseDto> AddAsync(PartidaCreateDto partidaDto);
        public Task<PartidaResponseDto> UpdateAsync(int id, PartidaCreateDto partidaDto);
        public Task<bool> DeleteAsync(int id);
    }
}
