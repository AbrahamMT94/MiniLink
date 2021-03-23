using MiniLink.Shared.Pagination;
using MiniLinkLogic.Libraries.MiniLink.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniLinkLogic.Libraries.MiniLink.Data
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> Table { get; }

        Task DeleteAsync(IList<TEntity> entities);
        Task DeleteAsync(TEntity entity);
        Task<IList<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null);
        Task<IPaginatedList<TEntity>> GetAllPagedAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null, int pageIndex = 0, int pageSize = 10, bool getOnlyTotalCount = false);
        Task<TEntity> GetByIdAsync(Guid? id);
        Task<IList<TEntity>> GetByIdsAsync(IList<Guid> ids);
        Task InsertAsync(IList<TEntity> entities);
        Task InsertAsync(TEntity entity);
        Task<int> SaveChangesAsync();
        Task UpdateAsync(IList<TEntity> entities);
        Task UpdateAsync(TEntity entity);
    }
}
