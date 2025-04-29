using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Shared.Banco;
using ScreenSound.Shared.Models;

namespace ScreenSound.API.Endpoints;

public static class ArtistasExtensions
{
    public static void AddEndpointsArtistas(this WebApplication app)
    {
        app.MapGet("/Artistas", ([FromServices] DAL<Artista> dal) =>
        {
            var listaArtistas = dal.Listar();
            if (listaArtistas is null)
                return Results.NotFound("Nenhum artista encontrado!");

            var listaArtistasResponse = EntitListToResponseList(listaArtistas);
            return Results.Ok(listaArtistasResponse);
        });

        app.MapGet("/Artistas/{nome}", ([FromServices] DAL<Artista> dal, string nome) =>
        {
            var artista = dal.ListarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
            if (artista is null)
                return Results.NotFound();
            return Results.Ok(EntityToResponse(artista));
        });

        app.MapPost("/Artistas", async ([FromServices] IHostEnvironment env, [FromServices] DAL<Artista> dal, [FromBody] ArtistaRequest artistaRequest) =>
        {
            var nome = artistaRequest.nome.Trim();
            var imagemArtista = DateTime.Now.ToString("ddMMyyyyhhmmss") + "." + nome + ".jpeg";

            var path = Path.Combine(env.ContentRootPath, "wwwroot", "FotosPerfil", imagemArtista);

            var artista = new Artista(artistaRequest.nome, artistaRequest.bio)
            {
                FotoPerfil = $"/FotosPerfil/cardArtista.png"
            };

            if (artistaRequest.fotoPerfil is not null)
            {
                using MemoryStream stream = new MemoryStream(Convert.FromBase64String(artistaRequest.fotoPerfil));
                using FileStream fileStream = new(path, FileMode.Create);
                await stream.CopyToAsync(fileStream);
                artista.FotoPerfil = $"/FotosPerfil/{imagemArtista}";
            }

            dal.Adicionar(artista);
            return Results.Ok();
        });

        app.MapDelete("/Artistas/{id}", ([FromServices] IHostEnvironment env, [FromServices] DAL<Artista> dal, int id) =>
        {
            var artista = dal.ListarPor(a => a.Id.Equals(id));
            if (artista is null)
                return Results.NotFound("Artista não encontrado!");



            var parts = artista.FotoPerfil!.TrimStart('/').Split('/');
            var pathAntigo = Path.Combine(env.ContentRootPath, "wwwroot", "FotosPerfil", parts[1]);

            if (File.Exists(pathAntigo) && !parts[1].Equals("cardArtista.png"))
                File.Delete(pathAntigo);

            dal.Deletar(artista);
            return Results.NoContent();
        });

        app.MapPut("/Artistas", async ([FromServices] IHostEnvironment env, [FromServices] DAL<Artista> dal, [FromBody] ArtistaRequestEdit artistaRequest) =>
        {

            var artistaBuscado = dal.ListarPor(a => a.Id.Equals(artistaRequest.Id));
            if (artistaBuscado is null)
                return Results.NotFound("Artista inválido!");

            if (artistaRequest.fotoPerfil is not null)
            {
                var nome = artistaRequest.nome.Trim();
                var imagemArtista = DateTime.Now.ToString("ddMMyyyyhhmmss") + "." + nome + ".jpeg";

                var parts = artistaBuscado.FotoPerfil!.TrimStart('/').Split('/');
                var pathAntigo = Path.Combine(env.ContentRootPath, "wwwroot", "FotosPerfil", parts[1]);
                var pathAtual = Path.Combine(env.ContentRootPath, "wwwroot", "FotosPerfil", imagemArtista);

                if (File.Exists(pathAntigo) && !parts[1].Equals("cardArtista.png"))
                    File.Delete(pathAntigo);

                using MemoryStream stream = new MemoryStream(Convert.FromBase64String(artistaRequest.fotoPerfil));
                using FileStream fileStream = new(pathAtual, FileMode.Create);
                await stream.CopyToAsync(fileStream);
                artistaBuscado.FotoPerfil = $"/FotosPerfil/{imagemArtista}";
            }
            
            artistaBuscado.Nome = artistaRequest.nome;
            artistaBuscado.Bio = artistaRequest.bio;
            artistaBuscado.Bio = artistaRequest.bio;
            dal.Atualizar(artistaBuscado);
            return Results.Ok();
        });
    }

    private static ICollection<ArtistaResponse> EntitListToResponseList(IEnumerable<Artista> listaArtistas)
    {
        return listaArtistas.Select(a => EntityToResponse(a)).ToList();
    }

    private static ArtistaResponse EntityToResponse(Artista artista)
    {
        return new ArtistaResponse(artista.Id, artista.Nome, artista.Bio, artista.FotoPerfil);
    }
}
