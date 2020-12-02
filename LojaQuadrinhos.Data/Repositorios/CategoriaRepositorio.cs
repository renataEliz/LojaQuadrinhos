
using LojaQuadrinhos.Model.Entidades;
using LojaQuadrinhos.Model.Interfaces;
using LojaQuadrinhos.WebApp.Data;


namespace LojaQuadrinhos.Data.Repositorios
{
    public class CategoriaRepositorio : Repositorio<Categoria>, ICategoriaRepositorio
    {
        public CategoriaRepositorio(LojaQuadrinhosContext context)
           : base(context)
        {
        }
    }
}
