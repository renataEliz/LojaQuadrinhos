

namespace LojaQuadrinhos.Model.Interfaces
{
    public interface IUnitOfwork
    {
        ICategoriaRepositorio CategoriaRepositorio { get; }

        IProdutoRepositorio ProdutoRepositorio { get; }

        IPedidoRepositorio PedidoRepositorio { get; }

        IPedidoProdutoRepositorio PedidoProdutoRepositorio { get; }

        void Commit();

        void Dispose();
    }
}
