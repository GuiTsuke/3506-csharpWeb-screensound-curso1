using System.ComponentModel.DataAnnotations;
using ScreenSound.Shared.Models;

namespace ScreenSound.API.Requests
{
    public record MusicaRequest([Required] string Nome, [Required]int ArtistaId, int AnoLancamento, ICollection<GeneroRequest>? Generos = null);
}
