using PIMTool.Shared.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Services.Common
{
    public interface IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        #region Modify

        void Add(TEntity entity);

        void Add(IList<TEntity> listEntity);

        void Update(TEntity entity);

        void Update(IList<TEntity> listEntity);

        void Delete(TEntity entity);

        void Delete(IList<TEntity> listEntity);

        Task DeleteById(long id);

        Task DeleteById(IList<long> listId);

        #endregion

        #region Query Without Selector

        Task<int> CountAllAsync();

        Task<int> CountAllAsync(IQueryable<TEntity> query);

        Task<TEntity> GetByIdAsync(long id, bool tracking = false);

        Task<IList<TEntity>> GetByIdAsync(IList<long> listId, bool tracking = false);

        Task<IList<TEntity>> GetAllAsync(bool tracking = false);

        Task<IList<TEntity>> GetAllAsync(IQueryable<TEntity> query, bool tracking = false);

        Task<TEntity> FirstOrDefaultAsync(IQueryable<TEntity> query, bool tracking = false);

        #endregion

        #region Query With selector

        EntitySelector<TEntity, TResult> CreateSelector<TResult>(Expression<Func<TEntity, TResult>> selector);

        IQueryable<TEntity> AsQueryable();

        Task<TResult> GetByIdAsync<TResult>(long id, EntitySelector<TEntity, TResult> selector);

        Task<IList<TResult>> GetByIdAsync<TResult>(IList<long> listId, EntitySelector<TEntity, TResult> selector);

        Task<IList<TResult>> GetAllAsync<TResult>(EntitySelector<TEntity, TResult> selector);

        Task<IList<TResult>> GetAllAsync<TResult>(IQueryable<TEntity> query, EntitySelector<TEntity, TResult> selector);

        Task<TResult> FirstOrDefaultAsync<TResult>(IQueryable<TEntity> query, EntitySelector<TEntity, TResult> selector);

        #endregion
    }
}
