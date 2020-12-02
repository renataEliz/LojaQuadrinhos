

using LojaQuadrinhos.Model.Entidades;
using LojaQuadrinhos.Model.Interfaces;
using LojaQuadrinhos.WebApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LojaQuadrinhos.Data.Repositorios
{
    public class PedidoProdutoRepositorio : Repositorio<PedidoProduto>, IPedidoProdutoRepositorio
    {

        private readonly LojaQuadrinhosContext _context;
        private readonly DbSet<PedidoProduto> _dbSet;
        public PedidoProdutoRepositorio(LojaQuadrinhosContext context)
          : base(context)
        {
            this._context = context;
            this._dbSet = this._context.Set<PedidoProduto>();
        }

        public List<PedidoProduto> BuscaProdutosPedidos(int id)
        {
            return _dbSet.Where(x => x.PedidoId == id).ToList();

        }
    }
}
