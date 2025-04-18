using System.Data.SqlClient;
using ScreenSound.Modelos;

namespace ScreenSound.Banco
{
    internal class ArtistaDAL
    {       
        private readonly ScreenSoundContext _context;

        public ArtistaDAL(ScreenSoundContext context)
        {
            _context = context;
        }

        public IEnumerable<Artista> Listar()
        {            
            return _context.Artistas.ToList();
        }

        public Artista ListarPorId(string nome)
        {
            return _context.Artistas.FirstOrDefault(a => a.Nome.Equals(nome));
        }

        public void Adicionar(Artista artista)
        {
            _context.Artistas.Add(artista);
            _context.SaveChanges();
            Console.WriteLine($"Artista {artista.Nome} adicionado com sucesso!");
        }

        public void Atualizar(Artista artista)
        {
            _context.Artistas.Update(artista);
            _context.SaveChanges();
            Console.WriteLine($"Artista {artista.Nome} atualizado com sucesso!");
        }

        public void Deletar(Artista artista)
        {
            _context.Artistas.Remove(artista);
            _context.SaveChanges();
            Console.WriteLine($"Artista {artista.Nome} removido com sucesso!");
        }

        //public IEnumerable<Artista> Listar()
        //{
        //    var lista = new List<Artista>();
        //    using var connection = new ScreenSoundContext().ObterConexão();
        //    connection.Open();

        //    string sql = "SELECT * FROM Artistas";
        //    SqlCommand command = new SqlCommand(sql, connection);
        //    using SqlDataReader reader = command.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        string nomeArtista = Convert.ToString(reader["Nome"]);
        //        string bioArtista = Convert.ToString(reader["Bio"]);
        //        int idArtista = Convert.ToInt32(reader["Id"]);
        //        Artista artista = new (nomeArtista, bioArtista) { Id = idArtista};

        //        lista.Add(artista);
        //    }

        //    return lista;
        //}

        //public void Adicionar(Artista artista)
        //{
        //   using var connection = new ScreenSoundContext().ObterConexão();
        //    connection.Open();

        //    string sql = "INSERT INTO Artistas (Nome, FotoPerfil, Bio) VALUES (@nome, @perfilPadrao, @bio)";
        //    SqlCommand command = new SqlCommand(sql, connection);

        //    command.Parameters.AddWithValue("@nome", artista.Nome);
        //    command.Parameters.AddWithValue("@perfilPadrao", artista.FotoPerfil);
        //    command.Parameters.AddWithValue("@bio", artista.Bio);

        //    int retorno = command.ExecuteNonQuery();
        //    Console.WriteLine($"Artista {artista.Nome} adicionado com sucesso! {retorno} linha(s) afetada(s).");
        //}
    }
}
