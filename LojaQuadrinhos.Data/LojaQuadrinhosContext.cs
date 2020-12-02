
using Microsoft.EntityFrameworkCore;
using LojaQuadrinhos.Model.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LojaQuadrinhos.WebApp.Data
{
    public class LojaQuadrinhosContext : IdentityDbContext
    {

        public LojaQuadrinhosContext(DbContextOptions<LojaQuadrinhosContext> options)
            : base(options)
        {
        }

        public LojaQuadrinhosContext() { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }

        public DbSet<PedidoProduto> PedidoProdutos { get;set;}

    }
}
