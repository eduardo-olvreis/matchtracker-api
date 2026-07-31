using MatchTracker.Api.DTOs.Partidas;
using MatchTracker.Api.Entities;
using MatchTracker.Api.Entities.Enums;
using MatchTracker.Api.Exceptions;
using MatchTracker.Api.Repositories;

namespace MatchTracker.Api.Services
{
    public class PartidaService : IPartidaService
    {
        private readonly IPartidaRepository _repository;
        public PartidaService(IPartidaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PartidaResponseDto>> GetAllAsync()
        {
            var partidas = await _repository.GetAllAsync();
            return partidas.Select(MapearParaDto).ToList();
        }

        public async Task<PartidaResponseDto> GetByIdAsync(int id)
        {
            var partida = await _repository.GetByIdAsync(id);
            if(partida == null) { throw new PartidaNaoEncontradaException($"Partida com Id {id} não encontrada."); }
            return MapearParaDto(partida);
        }

        public async Task<PartidaResponseDto> AddAsync(PartidaCreateDto partidaDto)
        {
            var placarValido = IsPlacarValido(partidaDto);
            if(placarValido == false) { throw new PlacarInvalidoException("Valor dos placares estão incorretos."); }
            var partida = new Partida
            {
                Mapa = partidaDto.Mapa.Value,
                Kills = partidaDto.Kills,
                Mortes = partidaDto.Mortes,
                Assistencias = partidaDto.Assistencias,
                PlacarTime = partidaDto.PlacarTime,
                PlacarAdversario = partidaDto.PlacarAdversario,
                Resultado = DefineResultado(partidaDto),
                DataPartida = partidaDto.DataPartida
            };
            var partidaCriada = await _repository.AddAsync(partida);
            return MapearParaDto(partidaCriada);
        }

        public async Task<PartidaResponseDto> UpdateAsync(int id, PartidaCreateDto partidaDto)
        {
            var partidaEncontrada = await _repository.GetByIdAsync(id);
            if(partidaEncontrada == null) { throw new PartidaNaoEncontradaException($"Partida com Id {id} não encontrada."); }
            if (!IsPlacarValido(partidaDto)) { throw new PlacarInvalidoException("Valor dos placares estão incorretos."); }
            partidaEncontrada.Mapa = partidaDto.Mapa.Value;
            partidaEncontrada.Kills = partidaDto.Kills;
            partidaEncontrada.Mortes = partidaDto.Mortes;
            partidaEncontrada.Assistencias = partidaDto.Assistencias;
            partidaEncontrada.PlacarTime = partidaDto.PlacarTime;
            partidaEncontrada.PlacarAdversario = partidaDto.PlacarAdversario;
            partidaEncontrada.Resultado = DefineResultado(partidaDto);
            partidaEncontrada.DataPartida = partidaDto.DataPartida;
            var partidaAtualizada = await _repository.UpdateAsync(partidaEncontrada);
            return MapearParaDto(partidaAtualizada);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private PartidaResponseDto MapearParaDto(Partida partida)
        {
            return new PartidaResponseDto
            {
                Id = partida.Id,
                Mapa = partida.Mapa,
                Kills = partida.Kills,
                Mortes = partida.Mortes,
                Assistencias = partida.Assistencias,
                PlacarTime = partida.PlacarTime,
                PlacarAdversario = partida.PlacarAdversario,
                Resultado = partida.Resultado,
                DataPartida = partida.DataPartida
            };
        }

        private bool IsPlacarValido(PartidaCreateDto dto)
        {
            if((dto.PlacarTime == 13 || dto.PlacarAdversario == 13) && (dto.PlacarTime < 13 || dto.PlacarAdversario < 13)) {  return true; }
            else if((dto.PlacarTime == 16 || dto.PlacarAdversario == 16) && (dto.PlacarTime >= 12 || dto.PlacarAdversario >= 12) && (dto.PlacarTime < 16 || dto.PlacarAdversario < 16)) { return true; }
            else if (dto.PlacarTime == 15 && dto.PlacarAdversario == 15) { return true; }
            else return false;
        }

        private ResultadoPartida DefineResultado(PartidaCreateDto dto)
        {
            if (dto.PlacarTime > dto.PlacarAdversario) { return ResultadoPartida.Vitoria; }
            if (dto.PlacarAdversario > dto.PlacarTime) { return ResultadoPartida.Derrota; }
            else { return ResultadoPartida.Empate; }
        }
    }
}
