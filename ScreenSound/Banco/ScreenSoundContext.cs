using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;

namespace ScreenSound.Banco;

internal class ScreenSoundContext : DbContext
{
    private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ScreenSoundV0;Integrated Security=True";

    public DbSet<Artista> Artistas{ get; set; }
    public DbSet<Musica> Musicas{ get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(connectionString)
            .UseLazyLoadingProxies();
    }
    //public SqlConnection ObterConexão()
    //{
    //    return new SqlConnection(connectionString);
    //}        
}
