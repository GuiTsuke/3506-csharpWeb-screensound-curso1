using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;

namespace ScreenSound.Banco
{
    internal class ScreenSoundContext : DbContext
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ScreenSound;Integrated Security=True";

        public DbSet<Artista> Artistas{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
        //public SqlConnection ObterConexão()
        //{
        //    return new SqlConnection(connectionString);
        //}        
    }
}
