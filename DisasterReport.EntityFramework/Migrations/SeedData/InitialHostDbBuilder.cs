﻿using DisasterReport.EntityFramework;
using EntityFramework.DynamicFilters;

namespace DisasterReport.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly DisasterReportDbContext _context;

        public InitialHostDbBuilder(DisasterReportDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
