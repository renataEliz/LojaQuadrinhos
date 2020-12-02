
using Microsoft.AspNetCore.Mvc;
using LojaQuadrinhos.WebApp.Data;
using LojaQuadrinhos.Business;
using LojaQuadrinhos.Data;
using LojaQuadrinhos.Model.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;


namespace LojaQuadrinhos.WebApp.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly ProdutoBusiness _produtoBusiness;
        private UnitOfWork unitOfWork;

        public ProdutosController(LojaQuadrinhosContext context)
        {
            unitOfWork = new UnitOfWork(context);
            _produtoBusiness = new ProdutoBusiness(unitOfWork);
        }

        public IActionResult Index()
        {
            return View(_produtoBusiness.ListarTodos());
        }

        public IActionResult Detalhes(int id)
        {
            ProdutoDto produtoDto = _produtoBusiness.BuscarPorId(id);
            if (produtoDto == null)
            {
                return NotFound();
            }
            return View(produtoDto);
        }

        public IActionResult Criar(int id)
        {
            ViewData["Categorias"] = new SelectList(CarregarCategorias(), "categoriaId", "Descricao", false);
            if (id > 0)
            {
                var produtoDto = _produtoBusiness.BuscarPorId(id);
                if (produtoDto == null)
                {
                    return NotFound();
                }
                return View(produtoDto);

            }

            return View();
        }

        [HttpPost, ActionName("Criar")]
        [ValidateAntiForgeryToken]
        public IActionResult Salvar(int id, [Bind("ID,Titulo,Quantidade,Preco,Detalhes,Categoria,Serie,Editora,TipoDeCapa,Idioma,paginas,Autor,Paginas,Ano,CategoriaId")] ProdutoDto produtoDto)
        {
            if (ModelState.IsValid)
            {
                _produtoBusiness.Salvar(produtoDto);

                return RedirectToAction(nameof(Index));
            }
            ViewData["Categorias"] = new SelectList(CarregarCategorias(), "categoriaId", "Descricao", produtoDto.CategoriaId);
            return View(produtoDto);
        }
        public IActionResult Excluir(int id)
        {
            ProdutoDto produtoDto = _produtoBusiness.BuscarPorId(id);
            if (produtoDto == null)
            {
                return NotFound();
            }
            return View(produtoDto);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public IActionResult ExcluirConfirma(int id)
        {

            bool retorno = _produtoBusiness.Excluir(id);

            if (retorno)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("Produto", "Não é possível excluir produto, pois ela esta vinculada a um pedido");
            return View();
        }
        private IEnumerable<CategoriaDto> CarregarCategorias()
        {
            return _produtoBusiness.BuscarCategorias().ToList();

        }
    }
}
