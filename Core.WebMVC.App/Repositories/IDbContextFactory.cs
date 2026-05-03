using DMMS.WebMVC.App.Data;
using DMMS.WebMVC.App.Services;
using System;

namespace DMMS.WebMVC.App.Repositories
{
    public interface IDbContextFactory
    {
        DMMSWebMVCAppContext Create(string connectionString);
    }
}
