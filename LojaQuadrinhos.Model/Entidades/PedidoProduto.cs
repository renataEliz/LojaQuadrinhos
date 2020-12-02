
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaQuadrinhos.Model.Entidades
{
    public class PedidoProduto
    {
        public int ID { get; set; }
        public int PedidoId { get; set; }
        public int ProdutoID { get; set; }
        public int QuantidadeProduto { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal PrecoTotalProduto { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal PrecoUnitario { get; set; }
    }
}
