using MatchTracker.Api.Entities.Enums;

namespace MatchTracker.Api.DTOs.Partidas
{
    public class PartidaResponseDto
    {
        public int Id { get; set; }
        public MapaCs2 Mapa { get; set; }
        public int Kills { get; set; }
        public int Mortes { get; set; }
        public int Assistencias { get; set; }
        public ResultadoPartida Resultado { get; set; }
        public DateTime DataPartida { get; set; }
    }
}
