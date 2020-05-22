using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace RoulleteApi.Core
{
    public class User : IdentityUser<Guid>, IBaseEntity<Guid>
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public long BalanceInCents { get; protected set; }

        public HashSet<GameHistory> GameHistories { get; set; }

        public User()
        {
            // Create and Update date to be equal, otherwise there will be difference in ticks
            var dateTimeNow = DateTime.UtcNow;

            Id = Guid.NewGuid();
            CreatedAt = dateTimeNow;
            UpdatedAt = dateTimeNow;
            IsDeleted = false;
            GameHistories = new HashSet<GameHistory>();
        }

        // I could add input validation here and throw custom error, and then handle it.
        public void SubtractBetAmountFromBalance(long betAmountInCents)
        {
            BalanceInCents -= betAmountInCents;
            UpdatedAt = DateTime.UtcNow;
        }

        // I could add input validation here and throw custom error, and then handle it.
        public void AddAmountToBalance(long amountToAddInCents)
        {
            BalanceInCents += amountToAddInCents;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
