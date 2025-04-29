namespace ScreenSound.Web.Response
{
    public record MusicaResponse(int Id, string Nome, int ArtistaId, string NomeArtista, int AnoLancamento, string FotoArtista, List<int> GenerosIds);    
}
