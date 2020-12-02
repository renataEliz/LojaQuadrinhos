using LojaQuadrinhos.Model.Entidades;
using System.ComponentModel;


namespace LojaQuadrinhos.Model.Dtos
{
    public class ProdutoIndexDto
    {
        public int ID { get; set; }

        [DisplayName("Título")] 
        public string Titulo { get; set; }

        [DisplayName("Quantidade")] 
        public int Quantidade { get; set; }

        [DisplayName("Preço")]
        public decimal Preco { get; set; }

        [DisplayName("Categoria")]
        public Categoria Categoria { get; set; }
    }
}
