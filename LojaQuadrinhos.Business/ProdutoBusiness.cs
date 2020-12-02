using LojaQuadrinhos.Model.Dtos;
using LojaQuadrinhos.Model.Entidades;
using LojaQuadrinhos.Model.Interfaces;
using System;
using System.Collections.Generic;

namespace LojaQuadrinhos.Business
{
    public class ProdutoBusiness
    {
        private IUnitOfwork _unitOfWork;
        public ProdutoBusiness(IUnitOfwork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Salvar(ProdutoDto produtoDto)
        {
            Produto produto = new Produto();

            if (produtoDto.ID > 0)
            {

                produto = _unitOfWork.ProdutoRepositorio.GetById(produtoDto.ID);
                produto = ConverteDtoParaEntidade(produtoDto, produto);
                _unitOfWork.ProdutoRepositorio.Update(produto);
            }

            else
            {
                produto = ConverteDtoParaEntidade(produtoDto, produto);
                _unitOfWork.ProdutoRepositorio.Add(produto);

            }
            _unitOfWork.Commit();

        }


        public IEnumerable<ProdutoIndexDto> ListarTodos()
        {
            var itens = _unitOfWork.ProdutoRepositorio.Get();
            List<ProdutoIndexDto> produtos = new List<ProdutoIndexDto>();

            foreach (var item in itens)
            {     
                produtos.Add(ConverteEntidadeIndexParaDto(item));
            }
            return produtos;

        }

        public IEnumerable<CategoriaDto> BuscarCategorias()
        {

            var itens =  _unitOfWork.CategoriaRepositorio.Get();
            List<CategoriaDto> categorias = new List<CategoriaDto>();
            foreach (var item in itens)
            {
                CategoriaDto categoria = new CategoriaDto {ID =  item.ID, categoriaId = item.ID.ToString(), Descricao = item.Descricao };
                categorias.Add(categoria);
            }
            return categorias;

        }

        public ProdutoDto BuscarPorId(int id)
        {
            Produto produto = _unitOfWork.ProdutoRepositorio.GetById(id);
            return ConverteEntidadeParaDto(produto);
        }

        public bool Excluir(int id)
        {
            bool produtoVinculadaPedido = _unitOfWork.PedidoRepositorio.ProdutoPedido(id);

            if (produtoVinculadaPedido)
                return false;

            Produto item = _unitOfWork.ProdutoRepositorio.GetById(id);
            _unitOfWork.ProdutoRepositorio.Delete(item);
            _unitOfWork.Commit();
            return true;
        }

        private Produto ConverteDtoParaEntidade(ProdutoDto produtoDto, Produto produto)
        {

            produto.Ano = produtoDto.Ano;
            produto.CategoriaId = Convert.ToInt32(produtoDto.CategoriaId);
            produto.Autor = produtoDto.Autor;
            produto.Editora = produtoDto.Editora;
            produto.Idioma = produtoDto.Idioma;
            produto.Paginas = produtoDto.Paginas;
            produto.Preco = produtoDto.Preco;
            produto.Quantidade = produtoDto.Quantidade;
            produto.Serie = produtoDto.Serie;
            produto.Titulo = produtoDto.Titulo;
            return produto;
        }

        private ProdutoDto ConverteEntidadeParaDto(Produto produto)
        {

            ProdutoDto produtoDto = new ProdutoDto();
            var categoria = _unitOfWork.CategoriaRepositorio.GetById(produto.CategoriaId);
            produtoDto.Categoria = new Categoria { ID = categoria.ID, Descricao = categoria.Descricao };
            produtoDto.CategoriaId = produto.Categoria.ID.ToString();
            produtoDto.ID = produto.ID;
            produtoDto.Ano = produto.Ano;
            produtoDto.Autor = produto.Autor;
            produtoDto.Editora = produto.Editora;
            produtoDto.Idioma = produto.Idioma;
            produtoDto.Paginas = produto.Paginas;
            produtoDto.Preco = produto.Preco;
            produtoDto.Quantidade = produto.Quantidade;
            produtoDto.Serie = produto.Serie;
            produtoDto.Titulo = produto.Titulo;

            return produtoDto;
        }


        private ProdutoIndexDto ConverteEntidadeIndexParaDto(Produto produto)
        {   
            ProdutoIndexDto produtoDto = new ProdutoIndexDto();
            var categoria = _unitOfWork.CategoriaRepositorio.GetById(produto.CategoriaId);
            produtoDto.Categoria = new Categoria {ID = categoria.ID, Descricao = categoria.Descricao }; 
            produtoDto.ID = produto.ID;
            produtoDto.Preco = produto.Preco;
            produtoDto.Quantidade = produto.Quantidade;
            produtoDto.Titulo = produto.Titulo;

            return produtoDto;
        }
    }
}
