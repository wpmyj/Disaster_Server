using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace DisasterReport.EntityFramework.Repositories
{
    public abstract class DisasterReportRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<DisasterReportDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected DisasterReportRepositoryBase(IDbContextProvider<DisasterReportDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class DisasterReportRepositoryBase<TEntity> : DisasterReportRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected DisasterReportRepositoryBase(IDbContextProvider<DisasterReportDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
