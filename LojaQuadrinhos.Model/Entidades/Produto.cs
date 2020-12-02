
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaQuadrinhos.Model.Entidades
{
    public class Produto
    {
        public int ID { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Preco { get; set; }

        public string Autor { get; set; }

        [Required]
        public Categoria Categoria { get; set; }

        [ColumnAttribute("CategoriaID")]
        public int CategoriaId { get; set; }

        public string Serie { get; set; }

        public string Editora { get; set; }


        public string Idioma { get; set; }

        [Required]
        public int Paginas { get; set; }

        [Required]
        public string Ano { get; set; }
    }
}
