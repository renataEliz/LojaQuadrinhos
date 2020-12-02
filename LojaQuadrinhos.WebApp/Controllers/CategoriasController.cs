
using Microsoft.AspNetCore.Mvc;
using LojaQuadrinhos.Model.Dtos;
using LojaQuadrinhos.Business;
using LojaQuadrinhos.Data;
using LojaQuadrinhos.WebApp.Data;

namespace LojaQuadrinhos.WebApp.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly CategoriaBusiness _categoriaBusiness;
        private UnitOfWork unitOfWork;

        public CategoriasController(LojaQuadrinhosContext context)
        {
            unitOfWork = new UnitOfWork(context);
            _categoriaBusiness = new CategoriaBusiness(unitOfWork);
        }

        public IActionResult Index()
        {
            return View(_categoriaBusiness.ListarTodos());
        }

        public IActionResult Criar(int id)
        {
            if(id > 0)
            {
                var categoriaDto = _categoriaBusiness.BuscarPorId(id);
                if (categoriaDto == null)
                {
                    return NotFound();
                }
                return View(categoriaDto);
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Criar")]
        public IActionResult Salvar(int id, [Bind("ID,Descricao")] CategoriaDto categoriaDto)
        {

            if (ModelState.IsValid)
            {
                _categoriaBusiness.Salvar(categoriaDto);

                return RedirectToAction(nameof(Index));
            }
            return View(categoriaDto);
        }

        public IActionResult Excluir(int id)
        {
            CategoriaDto categoriaDto = _categoriaBusiness.BuscarPorId(id);
            if (categoriaDto == null)
            {
                return NotFound();
            }
            return View(categoriaDto);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public IActionResult ExcluirConfirma(int id)
        {

            bool retorno = _categoriaBusiness.Excluir(id);

            if (retorno)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("Categoria", "Não é possível excluir categoria, pois ela esta vinculada a um produto");
            return View();

        }

    }
}
