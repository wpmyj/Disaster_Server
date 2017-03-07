using System.Linq;
using DisasterReport.EntityFramework;
using DisasterReport.MultiTenancy;

namespace DisasterReport.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly DisasterReportDbContext _context;

        public DefaultTenantCreator(DisasterReportDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
