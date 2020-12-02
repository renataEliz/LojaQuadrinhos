using LojaQuadrinhos.Model.Entidades;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LojaQuadrinhos.Model.Dtos
{
    public class ProdutoDto
    {
        public int ID { get; set; }

        [DisplayName("Título")]
        [Required(ErrorMessage = "Título é obrigatório", AllowEmptyStrings = false)]
        public string Titulo { get; set; }

        [DisplayName("Quantidade")]
        [Required(ErrorMessage = "Quantidade é obrigatório")]
        public int Quantidade { get; set; }

        [DisplayName("Preço")]
        [Required(ErrorMessage = "Preço é obrigatório")]
        [DataType(DataType.Currency)]
        public decimal Preco { get; set; }

        [DisplayName("Autor")]
        public string Autor { get; set; }

        public Categoria Categoria { get; set; }

        [DisplayName("Categoria")]
        [Required(ErrorMessage = "Categoria é obrigatório")]
        public string CategoriaId { get; set; }

        [DisplayName("Série")]
        public string Serie { get; set; }

        [DisplayName("Editora")]
        [Required(ErrorMessage = "Editora é obrigatório")]
        public string Editora { get; set; }

        [DisplayName("Idioma")]
        [Required(ErrorMessage = "Idioma é obrigatório")]
        public string Idioma { get; set; }

        [DisplayName("Número de páginas")]
        [Required(ErrorMessage = "Número de páginas é obrigatório")]
        public int Paginas { get; set; }

        [DisplayName("Ano")]
        [Required(ErrorMessage = "Ano é obrigatório")]
        public string Ano { get; set; }
    }
}
