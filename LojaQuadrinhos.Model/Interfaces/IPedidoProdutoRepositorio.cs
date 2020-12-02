using LojaQuadrinhos.Model.Entidades;
using System.Collections.Generic;

namespace LojaQuadrinhos.Model.Interfaces
{
    public interface IPedidoProdutoRepositorio : IRepositorio<PedidoProduto>
    {
        List<PedidoProduto> BuscaProdutosPedidos(int id);
    }
}
