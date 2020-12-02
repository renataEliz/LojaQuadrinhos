using LojaQuadrinhos.Model.Entidades;

namespace LojaQuadrinhos.Model.Interfaces
{
    public interface IPedidoRepositorio : IRepositorio<Pedido>
    {
        bool ProdutoPedido(int id);
    }
}
