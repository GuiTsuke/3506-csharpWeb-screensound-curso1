using Microsoft.EntityFrameworkCore;
using ScreenSound.Shared.Models;

namespace ScreenSound.Shared.Banco;

public class ScreenSoundContext : DbContext
{
    private string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=123456;Database=ScreenSound;";

    public DbSet<Artista> Artistas{ get; set; }
    public DbSet<Musica> Musicas{ get; set; }
    public DbSet<Genero> Generos{ get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(connectionString)
            .UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Musica>()
            .HasMany(c => c.Generos)
            .WithMany(c => c.Musicas);
    }
    //public SqlConnection ObterConexão()
    //{
    //    return new SqlConnection(connectionString);
    //}        
}
