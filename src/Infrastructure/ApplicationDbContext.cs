using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ApplicationDbContext 
        : IdentityDbContext<ApplicationUser, ApplicationRole, int>, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemImage> ItemImages { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<MerchantImage> MerchantImages { get; set; }
        public DbSet<MerchantLocation> MerchantLocations { get; set; }
        public DbSet<MerchantType> MerchantTypes { get; set; }
        public DbSet<MerchantUser> MerchantUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatusType> OrderStatusTypes { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentStatusType> PaymentStatusTypes { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<PriceType> PriceTypes { get; set; }
        public DbSet<SystemConfiguration> SystemConfigurations { get; set; }
        public DbSet<UnitType> UnitTypes { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService?.UserId.ToString();
                        entry.Entity.Created = DateTime.Now; //_dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService?.UserId.ToString();
                        entry.Entity.LastModified = DateTime.Now; //_dateTime.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents();

            return result;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())
                .ToList()
                .ForEach(r => r.DeleteBehavior = DeleteBehavior.Restrict);
        }

        private async Task DispatchEvents()
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .FirstOrDefault();
                if (domainEventEntity == null) break;

                domainEventEntity.IsPublished = true;
                await _domainEventService.Publish(domainEventEntity);
            }
        }
    }
}
