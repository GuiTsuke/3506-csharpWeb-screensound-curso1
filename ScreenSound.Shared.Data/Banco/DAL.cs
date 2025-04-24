using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScreenSound.Shared.Models;

namespace ScreenSound.Shared.Banco;

public class DAL<T> where T : class
{
    private readonly ScreenSoundContext _context;

    public DAL(ScreenSoundContext context)
    {
        _context = context;
    }

    public IEnumerable<T> Listar()
    {
        return _context.Set<T>().ToList();
    }

    public void Adicionar(T objeto)
    {
        _context.Set<T>().Add(objeto);
        _context.SaveChanges();
    }

    public void Atualizar(T objeto)
    {
        _context.Set<T>().Update(objeto);
        _context.SaveChanges();
    }
    public void Deletar(T objeto)
    {
        _context.Set<T>().Remove(objeto);
        _context.SaveChanges();
    }

    public T? ListarPor(Func<T, bool> condicao)
    {
        return _context.Set<T>().FirstOrDefault(condicao);
    }
}
