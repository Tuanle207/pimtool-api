using Microsoft.EntityFrameworkCore;
using PIMTool.Services.Common;
using PIMTool.Services.Project;
using PIMTool.Shared.Contract.Common;
using PIMTool.Shared.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace PIMTool.Db.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        internal AppDbContext context;
        internal DbSet<TEntity> dbSet;

        public RepositoryBase(AppDbContext dbContext)
        {
            context = dbContext;
            dbSet = context.Set<TEntity>();
        }

        #region Modify

        public void Add(TEntity entity)
        {
            context.Add(entity);
        }

        public void Add(IList<TEntity> listEntity)
        {
            foreach (TEntity entity in listEntity)
            {
                Add(entity);
            }
        }

        public void Update(TEntity entity)
        {
            context.Update(entity);
        }
        public void Update(IList<TEntity> listEntity)
        {
            foreach (TEntity entity in listEntity)
            {
                context.Update(entity);
            }
        }

        public void Delete(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public void Delete(IList<TEntity> listEntity)
        {
            foreach (TEntity entity in listEntity)
            {
                Delete(entity);
            }
        }

        public async Task DeleteById(long id)
        {
            TEntity entity = await dbSet.FindAsync(id);
            if (entity == null)
            {
                Delete(entity);
            }
        }

        public async Task DeleteById(IList<long> listId)
        {
            IList<TEntity> listEntity = await dbSet.Where(x => listId.Contains(x.Id))
                .ToListAsync();
            Delete(listEntity);
        }

        #endregion

        #region Query Without Selector

        public async Task<TEntity> FirstOrDefaultAsync(IQueryable<TEntity> query, bool tracking = false)
        {
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            TEntity result = await query.FirstOrDefaultAsync();
            return result;
        }

        public async Task<IList<TEntity>> GetAllAsync(bool tracking = false)
        {
            IQueryable<TEntity> query = dbSet.AsQueryable();
            if(!tracking)
            {
                query = query.AsNoTracking();
            }
            IList<TEntity> result = await query.ToListAsync();
            return result;
        }

        public async Task<IList<TEntity>> GetAllAsync(IQueryable<TEntity> query, bool tracking = false)
        {
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            IList<TEntity> result = await query.ToListAsync();
            return result;
        }

        public async Task<TEntity> GetByIdAsync(long id, bool tracking = false)
        {
            if (tracking)
            {
                return await dbSet.FindAsync(id);
            }
            else
            {
                IQueryable<TEntity> query = dbSet.AsQueryable();
                return await FirstOrDefaultAsync(query, false);
            }
        }

        public async Task<IList<TEntity>> GetByIdAsync(IList<long> listId, bool tracking = false)
        {
            IQueryable<TEntity> query = dbSet.AsQueryable();
            if (tracking)
            {
                query = query.AsNoTracking();
            }
            IList<TEntity> items = await query.Where(x => listId.Contains(x.Id))
                .ToListAsync();
            return items;
        }

        public async Task<int> CountAllAsync()
        {
            int count = await dbSet.CountAsync();
            return count;
        }

        public async Task<int> CountAllAsync(IQueryable<TEntity> query)
        {
            int count = await query.CountAsync();
            return count;
        }


        #endregion

        #region Query With selector

        public IQueryable<TEntity> AsQueryable()
        {
            return dbSet.AsQueryable();
        }

        public EntitySelector<TEntity, TResult> CreateSelector<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            return new EntitySelector<TEntity, TResult>(selector);
        }

        public async Task<IList<TResult>> GetAllAsync<TResult>(EntitySelector<TEntity, TResult> selector)
        {
            IList<TResult> result = await dbSet.AsNoTracking()
                .Select(selector.Expression)
                .ToListAsync();

            return result;
        }

        public async Task<IList<TResult>> GetAllAsync<TResult>(IQueryable<TEntity> query, EntitySelector<TEntity, TResult> selector)
        {
            IList<TResult> result = await query.AsNoTracking()
                .Select(selector.Expression)
                .ToListAsync();

            return result;
        }


        public async Task<TResult> GetByIdAsync<TResult>(long id, EntitySelector<TEntity, TResult> selector)
        {
            TResult result = await dbSet.AsNoTracking()
                .Where(x => x.Id == id)
                .Select(selector.Expression)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<IList<TResult>> GetByIdAsync<TResult>(IList<long> listId, EntitySelector<TEntity, TResult> selector)
        {
            IList<TResult> result = await dbSet.AsNoTracking()
                .Where(x => listId.Contains(x.Id))
                .Select(selector.Expression)
                .ToListAsync();

            return result;
        }

        public async Task<TResult> FirstOrDefaultAsync<TResult>(IQueryable<TEntity> query, EntitySelector<TEntity, TResult> selector)
        {
            TResult result = await query.AsNoTracking()
                .Select(selector.Expression)
                .FirstOrDefaultAsync();
            return result;
        }
        
        #endregion
    }
}
