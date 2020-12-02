
using System.ComponentModel.DataAnnotations;

namespace LojaQuadrinhos.Model.Entidades
{
    public class Categoria
    {
        public int ID { get; set; }

        [Required]
        public string Descricao { get; set; }
    }
}
