using DMMS.WebMVC.App.Data;
using DMMS.WebMVC.App.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace DMMS.WebMVC.App.Services
{
    public class DbContextFactory : IDbContextFactory
    {
        public DMMSWebMVCAppContext Create(string connectionString)
        {
            var options = new DbContextOptionsBuilder<DMMSWebMVCAppContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new DMMSWebMVCAppContext(options);
        }
    }
}
