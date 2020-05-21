using Microsoft.EntityFrameworkCore;
using RoulleteApi.Core;

namespace RoulleteApi.Repository.Ef.Configuration
{
    public static class ModelBuilderEntityConfiguration
    {
        public static void ConfigureJackpot(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jackpot>().Property(c => c.ConcurrencyStamp).IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();
        }
    }
}
