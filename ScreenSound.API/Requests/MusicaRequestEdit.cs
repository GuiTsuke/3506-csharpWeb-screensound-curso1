namespace ScreenSound.API.Requests
{
    public record MusicaRequestEdit(int Id, string Nome, int AnoLancamento, int ArtistaId) : MusicaRequest(Nome, ArtistaId, AnoLancamento);
}
