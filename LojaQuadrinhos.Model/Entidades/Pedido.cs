using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaQuadrinhos.Model.Entidades
{
   public class Pedido
    {
        public int ID { get; set; } 
        public virtual ICollection<PedidoProduto> pedidoProdutos { get; set; }

        public string Usuario { get; set; }

        public DateTime DataDoPedido { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal ValorTotal { get; set; } 

    }
}
