
using LojaQuadrinhos.Model.Entidades;
using LojaQuadrinhos.Model.Interfaces;
using LojaQuadrinhos.WebApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LojaQuadrinhos.Data.Repositorios
{
    public class ProdutoRepositorio : Repositorio<Produto>, IProdutoRepositorio
    {

        private readonly LojaQuadrinhosContext _context;
        private readonly DbSet<Produto> _dbSet;
        public ProdutoRepositorio(LojaQuadrinhosContext context)
            : base(context)
        {
            this._context = context;
            this._dbSet = this._context.Set<Produto>();
        }
        /// <summary>
        /// Verifica se a categoria esta vinculada a um produto.
        /// Se estiver, a categoria não pode ser excluida
        /// </summary>
        public bool CategoriaProduto(int id)
        {
            return _dbSet.Any(x => x.CategoriaId == id);
            
        }

        public IEnumerable<Produto> GetProdutosAtivos()
        {
            return _dbSet.Where(x => x.Quantidade > 0);
        }
    }
}
