#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

#endregion

namespace BlogPost.Services.Interfaces.Services
{
    public interface IBaseService<T, TDto> : IDisposable where T : class
    {
        #region get methods

        IEnumerable<TDto> GetAllList();
        Task<IEnumerable<TDto>> GetAllListAsync();
        TDto Get(params object[] key);
        Task<TDto> GetAsync(params object[] key);
        TDto GetById(object id);
        Task<TDto> GetByIdAsync(object id);
        TDto GetFirstOrDefault(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes);

        Task<TDto> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null,
            params Expression<Func<T, object>>[] includes);

        IEnumerable<TDto> Get(Expression<Func<T, bool>> filter = null, string[] includePaths = null, int? page = null,
            int? pageSize = null);

        Task<IEnumerable<TDto>> GetAsync(Expression<Func<T, bool>> filter = null, string[] includePaths = null,
            int? page = null, int? pageSize = null);

        #endregion

        #region find methods

        TDto Find(Expression<Func<T, bool>> match);
        Task<TDto> FindAsync(Expression<Func<T, bool>> match);
        ICollection<TDto> FindAll(Expression<Func<T, bool>> match);
        Task<ICollection<TDto>> FindAllAsync(Expression<Func<T, bool>> match);
        IQueryable<TDto> FindBy(Expression<Func<T, bool>> predicate);
        Task<ICollection<TDto>> FindByAsync(Expression<Func<T, bool>> predicate);

        #endregion

        #region count methods

        int Count();
        Task<int> CountAsync();

        #endregion

        #region create methods

        TDto Create(TDto model);
        Task<TDto> CreateAsync(TDto model);
        void Create(IEnumerable<TDto> entities);

        #endregion

        #region update methods

        TDto Update(TDto model);
        Task<TDto> UpdateAsync(TDto model);

        #endregion

        #region remove methods

        void Remove(TDto model);
        void RemoveAsync(TDto model);
        void Remove(params object[] key);
        void RemoveAsync(params object[] key);

        #endregion
    }
}