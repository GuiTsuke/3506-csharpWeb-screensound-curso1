using ScreenSound.Shared.Banco;
using ScreenSound.Shared.Models;

namespace ScreenSound.Menus;

internal class MenuSair : Menu
{
    public override void Executar(DAL<Artista> artistaDAL)
    {
        Console.WriteLine("Tchau tchau :)");
    }
}
