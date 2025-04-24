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

        app.MapPost("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] ArtistaRequest artistaRequest) =>
        {
            var artista = new Artista(artistaRequest.Nome, artistaRequest.Bio);

            dal.Adicionar(artista);
            return Results.Ok();
        });

        app.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id) =>
        {
            var artista = dal.ListarPor(a => a.Id.Equals(id));
            if (artista is null)
                return Results.NotFound("Artista não encontrado!");
            dal.Deletar(artista);
            return Results.NoContent();
        });

        app.MapPut("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] ArtistaRequestEdit artistaRequest) => {

            var artistaBuscado = dal.ListarPor(a => a.Id.Equals(artistaRequest.Id));
            if (artistaBuscado is null)
                return Results.NotFound("Artista inválido!");

            artistaBuscado.Nome = artistaRequest.Nome;            
            artistaBuscado.Bio = artistaRequest.Bio;
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
