﻿using System.Data.Common;
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
        public DbSet<ReporterInfoTb> reporterInfo { get; set; }
        public DbSet<MessageGroupTb> messageGroup { get; set; }
        public DbSet<DisasterInfoTb> disasterInfo { get; set; }
        public DbSet<DisasterKindTb> disasterKind { get; set; }
        public DbSet<UserTb> userInfo { get; set; }
        public DbSet<MessageNoteTb> messageNote { get; set; }
        public DbSet<GroupMemberTb> groupMember { get; set; }
        public DbSet<CityCodeTb> cityCode { get; set; }
        public DbSet<CommunityCodeTb> commCode { get; set; }
        public DbSet<DisasterRescueTb> disasterRescue { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<ReporterInfoTb>().HasOptional(r => r.Device).WithRequired(d => d.Reporter).WillCascadeOnDelete(true);
            modelBuilder.Entity<ReporterInfoTb>().HasRequired(r => r.User);

            modelBuilder.Entity<MessageGroupTb>().HasMany(m => m.Reporter).WithOptional(r => r.MessageGroup).WillCascadeOnDelete(true);
            modelBuilder.Entity<MessageGroupTb>().HasMany(m => m.Disaster).WithMany(d => d.MessageGroup);
            modelBuilder.Entity<MessageGroupTb>().HasMany(m => m.Message);

            modelBuilder.Entity<DisasterInfoTb>().HasRequired(d => d.Reporter);
            modelBuilder.Entity<DisasterInfoTb>().HasRequired(d => d.DisasterKind);

            modelBuilder.Entity<MessageNoteTb>().HasRequired(m => m.FromReporter);
            modelBuilder.Entity<MessageNoteTb>().HasMany(m => m.ToReporter).WithMany(r => r.Message);
        }
    }
}
