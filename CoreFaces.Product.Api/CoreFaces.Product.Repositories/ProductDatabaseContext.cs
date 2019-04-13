using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using CoreFaces.Product.Models.Mapping;
using CoreFaces.Product.Models.Domain;
using CoreFaces.Product.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace CoreFaces.Product.Repositories
{
    public class ProductDatabaseContext : DbContext
    {
        public ProductDatabaseContext(DbContextOptions<ProductDatabaseContext> options) : base(options)
        {
            //bool status = this.Database.EnsureDeleted();
            //IExecutionStrategy dd = this.Database.CreateExecutionStrategy();
            //bool test = this.Database.EnsureCreated();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new TestMap(modelBuilder.Entity<Test>());
            new ProductCategoryMap(modelBuilder.Entity<ProductCategory>());
            new ProductCategoryTranslationMap(modelBuilder.Entity<ProductCategoryTranslation>());
            new ProductMap(modelBuilder.Entity<CoreFaces.Product.Models.Domain.Product>());
            new ProductTranslationMap(modelBuilder.Entity<ProductTranslation>());
            new BrandMap(modelBuilder.Entity<Brand>());

        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            AddTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        private void AddTimestamps()
        {
            var changeSet = ChangeTracker.Entries<EntityBase>();
            if (changeSet != null)
            {
                foreach (var entry in changeSet.Where(c => c.State != EntityState.Unchanged))
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Entity.UpdateDate = DateTime.Now;
                        entry.Entity.CreateDate = DateTime.Now;
                    }
                    entry.Entity.UpdateDate = DateTime.Now;
                }
            }
        }
    }
}
