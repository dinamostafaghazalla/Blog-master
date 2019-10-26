#region Usings

using BlogPost.Services.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Models.DTO;
using Blog.Repository.Interface;
using Blog.Repository.Interface.Repositories;

#endregion

namespace Blog.Services.Services
{
    public class BaseService<T, TDto, TKey> : IBaseService<T, TDto> where T : class where TDto : BaseDto<TKey>
    {
        #region Initialization

        private readonly IRepository<T> _repository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        #endregion

        #region constructor

        public BaseService(IRepository<T> repository, IUnitOfWork uow, IMapper mapper)
        {
            _repository = repository;
            _uow = uow;
            _mapper = mapper;
        }

        #endregion

        #region get methods

        public IEnumerable<TDto> GetAllList()
        {
            var collection = _mapper.Map<List<TDto>>(_repository.GetAllList());
            return collection;
        }

        public async Task<IEnumerable<TDto>> GetAllListAsync()
        {
            var collection = _mapper.Map<List<TDto>>(await _repository.GetAllListAsync());
            return collection;
        }

        public TDto Get(params object[] key)
        {
            var model = _mapper.Map<TDto>(_repository.Get(key));
            return model;
        }

        public async Task<TDto> GetAsync(params object[] key)
        {
            var model = _mapper.Map<TDto>(await _repository.GetAsync(key));
            return model;
        }

        public TDto GetById(object id)
        {
            return _mapper.Map<TDto>(_repository.GetById(id));
        }

        public async Task<TDto> GetByIdAsync(object id)
        {
            return _mapper.Map<TDto>(await _repository.GetByIdAsync(id));
        }

        public TDto GetFirstOrDefault(Expression<Func<T, bool>> filter = null,
            params Expression<Func<T, object>>[] includes)
        {
            return _mapper.Map<TDto>(_repository.GetFirstOrDefault(filter, includes));
        }

        public async Task<TDto> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null,
            params Expression<Func<T, object>>[] includes)
        {
            return _mapper.Map<TDto>(await _repository.GetFirstOrDefaultAsync(filter, includes));
        }

        public IEnumerable<TDto> Get(Expression<Func<T, bool>> filter = null, string[] includePaths = null,
            int? page = null, int? pageSize = null)
        {
            return _mapper.Map<IEnumerable<TDto>>(_repository.Get(filter, includePaths, page, pageSize));
        }

        public async Task<IEnumerable<TDto>> GetAsync(Expression<Func<T, bool>> filter = null,
            string[] includePaths = null, int? page = null, int? pageSize = null)
        {
            return _mapper.Map<IEnumerable<TDto>>(
                await _repository.GetAsync(filter, includePaths, page, pageSize));
        }

        #endregion

        #region find methods

        public TDto Find(Expression<Func<T, bool>> match)
        {
            return _mapper.Map<TDto>(_repository.Find(match));
        }

        public async Task<TDto> FindAsync(Expression<Func<T, bool>> match)
        {
            return _mapper.Map<TDto>(await _repository.FindAsync(match));
        }

        public ICollection<TDto> FindAll(Expression<Func<T, bool>> match)
        {
            return _mapper.Map<ICollection<TDto>>(_repository.FindAll(match));
        }

        public async Task<ICollection<TDto>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return _mapper.Map<ICollection<TDto>>(await _repository.FindAllAsync(match));
        }

        public IQueryable<TDto> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _mapper.Map<IQueryable<TDto>>(_repository.FindBy(predicate));
        }

        public async Task<ICollection<TDto>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return _mapper.Map<ICollection<TDto>>(await _repository.FindByAsync(predicate));
        }

        #endregion

        #region count methods

        public int Count()
        {
            return _repository.Count();
        }

        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        #endregion

        #region create methods

        public TDto Create(TDto model)
        {
            var entity = _mapper.Map<T>(model);

            entity = _repository.Create(entity);

            _uow.Complete();

            return _mapper.Map<TDto>(entity);
        }

        public async Task<TDto> CreateAsync(TDto model)
        {
            var entity = _mapper.Map<T>(model);

            entity = _repository.Create(entity);

            await _uow.CompleteAsync();

            return _mapper.Map<TDto>(entity);
        }

        public void Create(IEnumerable<TDto> entities)
        {
            _repository.Create(_mapper.Map<List<T>>(entities));
            _uow.Complete();
        }

        #endregion

        #region update methods

        public TDto Update(TDto model)
        {
            var entityInDb = _repository.Get(model.Id);
            entityInDb = _mapper.Map(model, entityInDb);

            _repository.Update(entityInDb);

            _uow.Complete();

            return _mapper.Map<TDto>(entityInDb);
        }

        public async Task<TDto> UpdateAsync(TDto model)
        {
            var entityInDb = await _repository.GetAsync(model.Id);
            _mapper.Map(model, entityInDb);
            _repository.Update(entityInDb);
            await _uow.CompleteAsync();

            return _mapper.Map<TDto>(entityInDb);
        }

        #endregion

        #region remove methods

        public void Remove(TDto model)
        {
            var entityInDb = _repository.Get(model.Id);

            _repository.Remove(entityInDb);

            _uow.Complete();
        }

        public async void RemoveAsync(TDto model)
        {
            var entityInDb = _repository.Get(model.Id);

            _repository.Remove(entityInDb);

            await _uow.CompleteAsync();
        }

        public void Remove(params object[] key)
        {
            var entity = _repository.Get(key);

            _repository.Remove(entity);

            _uow.Complete();
        }

        public async void RemoveAsync(params object[] key)
        {
            var entity = _repository.Get(key);

            _repository.Remove(entity);

            await _uow.CompleteAsync();
        }

        #endregion

        #region Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}