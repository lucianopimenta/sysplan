using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SysPlan.Core.Repository
{
    public interface IRepository<T>
    {
        T Get(int codigo);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetNoTracking(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetNoTracking(Expression<Func<T, bool>> predicate, params string[] include);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate, params string[] include);
        IQueryable<T> GetAll();
        void Delete(T entity);
        void DeleteRange(List<T> entity);
        bool Save(T entity);
        IDbContextTransaction BeginTransaction();
        DbContext Ctx();
    }
}
