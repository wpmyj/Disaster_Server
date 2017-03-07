namespace DisasterReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AbpBackgroundJobs", new[] { "IsAbandoned", "NextTryTime" });
            DropIndex("dbo.AbpUserNotifications", new[] { "UserId", "State", "CreationTime" });
            AlterColumn("dbo.AbpAuditLogs", "ExecutionTime", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.AbpBackgroundJobs", "JobArgs", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.AbpBackgroundJobs", "NextTryTime", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.AbpBackgroundJobs", "LastTryTime", c => c.DateTime(precision: 0));
            AlterColumn("dbo.AbpBackgroundJobs", "CreationTime", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.AbpFeatures", "CreationTime", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.AbpEditions", "DeletionTime", c => c.DateTime(precision: 0));
            AlterColumn("dbo.AbpEditions", "LastModificationTime", c => c.DateTime(precision: 0));
            AlterColumn("dbo.AbpEditions", "CreationTime", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.AbpLanguages", "DeletionTime", c => c.DateTime(precision: 0));
            AlterColumn("dbo.AbpLanguages", "LastModificationTime", c => c.DateTime(precision: 0));
            AlterColumn("dbo.AbpLanguages", "CreationTime", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.AbpLanguageTexts", "Value", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.AbpLanguageTexts", "LastModificationTime", c => c.DateTime(precision: 0));
            AlterColumn("dbo.AbpLanguageTexts", "CreationTime", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.AbpNotifications", "Data", c => c.String(unicode: false));
            AlterColumn("dbo.AbpNotifications", "UserIds", c => c.String(unicode: false));
            AlterColumn("dbo.AbpNotifications", "ExcludedUserIds", c => c.String(unicode: false));
            AlterColumn("dbo.AbpNotifications", "TenantIds", c => c.String(unicode: false));
            AlterColumn("dbo.AbpNotifications", "CreationTime", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.AbpNotificationSubscriptions", "CreationTime", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.AbpOrganizationUnits", "DeletionTime", c => c.DateTime(precision: 0));
            AlterColumn("dbo.AbpOrganizationUnits", "LastModificationTime", c => c.DateTime(precision: 0));
            AlterColumn("dbo.AbpOrganizationUnits", "CreationTime", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.AbpPermissions", "CreationTime", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.AbpRoles", "DeletionTime", c => c.DateTime(precision: 0));
            AlterColumn("dbo.AbpRoles", "LastModificationTime", c => c.DateTime(precision: 0));
            AlterColumn("dbo.AbpRoles", "CreationTime", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.AbpUsers", "LockoutEndDateUtc", c => c.DateTime(precision: 0));
            AlterColumn("dbo.AbpUsers", "PhoneNumber", c => c.String(unicode: false));
            AlterColumn("dbo.AbpUsers", "SecurityStamp", c => c.String(unicode: false));
            AlterColumn("dbo.AbpUsers", "LastLoginTime", c => c.DateTime(precision: 0));
            AlterColumn("dbo.AbpUsers", "DeletionTime", c => c.DateTime(precision: 0));
            AlterColumn("dbo.AbpUsers", "LastModificationTime", c => c.DateTime(precision: 0));
            AlterColumn("dbo.AbpUsers", "CreationTime", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.AbpUserClaims", "ClaimType", c => c.String(unicode: false));
            AlterColumn("dbo.AbpUserClaims", "ClaimValue", c => c.String(unicode: false));
            AlterColumn("dbo.AbpUserClaims", "CreationTime", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.AbpUserRoles", "CreationTime", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.AbpSettings", "LastModificationTime", c => c.DateTime(precision: 0));
            AlterColumn("dbo.AbpSettings", "CreationTime", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.AbpTenantNotifications", "Data", c => c.String(unicode: false));
            AlterColumn("dbo.AbpTenantNotifications", "CreationTime", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.AbpTenants", "DeletionTime", c => c.DateTime(precision: 0));
            AlterColumn("dbo.AbpTenants", "LastModificationTime", c => c.DateTime(precision: 0));
            AlterColumn("dbo.AbpTenants", "CreationTime", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.AbpUserAccounts", "UserName", c => c.String(unicode: false));
            AlterColumn("dbo.AbpUserAccounts", "EmailAddress", c => c.String(unicode: false));
            AlterColumn("dbo.AbpUserAccounts", "LastLoginTime", c => c.DateTime(precision: 0));
            AlterColumn("dbo.AbpUserAccounts", "DeletionTime", c => c.DateTime(precision: 0));
            AlterColumn("dbo.AbpUserAccounts", "LastModificationTime", c => c.DateTime(precision: 0));
            AlterColumn("dbo.AbpUserAccounts", "CreationTime", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.AbpUserLoginAttempts", "CreationTime", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.AbpUserNotifications", "CreationTime", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.AbpUserOrganizationUnits", "CreationTime", c => c.DateTime(nullable: false, precision: 0));
            CreateIndex("dbo.AbpBackgroundJobs", new[] { "IsAbandoned", "NextTryTime" });
            CreateIndex("dbo.AbpUserNotifications", new[] { "UserId", "State", "CreationTime" });
        }
        
        public override void Down()
        {
            DropIndex("dbo.AbpUserNotifications", new[] { "UserId", "State", "CreationTime" });
            DropIndex("dbo.AbpBackgroundJobs", new[] { "IsAbandoned", "NextTryTime" });
            AlterColumn("dbo.AbpUserOrganizationUnits", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AbpUserNotifications", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AbpUserLoginAttempts", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AbpUserAccounts", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AbpUserAccounts", "LastModificationTime", c => c.DateTime());
            AlterColumn("dbo.AbpUserAccounts", "DeletionTime", c => c.DateTime());
            AlterColumn("dbo.AbpUserAccounts", "LastLoginTime", c => c.DateTime());
            AlterColumn("dbo.AbpUserAccounts", "EmailAddress", c => c.String());
            AlterColumn("dbo.AbpUserAccounts", "UserName", c => c.String());
            AlterColumn("dbo.AbpTenants", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AbpTenants", "LastModificationTime", c => c.DateTime());
            AlterColumn("dbo.AbpTenants", "DeletionTime", c => c.DateTime());
            AlterColumn("dbo.AbpTenantNotifications", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AbpTenantNotifications", "Data", c => c.String());
            AlterColumn("dbo.AbpSettings", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AbpSettings", "LastModificationTime", c => c.DateTime());
            AlterColumn("dbo.AbpUserRoles", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AbpUserClaims", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AbpUserClaims", "ClaimValue", c => c.String());
            AlterColumn("dbo.AbpUserClaims", "ClaimType", c => c.String());
            AlterColumn("dbo.AbpUsers", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AbpUsers", "LastModificationTime", c => c.DateTime());
            AlterColumn("dbo.AbpUsers", "DeletionTime", c => c.DateTime());
            AlterColumn("dbo.AbpUsers", "LastLoginTime", c => c.DateTime());
            AlterColumn("dbo.AbpUsers", "SecurityStamp", c => c.String());
            AlterColumn("dbo.AbpUsers", "PhoneNumber", c => c.String());
            AlterColumn("dbo.AbpUsers", "LockoutEndDateUtc", c => c.DateTime());
            AlterColumn("dbo.AbpRoles", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AbpRoles", "LastModificationTime", c => c.DateTime());
            AlterColumn("dbo.AbpRoles", "DeletionTime", c => c.DateTime());
            AlterColumn("dbo.AbpPermissions", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AbpOrganizationUnits", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AbpOrganizationUnits", "LastModificationTime", c => c.DateTime());
            AlterColumn("dbo.AbpOrganizationUnits", "DeletionTime", c => c.DateTime());
            AlterColumn("dbo.AbpNotificationSubscriptions", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AbpNotifications", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AbpNotifications", "TenantIds", c => c.String());
            AlterColumn("dbo.AbpNotifications", "ExcludedUserIds", c => c.String());
            AlterColumn("dbo.AbpNotifications", "UserIds", c => c.String());
            AlterColumn("dbo.AbpNotifications", "Data", c => c.String());
            AlterColumn("dbo.AbpLanguageTexts", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AbpLanguageTexts", "LastModificationTime", c => c.DateTime());
            AlterColumn("dbo.AbpLanguageTexts", "Value", c => c.String(nullable: false));
            AlterColumn("dbo.AbpLanguages", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AbpLanguages", "LastModificationTime", c => c.DateTime());
            AlterColumn("dbo.AbpLanguages", "DeletionTime", c => c.DateTime());
            AlterColumn("dbo.AbpEditions", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AbpEditions", "LastModificationTime", c => c.DateTime());
            AlterColumn("dbo.AbpEditions", "DeletionTime", c => c.DateTime());
            AlterColumn("dbo.AbpFeatures", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AbpBackgroundJobs", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AbpBackgroundJobs", "LastTryTime", c => c.DateTime());
            AlterColumn("dbo.AbpBackgroundJobs", "NextTryTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AbpBackgroundJobs", "JobArgs", c => c.String(nullable: false));
            AlterColumn("dbo.AbpAuditLogs", "ExecutionTime", c => c.DateTime(nullable: false));
            CreateIndex("dbo.AbpUserNotifications", new[] { "UserId", "State", "CreationTime" });
            CreateIndex("dbo.AbpBackgroundJobs", new[] { "IsAbandoned", "NextTryTime" });
        }
    }
}
