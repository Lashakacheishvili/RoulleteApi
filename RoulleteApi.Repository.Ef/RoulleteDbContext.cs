using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RoulleteApi.Core;
using RoulleteApi.Repository.Ef.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RoulleteApi.Repository.Ef
{
    public class RoulleteDbContext : IdentityDbContext<User, UserRole, Guid>
    {
        public DbSet<GameHistory> GameHistories { get; set; }

        // Well there is actually always one value in database, and it is always updated.
        public DbSet<Jackpot> Jackpots { get; set; }

        public DbSet<RequestLog> RequestLogs { get; set; }

        public RoulleteDbContext(DbContextOptions<RoulleteDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureJackpot();

            modelBuilder.SeedUser();
            modelBuilder.SeedJackpot();

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // NOTE: It is true that I map CreatedAt and UpdatedAt values in code and here they will be overwrote
            //       But I will still use this sugar to be sure not a single entity is missed, and leave mapping
            //       logic in code also, because someday I can change database or ORM, that will not have this kind
            //       of sugar, and custom mapping is needed. CreateAt is done in BaseEntityConstructor, Update is
            //       encapsulated in repository methods.

            var addedEntites = ChangeTracker.Entries<TrackedObject>().Where(c => c.State == EntityState.Added).ToList();
            addedEntites.ForEach(c =>
            {
                // Create and Update date to be equal, otherwise there will be difference in ticks
                var utcNow = DateTime.UtcNow;

                c.Property(p => p.CreatedAt).CurrentValue = utcNow;
                c.Property(p => p.UpdatedAt).CurrentValue = utcNow;
            });

            var updatedEntities = ChangeTracker.Entries<TrackedObject>().Where(c => c.State == EntityState.Modified).ToList();

            updatedEntities.ForEach(c =>
            {
                c.Property(p => p.UpdatedAt).CurrentValue = DateTime.UtcNow;
            });

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
