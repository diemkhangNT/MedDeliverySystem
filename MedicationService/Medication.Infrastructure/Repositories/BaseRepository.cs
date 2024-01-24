using Medication.Domain.Extensions;
using Medication.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medication.Infrastructure.Repositories
{
    public class BaseRepository<TContext, TEntity> : IBaseRepository<TEntity>
        where TContext : DbContext
        where TEntity : class
    {
        protected TContext _context;

        public BaseRepository(TContext context)
        {
            _context = context;
        }

        public DbSet<TEntity> GetDbSet() => _context.Set<TEntity>();

        public void DeleteItem(TEntity entity)
        {
            GetDbSet().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllOfItems(ExpressionModel<TEntity> expressionModel, bool? isTrackingForUpdate = true)
        {
            var dbSet = isTrackingForUpdate == true ? GetDbSet() : GetDbSet().AsNoTracking();
            return await dbSet.Where(expressionModel.RootExpression).ToListAsync();
        }

        public async Task<TEntity> GetSingleItem(ExpressionModel<TEntity> expressionModel, bool? isTrackingForUpdate = true)
        {
            var dbSet = isTrackingForUpdate == true ? GetDbSet() : GetDbSet().AsNoTracking();
            return await dbSet.FirstOrDefaultAsync(expressionModel.RootExpression);
        }

        public async Task InsertItem(TEntity entity)
        {
            await GetDbSet().AddAsync(entity);
        }

        public void UpdateItem(TEntity entity)
        {
            GetDbSet().Update(entity);
        }
    }
}
