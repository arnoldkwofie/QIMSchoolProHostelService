using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

using Qface.Persistence.EntityFrameworkExtensions;
using Qface.Domain.Shared.Common;
using Qsmart.EventBus.Shared.common;
using Qface.Application.Shared.Common.Interfaces;
using QIMSchoolPro.Hostel.Domain.Entities;

namespace QIMSchoolPro.Hostel.Persistence
{
    public class HostelDbContext : DbContextBase
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDomainEventService _domainEventService;

        public HostelDbContext(DbContextOptions<HostelDbContext> options)
             : base(options)
        {
        }

        public HostelDbContext(
        DbContextOptions<HostelDbContext> options,
        ICurrentUserService currentUserService,
            IDomainEventService domainEventService)
        : base(options)
        {
            _currentUserService = currentUserService;
            _domainEventService = domainEventService;

        }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Bed> Beds { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<RoomTypeAmenity> RoomTypeAmenities { get; set; }
        public DbSet<Booking> Bookings { get; set; }
       


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            //var userId = _currentUserService.UserId ?? Guid.NewGuid().ToString();
            var userId = Guid.NewGuid().ToString();

            SetAuditableEntity(userId);
            var result = await base.SaveChangesAsync(cancellationToken);
            await DispatchEvents();

            return result;
        }

        public override int SaveChanges()
        {
            var userId = _currentUserService.UserId ?? Guid.NewGuid().ToString();

            SetAuditableEntity(userId);
            return base.SaveChanges();
        }

        public void SaveChangesWithIdentityInsert<T>()
        {
            using var transaction = Database.BeginTransaction();
            EnableIdentityInsert<T>();
            SaveChanges();
            DisableIdentityInsert<T>();
            transaction.Commit();
        }
        public async Task SaveChangesWithIdentityInsertAsync<T>(CancellationToken cancellationToken = new CancellationToken())
        {
            await using var transaction = await Database.BeginTransactionAsync();
            await EnableIdentityInsertAsync<T>();
            await SaveChangesAsync(cancellationToken);
            await DisableIdentityInsertAsync<T>();
            await transaction.CommitAsync();
        }

        private async Task DispatchEvents()
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker.Entries<AuditableDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .FirstOrDefault(domainEvent => !domainEvent.IsPublished);
                if (domainEventEntity == null)
                {
                    break;
                }

                domainEventEntity.IsPublished = true;
                await _domainEventService.Publish(domainEventEntity);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HostelDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<DomainEvent>();
        }

    
    }
}
