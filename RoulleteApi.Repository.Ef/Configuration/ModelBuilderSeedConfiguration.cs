using Microsoft.EntityFrameworkCore;
using RoulleteApi.Core;
using System.Collections.Generic;
using System.Text;

namespace RoulleteApi.Repository.Ef.Configuration
{
    public static class ModelBuilderSeedConfiguration
    {
        public static void SeedUser(this ModelBuilder modelBuilder)
        {
            var seedTargetUser = new User()
            {
                UserName = "gisaiashvili",
                NormalizedUserName = "GISAIASHVILI",
                SecurityStamp = "62dc8efa-b3e0-4d76-ad2a-360bca02951d",
                ConcurrencyStamp = "238c3691-7ac7-4eef-b1bc-3154ba5a032d",
                LockoutEnabled = false,
                TwoFactorEnabled = false,
                // Password is 123456!Aa
                PasswordHash = "AQAAAAEAACcQAAAAENVZ812bufH4FnJvHyQSt5WigcNqbcn3CcHE/U8fIw6ZjifFeZR0qPQh8ixLu2L5+A==",
            };

            seedTargetUser.AddAmountToBalance(100000); // 1000 USD

            modelBuilder.Entity<User>().HasData(
            new List<User> { seedTargetUser });
        }

        public static void SeedJackpot(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jackpot>().HasData(
                 new List<Jackpot> {
                    new Jackpot()
                    {
                        Id = 1,
                        ConcurrencyStamp = Encoding.ASCII.GetBytes("228bg691-7ac7-452f-b1bc-ae54ba5a03r4"),
                    },
                 });
        }
    }
}
