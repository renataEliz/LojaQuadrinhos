using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace LojaQuadrinhos.Model.Interfaces
{
    //Repositorio generico 
    public interface IRepositorio<T> where T : class
    {
        IEnumerable<T> Get();
        T GetById(int id);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
