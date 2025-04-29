using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Shared.Banco;
using ScreenSound.Shared.Models;

namespace ScreenSound.API.Endpoints;

public static class GenerosExtensions
{
    public static void AddEndpointsGeneros(this WebApplication app)
    {
        app.MapGet("/Generos", ([FromServices] DAL<Genero> dal) =>
        {
            var listaGeneros = dal.Listar();
            if (listaGeneros is null)
                return Results.NotFound("Nenhum gênero encontrado!");

            var listaGenerosResponse = EntityListToResponseList(listaGeneros);
            return Results.Ok(listaGenerosResponse);
        });

        app.MapGet("/Generos/{nome}", ([FromServices] DAL<Genero> dal, string nome) =>
        {
            var genero = dal.ListarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));

            if (genero is null)
                return Results.NotFound("Gênero não Encontrado!");
            return Results.Ok(genero);
        });

        app.MapPost("/Generos", ([FromServices] DAL<Genero> dal, [FromBody] GeneroRequest generoRequest) =>
        {            
            dal.Adicionar(RequestToEntity(generoRequest));
            return Results.Ok();
        });

        app.MapDelete("/Generos/{id}", ([FromServices] DAL<Genero> dal, int id) =>
        {
            var genero = dal.ListarPor(a => a.Id.Equals(id));
            if (genero is null)
                return Results.NotFound("Gênero não encontrado!");
            dal.Deletar(genero);
            return Results.NoContent();
        });        
    }

    private static Genero RequestToEntity(GeneroRequest generoRequest)
    {
        return new Genero()
        {
            Nome = generoRequest.Nome,
            Descricao = generoRequest.Descricao,
        };
    }

    private static ICollection<GeneroResponse> EntityListToResponseList(IEnumerable<Genero> listaGeneros)
    {
        return listaGeneros.Select(g => EntityToResponse(g)).ToList();
    }

    private static GeneroResponse EntityToResponse(Genero genero)
    {
        return new GeneroResponse(genero.Id, genero.Nome!, genero.Descricao!);
    }
}
