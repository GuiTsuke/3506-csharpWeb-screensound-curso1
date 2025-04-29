using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ScreenSound.API.Endpoints;
using ScreenSound.Shared.Banco;
using ScreenSound.Shared.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.WithOrigins("https://localhost:7274")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<ScreenSoundContext>((options) =>
{
    options
    .UseNpgsql(builder.Configuration["ConnectionStrings:DefaultConnection"])
    .UseLazyLoadingProxies();
});
builder.Services.AddTransient<DAL<Artista>>();
builder.Services.AddTransient<DAL<Musica>>();
builder.Services.AddTransient<DAL<Genero>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();
app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();

app.UseCors(options =>
{
    options.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
});

app.UseStaticFiles();

app.AddEndpointsArtistas();
app.AddEndpointsMusicas();
app.AddEndpointsGeneros();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
