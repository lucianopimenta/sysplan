using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SysPlan.Core.Entity;
using SysPlan.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SysPlan.Core.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(IDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.BeginTransaction();
        }

        public DbContext Ctx()
        {
            return _context.Ctx();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public void DeleteRange(List<T> entity)
        {
            try
            {
                _dbSet.RemoveRange(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T Get(int codigo)
        {
            return _dbSet.Find(codigo);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public IQueryable<T> GetNoTracking(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate);
        }

        public IQueryable<T> GetNoTracking(Expression<Func<T, bool>> predicate, params string[] include)
        {
            var query = _dbSet.AsNoTracking().Where(predicate);

            foreach (var item in include)
            {
                query = query.Include(item);
            }

            return query;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate, params string[] include)
        {
            var query = _dbSet.Where(predicate);

            foreach (var item in include)
            {
                query = query.Include(item);
            }

            return query;
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        private void Insert(T entity)
        {
            (entity as EntityBase).DataCriacao = DateTime.Now;
            _dbSet.Add(entity);
        }

        public bool Save(T entity)
        {
            if ((entity as EntityBase).Codigo == 0)
                Insert(entity);
            else
                Update(entity);

            return _context.SaveChanges() > 0;
        }

        private void Update(T entity)
        {
            (entity as EntityBase).DataAtualizacao = DateTime.Now;
            _dbSet.Update(entity);
        }
    }
}
