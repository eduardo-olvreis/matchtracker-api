using MatchTracker.Api.DTOs.Partidas;
using MatchTracker.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace MatchTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartidaController : ControllerBase
    {
        private readonly IPartidaService _service;
        public PartidaController(IPartidaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartidaResponseDto>>> GetAllAsync()
        {
            var partidas = await _service.GetAllAsync();
            return Ok(partidas);
        }

        [HttpGet("{id}", Name = "GetPartidaById")]    
        public async Task<ActionResult<PartidaResponseDto>> GetByIdAsync(int id)
        {
            var partida = await _service.GetByIdAsync(id);
            return Ok(partida);
        }

        [HttpPost]
        public async Task<ActionResult<PartidaResponseDto>> AddAsync(PartidaCreateDto partidaDto)
        {
            var partidaCriada = await _service.AddAsync(partidaDto);
            return CreatedAtRoute("GetPartidaById", new { id = partidaCriada.Id }, partidaCriada);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PartidaResponseDto>> UpdateAsync(int id, [FromBody] PartidaCreateDto partidaDto)
        {
            var partidaAtualizada = await _service.UpdateAsync(id, partidaDto);
            return Ok(partidaAtualizada);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
