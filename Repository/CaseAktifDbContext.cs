using Domain.CaseAktifAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Repository
{
    public class CaseAktifDbContext : DbContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CaseAktifDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {

        }

        #region Domains

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<CustomerOrder> CustomerOrder { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomerOrder>()
           .HasOne(co => co.Customer)
           .WithMany()
           .HasForeignKey("CustomerId");

            modelBuilder.Entity<CustomerOrder>()
                .HasMany(co => co.Products)
                .WithOne()
                .HasForeignKey("CustomerOrderId");
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.ChangeTracker.DetectChanges();
            var added = this.ChangeTracker.Entries()
                .Where(t => t.State == EntityState.Added)
                .Select(t => t.Entity)
                .ToArray();

            foreach (var entity in added)
            {
                if (entity is Entity track)
                {
                    track.CreatedDate = DateTime.Now;
                }
            }

            var modified = this.ChangeTracker.Entries()
                .Where(t => t.State == EntityState.Modified)
                .Select(t => t.Entity)
                .ToArray();

            foreach (var entity in modified)
            {
                if (entity is Entity track)
                {
                    track.ModifiedDate = DateTime.Now;
                }
            }
            return base.SaveChangesAsync();
        }


    }
}
