using LojaQuadrinhos.Model.Interfaces;
using LojaQuadrinhos.WebApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;



namespace LojaQuadrinhos.Data.Repositorios
{
    //Implementação do repositorio generico
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly LojaQuadrinhosContext _context;
        private readonly DbSet<T> _dbSet;
        public Repositorio(LojaQuadrinhosContext context)
        {
            this._context = context;
            this._dbSet = this._context.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _dbSet.Update(entity);
        }
        public IEnumerable<T> Get()
        {
            return _dbSet.AsEnumerable<T>().ToList();
        }
       
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }
    }
}

