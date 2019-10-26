#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

#endregion

namespace Blog.Repository.Interface.Repositories
{
    public interface IRepository<T> where T : class
    {
        #region get methods
        IQueryable<T> GetAll();
        
        IEnumerable<T> GetAllList();
        Task<IEnumerable<T>> GetAllListAsync();
        T Get(params object[] key);
        Task<T> GetAsync(params object[] key);
        List<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes);
        IQueryable<T> Query(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
       
        T GetById(object id);
        Task<T> GetByIdAsync(object id);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes);
        IEnumerable<T> Get(
          Expression<Func<T, bool>> filter = null,
          string[] includePaths = null,
          int? page = null,
          int? pageSize = null);

        Task<IEnumerable<T>> GetAsync(
         Expression<Func<T, bool>> filter = null,
         string[] includePaths = null,
         int? page = null,
         int? pageSize = null);

        #endregion

        #region find methods
        T Find(Expression<Func<T, bool>> match);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        ICollection<T> FindAll(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        #endregion

        #region count methods
        int Count();
        Task<int> CountAsync();
        #endregion

        #region create methods
        T Create(T entity);
        void Create(IEnumerable<T> entities);
        #endregion

        #region update methods
        T Update(T entity);
        #endregion

        #region remove methods
        void Remove(T entity);
        void HardDelete(T entity);
        void Remove(IEnumerable<T> entity);
        void Remove(params object[] key);
        
        bool IsExist(Expression<Func<T, bool>> filterQuery);
        #endregion
    }
}