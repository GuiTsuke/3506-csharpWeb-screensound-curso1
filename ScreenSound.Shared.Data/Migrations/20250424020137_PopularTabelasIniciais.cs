using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Shared.Data.Migrations
{
    /// <inheritdoc />
    public partial class PopularTabelasIniciais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Inserir Artistas
            migrationBuilder.InsertData(
                table: "Artistas",
                columns: new[] { "Id", "Nome", "FotoPerfil", "Bio" },
                values: new object[,]
                {
            { 1, "Queen", "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png", "Banda britânica de rock formada em 1970." },
            { 2, "Michael Jackson", "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png", "Rei do Pop." }
                });

            // Inserir Gêneros
            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "Nome", "Descricao" },
                values: new object[,]
                {
            { 1, "Rock", "Gênero musical enérgico com guitarras." },
            { 2, "Pop", "Músicas populares e comerciais." }
                });

            // Inserir Músicas
            migrationBuilder.InsertData(
                table: "Musicas",
                columns: new[] { "Id", "Nome", "AnoLancamento", "ArtistaId" },
                values: new object[,]
                {
            { 1, "Bohemian Rhapsody", 1975, 1 },
            { 2, "Thriller", 1982, 2 }
                });

            // Inserir relação muitos-para-muitos
            migrationBuilder.InsertData(
                table: "GeneroMusica",
                columns: new[] { "GenerosId", "MusicasId" },
                values: new object[,]
                {
            { 1, 1 }, // Rock ↔ Bohemian Rhapsody
            { 2, 2 }  // Pop ↔ Thriller
                });
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("GeneroMusica", new[] { "GenerosId", "MusicasId" }, new object[] { 1, 1 });
            migrationBuilder.DeleteData("GeneroMusica", new[] { "GenerosId", "MusicasId" }, new object[] { 2, 2 });

            migrationBuilder.DeleteData("Musicas", "Id", 1);
            migrationBuilder.DeleteData("Musicas", "Id", 2);

            migrationBuilder.DeleteData("Artistas", "Id", 1);
            migrationBuilder.DeleteData("Artistas", "Id", 2);

            migrationBuilder.DeleteData("Generos", "Id", 1);
            migrationBuilder.DeleteData("Generos", "Id", 2);
        }
    }
}
