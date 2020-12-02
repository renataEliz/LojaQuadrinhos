using LojaQuadrinhos.Data;
using LojaQuadrinhos.Model.Dtos;
using LojaQuadrinhos.Model.Entidades;
using LojaQuadrinhos.Model.Interfaces;
using System;
using System.Collections.Generic;

namespace LojaQuadrinhos.Business
{
    public class CategoriaBusiness
    {
        private IUnitOfwork _unitOfWork;
        public CategoriaBusiness(IUnitOfwork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Salvar(CategoriaDto categoriaDto)
        {
            Categoria categoria = new Categoria();

            if (categoriaDto.ID > 0)
            {
                categoria = _unitOfWork.CategoriaRepositorio.GetById(categoriaDto.ID);
                categoria.Descricao = categoriaDto.Descricao;
                _unitOfWork.CategoriaRepositorio.Update(categoria);
                _unitOfWork.Commit();
            }

            else
            {

                categoria.Descricao = categoriaDto.Descricao;
                _unitOfWork.CategoriaRepositorio.Add(categoria);
                _unitOfWork.Commit();
            }

        }

        public IEnumerable<CategoriaDto> ListarTodos()
        {
            var itens = _unitOfWork.CategoriaRepositorio.Get();
            List<CategoriaDto> categorias = new List<CategoriaDto>();

            foreach (var item in itens)
            {
                categorias.Add(ConverteEntidadeParaDto(item));
            }

            return categorias;

        }

        public CategoriaDto BuscarPorId(int id)
        {
            CategoriaDto CategoriaDto = ConverteEntidadeParaDto(_unitOfWork.CategoriaRepositorio.GetById(id));
            return CategoriaDto;
        }

        public bool Excluir(int id)
        {
            bool categoriaVinculadaProduto = _unitOfWork.ProdutoRepositorio.CategoriaProduto(id);

            if (categoriaVinculadaProduto)
                return false;

            Categoria item = _unitOfWork.CategoriaRepositorio.GetById(id);
            _unitOfWork.CategoriaRepositorio.Delete(item);
            _unitOfWork.Commit();
            return true;
        }

        private CategoriaDto ConverteEntidadeParaDto(Categoria item)
        {
            CategoriaDto categoriaDto = new CategoriaDto { ID = item.ID, Descricao = item.Descricao };
            return categoriaDto;
        }
    }
}
