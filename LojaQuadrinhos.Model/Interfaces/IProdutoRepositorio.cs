using LojaQuadrinhos.Model.Entidades;
using System.Collections.Generic;

namespace LojaQuadrinhos.Model.Interfaces
{
    public interface IProdutoRepositorio : IRepositorio<Produto>
    {
        bool CategoriaProduto(int id);

        IEnumerable<Produto> GetProdutosAtivos();
    }
}