using Microsoft.AspNetCore.Identity;
using RoulleteApi.Common.Exceptions;
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

        public void SubtractAmountFromBalance(long betAmountInCents)
        {
            if (betAmountInCents < 0)
            {
                throw new InvalidBetAmountException(betAmountInCents, $"{betAmountInCents} is less than zero, choose positive number");
            }

            BalanceInCents -= betAmountInCents;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddAmountToBalance(long amountToAddInCents)
        {
            if (amountToAddInCents < 0)
            {
                throw new InvalidBetAmountException(amountToAddInCents, $"{amountToAddInCents} is less than zero, choose positive number");
            }

            BalanceInCents += amountToAddInCents;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
