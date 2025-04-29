namespace ScreenSound.Web.Requests
{
    public record MusicaRequestEdit(int Id, string Nome, int AnoLancamento, int ArtistaId, ICollection<GeneroRequest> Generos) : MusicaRequest(Nome, ArtistaId, AnoLancamento, Generos);
}
