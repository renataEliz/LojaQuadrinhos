
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LojaQuadrinhos.Model.Dtos
{
    public class CategoriaDto
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatório", AllowEmptyStrings = false)]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        public string categoriaId { get; set; }

    }
}
