using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ScreenSound.API.Endpoints;
using ScreenSound.Shared.Banco;
using ScreenSound.Shared.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ScreenSoundContext>((options) =>
{
    options
    .UseNpgsql(builder.Configuration["ConnectionStrings:DefaultConnection"])
    .UseLazyLoadingProxies();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowGuitsuke", policy =>
    {
        policy.WithOrigins("https://guitsuke.github.io") // Substitua pelo seu domínio
              .AllowAnyHeader() // Permite qualquer cabeçalho
              .AllowAnyMethod(); // Permite qualquer método
    });
});

builder.Services.AddTransient<DAL<Artista>>();
builder.Services.AddTransient<DAL<Musica>>();
builder.Services.AddTransient<DAL<Genero>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

app.UseStaticFiles();

app.UseCors("AllowGuitsuke");

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();

app.AddEndpointsArtistas();
app.AddEndpointsMusicas();
app.AddEndpointsGeneros();

app.Run();
