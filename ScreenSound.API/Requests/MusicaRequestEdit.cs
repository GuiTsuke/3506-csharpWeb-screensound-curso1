namespace ScreenSound.API.Requests
{
    public record MusicaRequestEdit(int Id, string Nome, int AnoLancamento, int ArtistaId, ICollection<GeneroRequest>? Generos = null) : MusicaRequest(Nome, ArtistaId, AnoLancamento, Generos);
}
