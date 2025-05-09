﻿using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Shared.Banco;
using ScreenSound.Shared.Models;

namespace ScreenSound.API.Endpoints;

public static class MusicasExtensions
{
    public static void AddEndpointsMusicas(this WebApplication app)
    {
        app.MapGet("/Musicas", ([FromServices] DAL<Musica> dal) =>
        {
            var listaMusicas = dal.Listar();
            if (listaMusicas is null)
                return Results.NotFound("Nenhuma música encontrada!");

            var listaMusicasResponse = EntityListToResponseList(listaMusicas);
            return Results.Ok(listaMusicasResponse);
        });

        app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> dal, string nome) =>
        {
            var musica = dal.ListarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));

            if (musica is null)
                return Results.NotFound();

            return Results.Ok(EntityToResponse(musica));
        });

        app.MapPost("/Musicas", ([FromServices] DAL<Musica> dal, [FromServices] DAL<Genero> dalGenero, [FromBody] MusicaRequest musicaRequest) =>
        {
            var musica = new Musica(musicaRequest.Nome)
            {
                ArtistaId = musicaRequest.ArtistaId,
                AnoLancamento = musicaRequest.AnoLancamento,
                Generos = musicaRequest.Generos is not null? GeneroRequestConverter(musicaRequest.Generos, dalGenero): [],
            };
            dal.Adicionar(musica);
            return Results.Ok();
        });

        app.MapDelete("/Musicas/{id}", ([FromServices] DAL<Musica> dal, int id) =>
        {
            var musica = dal.ListarPor(a => a.Id.Equals(id));
            if (musica is null)
                return Results.NotFound("Música não encontrada!");
            dal.Deletar(musica);
            return Results.NoContent();
        });

        app.MapPut("/Musicas", ([FromServices] DAL<Musica> dal, [FromServices] DAL < Genero > dalGenero, [FromBody] MusicaRequestEdit musica) =>
        {
            var musicaBuscada = dal.ListarPor(a => a.Id.Equals(musica.Id));
            if (musicaBuscada is null)
                return Results.NotFound("Música inválida!");

            musicaBuscada.Nome = musica.Nome;
            musicaBuscada.AnoLancamento = musica.AnoLancamento;
            musicaBuscada.ArtistaId = musica.ArtistaId;

            musicaBuscada.Generos?.Clear();
            if(musica.Generos is not null)
            {
                var novosGeneros = GeneroRequestConverter(musica.Generos, dalGenero);
                foreach(var genero in novosGeneros)
                {
                    musicaBuscada.Generos?.Add(genero);
                }
            }            

            dal.Atualizar(musicaBuscada);
            return Results.Ok();
        });
    }

    private static ICollection<MusicaResponse> EntityListToResponseList(IEnumerable<Musica> listaMusicas)
    {
        return listaMusicas.Select(m => EntityToResponse(m)).ToList();
    }

    private static MusicaResponse EntityToResponse(Musica musica)
    {
        return new MusicaResponse(musica.Id, musica.Nome!, musica.Artista!.Id, musica.Artista.Nome!, musica.AnoLancamento, musica.Artista.FotoPerfil!, [.. musica.Generos!.Select(g => g.Id)]);
    }

    private static ICollection<Genero> GeneroRequestConverter(ICollection<GeneroRequest> generos, DAL<Genero> dalGenero)
    {
        var listaDeGeneros = new List<Genero>();
        foreach (var item in generos)
        {
            var genero = dalGenero.ListarPor(g => g.Nome.ToUpper().Equals(item.Nome.ToUpper()));
            Genero generoAdicionado;

            
            if (genero is not null)
            {
                generoAdicionado = genero;
            }
            else
            {
                generoAdicionado = new Genero()
                {
                    Nome = item.Nome,
                    Descricao = item.Descricao
                };
            }
            if(!listaDeGeneros.Any(g => g.Nome.ToUpper() == generoAdicionado.Nome!.ToUpper()))
            {
                listaDeGeneros.Add(generoAdicionado);
            }
        }

        return listaDeGeneros;
    }

    private static Genero RequestToEntity(GeneroRequest genero)
    {
        return new Genero() { Nome = genero.Nome, Descricao = genero.Descricao };
    }
}
