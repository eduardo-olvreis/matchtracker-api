using MatchTracker.Api.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace MatchTracker.Api.Entities
{
    public class Partida
    {
        [Key]
        public int Id { get; set; }

        [EnumDataType(typeof(MapaCs2), ErrorMessage = "Mapa informado é inválido.")]
        public MapaCs2 Mapa { get; set; }

        [Range(0,100,ErrorMessage = "Campo 'Kills' deve estar entre o intervalo de 0 a 100.")]
        public int Kills { get; set; }

        [Range(0, 100, ErrorMessage = "Campo 'Mortes' deve estar entre o intervalo de 0 a 100.")]
        public int Mortes { get; set; }

        [Range(0, 100, ErrorMessage = "Campo 'Assistencias' deve estar entre o intervalo de 0 a 100.")]
        public int Assistencias { get; set; }

        [EnumDataType(typeof(ResultadoPartida), ErrorMessage = "Resultado informado é inválido.")]
        public ResultadoPartida Resultado { get; set; }

        [Required(ErrorMessage = "Campo 'DataPartida' não pode ser nulo.")]
        public DateOnly DataPartida { get; set; }
    }
}
