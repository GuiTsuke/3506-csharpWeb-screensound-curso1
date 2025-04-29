using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using ScreenSound.Web.Requests;
using ScreenSound.Web.Response;

namespace ScreenSound.Web.Services
{
    public class ArtistaAPI
    {
        private readonly HttpClient _httpClient;

        public ArtistaAPI(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }

        public async Task<ICollection<ArtistaResponse>?> GetArtistasAsync()
        {
            return await _httpClient.GetFromJsonAsync<ICollection<ArtistaResponse>>("Artistas");
        }

        public async Task AddArtistaAsync(ArtistaRequest artistaRequest)
        {
            await _httpClient.PostAsJsonAsync("Artistas", artistaRequest);
        }

        public async Task UpdateArtistaAsync(ArtistaRequestEdit artistaRequest)
        {
            await _httpClient.PutAsJsonAsync("Artistas", artistaRequest);
        }        

        public async Task DeleteArtistaAsync(int id)
        {
            await _httpClient.DeleteAsync($"Artistas/{id}");
        }

        public async Task<ArtistaResponse?> GetArtistaPorNomeAsync(string nome)
        {
            return await _httpClient.GetFromJsonAsync<ArtistaResponse>($"Artistas/{nome}");
        }


    }
}
