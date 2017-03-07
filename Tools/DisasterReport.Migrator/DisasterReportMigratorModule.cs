using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using DisasterReport.EntityFramework;

namespace DisasterReport.Migrator
{
    [DependsOn(typeof(DisasterReportDataModule))]
    public class DisasterReportMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<DisasterReportDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}