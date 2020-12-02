
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LojaQuadrinhos.Model.Dtos
{
    public class PedidoDto
    {

        [DisplayName("Número do Pedido")]
        public int ID { get; set; }

        [DisplayName("Usuário")]
        public string Usuario { get; set; }

        [DisplayName("Data do Pedido")]
        [DataType(DataType.Date)]
        public DateTime DataDoPedido { get; set; }

        [DisplayName("Valor Total")]
        public decimal ValorTotal { get; set; }
        public int Quantidade { get; set; }
        
        [DisplayName("Produtos")]
        public string ProdutoId { get; set; }

        public List<PedidoProdutoDto> PedidoProdutosDto { get; set; }

        public bool Sucesso;

        public string QtdDisponivelEstoque;

    }
}
