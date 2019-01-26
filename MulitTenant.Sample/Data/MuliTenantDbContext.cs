using Microsoft.EntityFrameworkCore;
using MulitTenant.Sample.Models;
using MulitTenant.Sample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;

namespace MulitTenant.Sample.Data
{
    public class MuliTenantDbContext:DbContext
    {
        private int _tenantId;
        public MuliTenantDbContext(DbContextOptions<MuliTenantDbContext> options,IAppSession appSession):base(options)
        {
            _tenantId = appSession.TenantId;
        }

        public DbSet<Tenant> Tenants { get; set; }

        public DbSet<Post> Posts { get; set; }
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.BaseType == typeof(BaseEntity))
                {
                    ConfigureGlobalFiltersMethodInfo
                    .MakeGenericMethod(entityType.ClrType)
                    .Invoke(this, new object[] { modelBuilder });
                }
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            ChangeTracker.DetectChanges();
            var entities = ChangeTracker.Entries().Where(e => e.State == EntityState.Added && e.Entity.GetType().BaseType == typeof(BaseEntity));
            foreach (var item in entities)
            {
                (item.Entity as BaseEntity).TenantId = _tenantId;
            }
            return await base.SaveChangesAsync(cancellationToken);
        }


        private static MethodInfo ConfigureGlobalFiltersMethodInfo = typeof(MuliTenantDbContext)
            .GetMethod(nameof(ConfigureGlobalFilters), BindingFlags.Instance | BindingFlags.NonPublic);
        protected void ConfigureGlobalFilters<T>(ModelBuilder builder) where T : BaseEntity
        {
            builder.Entity<T>().HasQueryFilter(e => e.TenantId == _tenantId && !e.IsDeleted);           
        }
    }
}
