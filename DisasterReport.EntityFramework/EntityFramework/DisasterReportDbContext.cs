using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using DisasterReport.Authorization.Roles;
using DisasterReport.MultiTenancy;
using DisasterReport.Users;
using DisasterReport.DomainEntities;

namespace DisasterReport.EntityFramework
{
    public class DisasterReportDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public DisasterReportDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in DisasterReportDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of DisasterReportDbContext since ABP automatically handles it.
         */
        public DisasterReportDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public DisasterReportDbContext(DbConnection connection)
            : base(connection, true)
        {

        }

        public DbSet<DeviceInfoTb> deviceInfo { get; set; }
        public DbSet<UploadsFileTb> uploadsFile { get; set; }
        public DbSet<DisasterInfoTb> disasterInfo { get; set; }
        public DbSet<DisasterKindTb> disasterKind { get; set; }
        public DbSet<ReporterInfoTb> reporterInfo { get; set; }
        public DbSet<UserTb> userInfo { get; set; }
        public DbSet<MessageNoteTb> messageNote { get; set; }
        public DbSet<MessageGroupTb> messageGroup { get; set; }
        public DbSet<GroupOwnerTb> groupOwner { get; set; }
        public DbSet<GroupAdminTb> groupAdmins { get; set; }
        public DbSet<GroupMemberTb> groupMember { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<DisasterInfoTb>().HasRequired(d => d.Reporter);
            modelBuilder.Entity<DisasterInfoTb>().HasMany(d => d.Uploads);
            modelBuilder.Entity<DisasterKindTb>().HasOptional(d => d.Parent).WithMany().HasForeignKey(d => d.ParentId);
            modelBuilder.Entity<MessageNoteTb>().HasRequired(m => m.FromReporter);
            modelBuilder.Entity<MessageNoteTb>().HasRequired(m => m.ToReporter);
            modelBuilder.Entity<GroupOwnerTb>().HasRequired(m => m.GroupOwner).WithMany(r => r.GroupOwner).WillCascadeOnDelete(true);
            modelBuilder.Entity<GroupAdminTb>().HasMany(m => m.GroupAdmins).WithMany(r => r.GroupAdmin);
            modelBuilder.Entity<GroupMemberTb>().HasMany(m => m.GroupMembers).WithMany(r => r.GroupMember);

        }
    }
}
