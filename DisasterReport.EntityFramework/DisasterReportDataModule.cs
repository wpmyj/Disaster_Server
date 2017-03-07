using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using DisasterReport.EntityFramework;

namespace DisasterReport
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(DisasterReportCoreModule))]
    public class DisasterReportDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<DisasterReportDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
