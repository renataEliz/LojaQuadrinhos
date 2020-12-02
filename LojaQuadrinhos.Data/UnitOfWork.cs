using LojaQuadrinhos.Data.Repositorios;
using LojaQuadrinhos.Model.Interfaces;
using LojaQuadrinhos.WebApp.Data;
using System;


namespace LojaQuadrinhos.Data
{
    public class UnitOfWork : IUnitOfwork, IDisposable
    {
        public LojaQuadrinhosContext Context { get; internal set; }

        private IProdutoRepositorio _produtoRepositorio;
        private ICategoriaRepositorio _categoriaRepositorio;
        private IPedidoRepositorio _pedidoRepositorio;
        private IPedidoProdutoRepositorio _pedidoProdutoRepositorio;

        public UnitOfWork(LojaQuadrinhosContext context)
        {
            this.Context = context;
        }
       
        public IProdutoRepositorio ProdutoRepositorio
        {
            get
            {
                return _produtoRepositorio = _produtoRepositorio ?? new ProdutoRepositorio(Context);
            }
        }

        public ICategoriaRepositorio CategoriaRepositorio
        {
            get
            {
                return _categoriaRepositorio = _categoriaRepositorio ?? new CategoriaRepositorio(Context);
            }
        }

        public IPedidoRepositorio PedidoRepositorio
        {
            get
            {
                return _pedidoRepositorio = _pedidoRepositorio ?? new PedidoRepositorio(Context);
            }
        }

        public IPedidoProdutoRepositorio PedidoProdutoRepositorio
        {
            get
            {
                return _pedidoProdutoRepositorio = _pedidoProdutoRepositorio ?? new PedidoProdutoRepositorio(Context);
            }
        }


        public void Commit()
        {
            Context.SaveChanges();
        }
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
