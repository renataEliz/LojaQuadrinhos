
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LojaQuadrinhos.WebApp.Data;
using LojaQuadrinhos.Model.Dtos;
using LojaQuadrinhos.Business;
using LojaQuadrinhos.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Security.Claims;
using System;


namespace LojaQuadrinhos.WebApp.Controllers
{
    public class PedidosController : Controller
    {
        private readonly PedidoBusiness _pedidoBusiness;
        private UnitOfWork unitOfWork;
        private readonly LojaQuadrinhosContext _context;

        public PedidosController(LojaQuadrinhosContext context)
        {
            _context = context;
            unitOfWork = new UnitOfWork(context);
            _pedidoBusiness = new PedidoBusiness(unitOfWork);
        }

        public IActionResult Index()
        {
            return View(_pedidoBusiness.ListarTodos());
        }
        public IActionResult Criar()
        {
            var userName = User.FindFirstValue(ClaimTypes.Name);
            PedidoDto pedidoDto = new PedidoDto()
            {
                Usuario = userName,
                DataDoPedido = DateTime.Now.Date,
                Quantidade = 1,
                PedidoProdutosDto = new List<PedidoProdutoDto>(),
            };
            ViewData["Produtos"] = new SelectList(CarregarProdutosAtivos(), "ProdutoId", "Titulo", false);

            return View(pedidoDto);
        }

        [HttpPost, ActionName("Criar")]
        [ValidateAntiForgeryToken]
        public IActionResult Salvar([Bind("ID,Usuario,DataDoPedido,ValorTotal,Quantidade,ProdutoId,PedidoProdutosDto")] PedidoDto pedido)
        {
            if (ModelState.IsValid)
            {
                _pedidoBusiness.Salvar(pedido);

                return RedirectToAction(nameof(Index));
            }
            ViewData["Produtos"] = new SelectList(CarregarProdutosAtivos(), "ProdutoId", "Titulo", false);

            return View(pedido);
        }

        [HttpPost]
        public IActionResult AdicionarProdutoAoPedido([Bind("ID,Usuario,DataDoPedido,ValorTotal,Quantidade,ProdutoId,PedidoProdutosDto")] PedidoDto pedido)
        {
            ViewData["Produtos"] = new SelectList(CarregarProdutosAtivos(), "ProdutoId", "Titulo", false);

            if (pedido.PedidoProdutosDto is null)
            {
                pedido.PedidoProdutosDto = new List<PedidoProdutoDto>();
            }
            
            var retorno = _pedidoBusiness.BuscarProdutoDoPedido(pedido);

            if (pedido.Sucesso)
            {
                ModelState.Clear();

                retorno.Quantidade = 1;
                return View("Criar", retorno);
            }
            else
            {
                ModelState.AddModelError("Produto", "O item selecionado possui apenas " + retorno.QtdDisponivelEstoque + " disponível");
                return View("Criar", pedido);             
            }

        }

        public IActionResult ExcluirProdutoDoPedido([Bind("ID,ValorTotal,QuantidadeDoPedido,Preco,PedidoProdutosDto")] int id, PedidoDto pedido)
        {
            ViewData["Produtos"] = new SelectList(CarregarProdutosAtivos(), "ProdutoId", "Titulo", false);
            ModelState.Clear();
            var pedidoProduto = pedido.PedidoProdutosDto.First(x => x.ProdutoID == id);
            pedido.PedidoProdutosDto.Remove(pedidoProduto);
            pedido.ValorTotal += -pedidoProduto.PrecoTotal;
            return View("Criar", pedido);
        }

        public IActionResult DetalhesProduto(int id)
        {
            ProdutoDto produtoDto = _pedidoBusiness.BuscarPoduto(id);
            if (produtoDto == null)
            {
                return NotFound();
            }
            return View(produtoDto);
        }

        public IActionResult Detalhes(int id)
        {
            PedidoDto pedidoDto = _pedidoBusiness.BuscarPorId(id);
            if (pedidoDto == null)
            {
                return NotFound();
            }
            return View(pedidoDto);
        }
        public IActionResult Excluir(int id)
        {
            PedidoDto pedidoDto = _pedidoBusiness.BuscarPorId(id);
            if (pedidoDto == null)
            {
                return NotFound();
            }

            return View(pedidoDto);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public IActionResult ExcluirConfirma(int id)
        {
            _pedidoBusiness.Excluir(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.ID == id);
        }

        /// Carrega lista de produtos que tem estoque no Dropdow       
        private IEnumerable<ProdutosListaDto> CarregarProdutosAtivos()
        {
            return _pedidoBusiness.BuscarProdutos().ToList();
        }



    }
}
