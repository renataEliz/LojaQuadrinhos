
using LojaQuadrinhos.Model.Dtos;
using LojaQuadrinhos.Model.Entidades;
using LojaQuadrinhos.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LojaQuadrinhos.Business
{
    public class PedidoBusiness
    {
        private IUnitOfwork _unitOfWork;

        public PedidoBusiness(IUnitOfwork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PedidoDto> ListarTodos()
        {
            var itens = _unitOfWork.PedidoRepositorio.Get();
            List<PedidoDto> pedidos = new List<PedidoDto>();

            foreach (var item in itens)
            {
                pedidos.Add(ConverteEntidadeIndexParaDto(item));
            }
            return pedidos;
        }

        public PedidoDto BuscarPorId(int id)
        {
            Pedido pedido = _unitOfWork.PedidoRepositorio.GetById(id);
            return ConverteEntidadeParaDto(pedido);
        }

        private List<PedidoProdutoDto> BuscaProdutosDoPedido(int id)
        {

            ICollection<PedidoProduto> pedidoProdutos = _unitOfWork.PedidoProdutoRepositorio.BuscaProdutosPedidos(id);
            List<PedidoProdutoDto> pedidoProdutosDto = new List<PedidoProdutoDto>();

            foreach (var item in pedidoProdutos)
            {

                pedidoProdutosDto.Add(ConvertePedidoEntidadeParaDto(item));
            }
            return pedidoProdutosDto;

        }

        public IEnumerable<ProdutosListaDto> BuscarProdutos()
        {

            var itens = _unitOfWork.ProdutoRepositorio.GetProdutosAtivos();
            List<ProdutosListaDto> produtos = new List<ProdutosListaDto>();
            foreach (var item in itens)
            {
                ProdutosListaDto produto = new ProdutosListaDto { ProdutoId = item.ID.ToString(), Titulo = item.Titulo + " - R$ " + item.Preco.ToString() };
                produtos.Add(produto);
            }
            return produtos;
        }

        public PedidoDto BuscarProdutoDoPedido(PedidoDto pedido)
        {
            var produto = _unitOfWork.ProdutoRepositorio.GetById(Convert.ToInt32(pedido.ProdutoId));

            if (pedido.PedidoProdutosDto.Exists(x => x.ProdutoID == Convert.ToInt32(pedido.ProdutoId)))
            {
                var pedidoProduto = pedido.PedidoProdutosDto.First(x => x.ProdutoID == Convert.ToInt32(pedido.ProdutoId));

                if (pedidoProduto.QuantidadeDeProduto + pedido.Quantidade > produto.Quantidade)
                {
                    pedido.Sucesso = false;
                    pedido.QtdDisponivelEstoque = produto.Quantidade.ToString();
                    return pedido;
                }
                else
                {
                    pedido.PedidoProdutosDto.Remove(pedidoProduto);
                    pedidoProduto.QuantidadeDeProduto += pedido.Quantidade;
                    pedidoProduto.PrecoTotal = produto.Preco * pedidoProduto.QuantidadeDeProduto;
                    pedidoProduto.PrecoUnitario = produto.Preco;
                    pedido.PedidoProdutosDto.Add(pedidoProduto);
                    pedido.ValorTotal = SomaValorTotal(pedido.PedidoProdutosDto);
                    pedido.Sucesso = true;
                    return pedido;
                }
            }
            else
            {
                if (pedido.Quantidade > produto.Quantidade)
                {
                    pedido.Sucesso = false;
                    pedido.QtdDisponivelEstoque = produto.Quantidade.ToString();
                    return pedido;
                }
                else
                {
                    pedido.PedidoProdutosDto.Add(new PedidoProdutoDto
                    {
                        QuantidadeDeProduto = pedido.Quantidade,
                        ProdutoID = produto.ID,
                        PrecoTotal = produto.Preco * pedido.Quantidade,
                        PrecoUnitario = produto.Preco,
                        Titulo = produto.Titulo
                        
                });
                    pedido.ValorTotal = SomaValorTotal(pedido.PedidoProdutosDto);
                    pedido.Sucesso = true;
                    return pedido;
                }
            }

        }

        public ProdutoDto BuscarPoduto(int id)
        {
            ProdutoBusiness produtoBusiness = new ProdutoBusiness(_unitOfWork);
            return produtoBusiness.BuscarPorId(id);
        }

        public void Excluir(int id)
        {
            var pedido = _unitOfWork.PedidoRepositorio.GetById(id);
            var pedidoProdutos = _unitOfWork.PedidoProdutoRepositorio.Get().Where(x => x.PedidoId == pedido.ID).ToList();
            _unitOfWork.PedidoRepositorio.Delete(pedido);
            _unitOfWork.Commit();
            BaixarEstoque(pedido.pedidoProdutos, false);

        }

        public void Salvar(PedidoDto pedidoDto)
        {
            Pedido pedido = ConverteDtoParaEntidade(pedidoDto);
            _unitOfWork.PedidoRepositorio.Add(pedido);
            _unitOfWork.Commit();
            BaixarEstoque(pedido.pedidoProdutos, true);
        }

        private void BaixarEstoque(ICollection<PedidoProduto> pedidoProdutos, bool baixarEstoque)
        {
            foreach (var item in pedidoProdutos)
            {
                var produto = _unitOfWork.ProdutoRepositorio.GetById(item.ProdutoID);
                if (baixarEstoque)
                    produto.Quantidade += -item.QuantidadeProduto;
                else
                    produto.Quantidade += +item.QuantidadeProduto;
                _unitOfWork.ProdutoRepositorio.Update(produto);
            }
            _unitOfWork.Commit();
        }

        private Pedido ConverteDtoParaEntidade(PedidoDto pedidoDto)
        {
            Pedido pedido = new Pedido
            {
                DataDoPedido = pedidoDto.DataDoPedido,
                Usuario = pedidoDto.Usuario,
                ValorTotal = pedidoDto.ValorTotal,
                pedidoProdutos = ConverteDtoParaEntidade(pedidoDto.PedidoProdutosDto)
            };
            return pedido;
        }

        private List<PedidoProduto> ConverteDtoParaEntidade(List<PedidoProdutoDto> pedidoProdutosDto)
        {
            List<PedidoProduto> pedidoProdutos = new List<PedidoProduto>();
            foreach (var item in pedidoProdutosDto)
            {
                var pedidoProduto = new PedidoProduto
                {
                    ProdutoID = item.ProdutoID,
                    QuantidadeProduto = item.QuantidadeDeProduto,
                    PrecoUnitario = item.PrecoUnitario,
                    PrecoTotalProduto = item.PrecoTotal
                };
                pedidoProdutos.Add(pedidoProduto);
            }
            return pedidoProdutos;
        }

        private PedidoDto ConverteEntidadeParaDto(Pedido item)
        {
            PedidoDto pedidoDto = new PedidoDto();
            pedidoDto.ID = item.ID;
            pedidoDto.DataDoPedido = item.DataDoPedido;
            pedidoDto.Usuario = item.Usuario;
            pedidoDto.ValorTotal = item.ValorTotal;
            pedidoDto.PedidoProdutosDto = BuscaProdutosDoPedido(item.ID);

            return pedidoDto;
        }

        private PedidoDto ConverteEntidadeIndexParaDto(Pedido item)
        {

            PedidoDto pedidoDto = new PedidoDto();
            pedidoDto.ID = item.ID;
            pedidoDto.DataDoPedido = item.DataDoPedido;
            pedidoDto.Usuario = item.Usuario;
            pedidoDto.ValorTotal = item.ValorTotal;

            return pedidoDto;

        }

        private decimal SomaValorTotal(List<PedidoProdutoDto> pedidoProdutosDto)
        {
            decimal retorno = 0;

            foreach (var item in pedidoProdutosDto)
            {
                retorno += item.PrecoTotal;
            }
            return retorno;
        }

        private PedidoProdutoDto ConvertePedidoEntidadeParaDto(PedidoProduto pedidoProduto)
        {
            Produto produto = _unitOfWork.ProdutoRepositorio.GetById(pedidoProduto.ProdutoID);
            var pedidoProdutoDto = new PedidoProdutoDto
            {
                PedidoId = pedidoProduto.ID,
                ProdutoID = produto.ID,
                Titulo = produto.Titulo,
                PrecoUnitario = produto.Preco,
                QuantidadeDeProduto = pedidoProduto.QuantidadeProduto,
                PrecoTotal = pedidoProduto.PrecoTotalProduto,
            };
            return pedidoProdutoDto;
        }
    }
}
