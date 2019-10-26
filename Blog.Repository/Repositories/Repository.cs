#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blog.Repository.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Blog.Repository.Repositories
{
    public partial class Repository<T> : IRepository<T> where T : class
    {
        #region shared variables

        private readonly ServiceContext _ctx;
        private readonly DbSet<T> _set;

        #endregion

        #region constructor

        public Repository(ServiceContext ctx)
        {
            _ctx = ctx;
            _set = _ctx.Set<T>();
        }

        #endregion

        #region get methods

        public virtual IQueryable<T> GetAll()
        {
            return _set;
        }

        public virtual IEnumerable<T> GetAllList()
        {
            return _set.ToList();
        }

        public virtual async Task<IEnumerable<T>> GetAllListAsync()
        {
            return await _set.ToListAsync();
        }

        public virtual T Get(params object[] key)
        {
            return _set.Find(key);
        }

        public virtual async Task<T> GetAsync(params object[] key)
        {
            return await _set.FindAsync(key);
        }

        public virtual List<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _set;

            foreach (var include in includes) query = query.Include(include);

            if (filter != null) query = query.Where(filter);

            if (orderBy != null) query = orderBy(query);

            return query.ToList();
        }

        public virtual async Task<List<T>> GetAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _set;

            foreach (var include in includes) query = query.Include(include);

            if (filter != null) query = query.Where(filter);

            if (orderBy != null) query = orderBy(query);

            return await query.ToListAsync();
        }

        public virtual IQueryable<T> Query(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _set;

            if (filter != null) query = query.Where(filter);

            if (orderBy != null) query = orderBy(query);

            return query;
        }

        public virtual T GetById(object id)
        {
            return _set.Find(id);
        }

        public virtual async Task<T> GetByIdAsync(object id)
        {
            return await _set.FindAsync(id);
        }

        public virtual T GetFirstOrDefault(Expression<Func<T, bool>> filter = null,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _set;

            foreach (var include in includes) query = query.Include(include);

            return query.FirstOrDefault(filter);
        }

        public virtual async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _set;

            foreach (var include in includes) query = query.Include(include);

            return await query.FirstOrDefaultAsync(filter);
        }

        public IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            string[] includePaths = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<T> query = _set;

            if (filter != null) query = query.Where(filter);

            if (includePaths != null)
                for (var i = 0; i < includePaths.Count(); i++)
                    query = query.Include(includePaths[i]);

            if (pageSize != null) query = query.Take((int) pageSize);

            return query.ToList();
        }

        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null,
            string[] includePaths = null, int? page = null, int? pageSize = null)
        {
            IQueryable<T> query = _set;

            if (filter != null) query = query.Where(filter);

            if (includePaths != null)
                for (var i = 0; i < includePaths.Count(); i++)
                    query = query.Include(includePaths[i]);
            if (pageSize != null) query = query.Take((int) pageSize);

            return await query.ToListAsync();
        }

        #endregion

        #region find methods

        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return _set.SingleOrDefault(match);
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _set.SingleOrDefaultAsync(match);
        }

        public virtual ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return _set.Where(match).ToList();
        }

        public virtual async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await _set.Where(match).ToListAsync();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            var query = _set.Where(predicate);
            return query;
        }

        public virtual async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _set.Where(predicate).ToListAsync();
        }

        #endregion

        #region count methods

        public int Count()
        {
            return _set.Count();
        }

        public async Task<int> CountAsync()
        {
            return await _set.CountAsync();
        }

        #endregion

        #region create methods

        public virtual T Create(T entity)
        {
            return _set.Add(entity).Entity;
        }

        public virtual async Task CreateAsync(T entity)
        {
            await _set.AddAsync(entity);
        }

        public virtual void Create(IEnumerable<T> entities)
        {
            _set.AddRange(entities);
        }

        #endregion

        #region update methods

        public virtual T Update(T entity)
        {
            _set.Attach(entity);
            _ctx.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        #endregion

        #region remove methods

        public virtual void Remove(T entity)
        {
            _set.Update(entity);
        }

        public virtual void HardDelete(T entity)
        {
            _set.Remove(entity);
        }

        public virtual void Remove(IEnumerable<T> entites)
        {
            foreach (var entity in entites)
            {
                _set.UpdateRange(entity);
            }
        }

        public virtual void Remove(params object[] key)
        {
            var entity = Get(key);
            Update(entity);
        }

        public bool IsExist(Expression<Func<T, bool>> filterQuery)
        {
            return _set.Any(filterQuery);
        }

        #endregion
    }
}