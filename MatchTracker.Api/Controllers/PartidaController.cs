using MatchTracker.Api.DTOs.Partidas;
using MatchTracker.Api.Entities;
using MatchTracker.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MatchTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartidaController : ControllerBase
    {
        private readonly IPartidaRepository _repository;
        public PartidaController(IPartidaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartidaResponseDto>>> GetAllAsync()
        {
            var partidas = await _repository.GetAllAsync();
            var response = partidas.Select(MapearParaDto).ToList();
            return Ok(response);
        }

        [HttpGet("{id}", Name = "GetPartidaById")]    
        public async Task<ActionResult<PartidaResponseDto>> GetByIdAsync(int id)
        {
            var partida = await _repository.GetByIdAsync(id);
            if(partida == null)
            {
                return NotFound($"Partida de ID: {id} não encontrada.");
            }
            var response = MapearParaDto(partida);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<PartidaResponseDto>> AddAsync(PartidaCreateDto partidaDto)
        {
            var partida = new Partida
            {
                Mapa = partidaDto.Mapa.Value,
                Kills = partidaDto.Kills,
                Mortes = partidaDto.Mortes,
                Assistencias = partidaDto.Assistencias,
                PlacarTime = partidaDto.PlacarTime,
                PlacarAdversario = partidaDto.PlacarAdversario,
                DataPartida = partidaDto.DataPartida
            };
            var partidaCriada = await _repository.AddAsync(partida);
            var response = MapearParaDto(partidaCriada);
            return CreatedAtRoute("GetPartidaById", new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PartidaResponseDto>> UpdateAsync(int id, [FromBody] PartidaCreateDto partidaDto)
        {
            var partidaEncontrada = await _repository.GetByIdAsync(id);
            if (partidaEncontrada == null) { return NotFound($"Partida com Id {id} não encontrada."); }
            partidaEncontrada.Mapa = partidaDto.Mapa.Value;
            partidaEncontrada.Kills = partidaDto.Kills;
            partidaEncontrada.Mortes = partidaDto.Mortes;
            partidaEncontrada.Assistencias = partidaDto.Assistencias;
            partidaEncontrada.PlacarTime = partidaDto.PlacarTime;
            partidaEncontrada.PlacarAdversario = partidaDto.PlacarAdversario;
            partidaEncontrada.DataPartida = partidaDto.DataPartida;
            var partidaAtualizada = await _repository.UpdateAsync(partidaEncontrada);
            var response = MapearParaDto(partidaAtualizada);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var partida = await _repository.DeleteAsync(id);
            if(partida == false) { return NotFound($"Partida com Id {id} não encontrada."); }
            return NoContent();
        }

        public PartidaResponseDto MapearParaDto(Partida partida)
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
    }
}
