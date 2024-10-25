using CNSMarketing.Domain.Entity.Common;
using CNSMarketing.Service.Abstraction;
using CNSMarketing.Service.Repositories;
using System.Linq.Expressions;

namespace CNSMarketing.Persistence.Service
{
    public class GenericService<TEntity, Key, IRead, IWrite> : IGenericService<TEntity, Key, IRead, IWrite>
     where TEntity : BaseEntity<Key>
     where IRead : IReadRepository<TEntity, Key>
     where IWrite : IWriteRepository<TEntity, Key>
    {
        private readonly IRead _readRepository;
        private readonly IWrite _writeRepository;

        public GenericService(IRead readRepository, IWrite writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        public async Task<TEntity> GetByIdAsync(Key id)
        {
            return await _readRepository.GetByIdAsync(id);
        }
        public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> method, bool tracking = true)
        {
            return _readRepository.GetWhere(method, tracking);
        }

        public IQueryable<TEntity> GetAll(bool tracking = true)
        {
            return _readRepository.GetAll(tracking);
        }


        public async Task AddAsync(TEntity entity)
        {
            var idProperty = typeof(TEntity).GetProperty("Id");
            var idValue = idProperty?.GetValue(entity);

            if (idProperty?.PropertyType == typeof(int))
            {
                entity.CreatedDate = DateTime.Now;
            }

            await _writeRepository.AddAsync(entity);
            await _writeRepository.SaveAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _writeRepository.Update(entity);
            await _writeRepository.SaveAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _writeRepository.Remove(entity);
            await _writeRepository.SaveAsync();
        }

        public async Task<bool> RemoveAsync(Key id)
        {
            var result = await _writeRepository.RemoveAsync(id);
            await _writeRepository.SaveAsync();
            return result;
        }

        public async Task<bool> RemoveRange(List<TEntity> datas)
        {
            var resultData = _writeRepository.RemoveRange(datas);
            await _writeRepository.SaveAsync();
            return resultData;
        }

        public async Task<bool> AddRangeAsync(List<TEntity> datas)
        {
            var result = await _writeRepository.AddRangeAsync(datas);
            await _writeRepository.SaveAsync();

            return result;
        }
    }
}
