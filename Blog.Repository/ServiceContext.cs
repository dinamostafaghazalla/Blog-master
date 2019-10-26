using Blog.Models;
using Blog.Repository.Interface;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Blog.Repository
{
    public class ServiceContext : ApiAuthorizationDbContext<ApplicationUser>, IServiceContext
    {
        public ServiceContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) :
            base(options, operationalStoreOptions)
        {
        }

        #region Entites

        public DbSet<Article> Articles { get; set; }

        #endregion
    }
}