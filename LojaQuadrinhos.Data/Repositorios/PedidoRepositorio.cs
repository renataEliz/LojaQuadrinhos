using LojaQuadrinhos.Model.Entidades;
using LojaQuadrinhos.Model.Interfaces;
using LojaQuadrinhos.WebApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LojaQuadrinhos.Data.Repositorios
{
    public  class PedidoRepositorio : Repositorio<Pedido>, IPedidoRepositorio
    {

        private readonly LojaQuadrinhosContext _context;
        private readonly DbSet<Pedido> _dbSet;
        public PedidoRepositorio(LojaQuadrinhosContext context)
           : base(context)
        {
            this._context = context;
            this._dbSet = this._context.Set<Pedido>();
        }

        public bool ProdutoPedido(int id)
        {
            return _dbSet.Any(x => x.pedidoProdutos.Any(y => y.ProdutoID == id));
        }
    }
}
