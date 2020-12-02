
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaQuadrinhos.Model.Dtos
{
   
    public class PedidoProdutoDto
    {

        public int ID { get; set; }
        public int PedidoId { get; set; }

        [DisplayName("Código")]
        public int ProdutoID { get; set; }

        [DisplayName("Quantidade")]
        public int QuantidadeDeProduto { get; set; }

        [DisplayName("Titulo")]
        public string Titulo { get; set; }

        [DisplayName("Preço Unitário")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal PrecoUnitario { get; set; }

        [DisplayName("Preço Total")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal PrecoTotal { get; set; }
    }
}
