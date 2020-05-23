using Microsoft.AspNetCore.Identity;
using RoulleteApi.Common.Exceptions;
using RoulleteApi.Common.Resources;
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

        public User(long initialBalanceInCents) : this()
        {
            BalanceInCents = initialBalanceInCents;
        }

        public void SubtractAmountFromBalance(long amountToSubtractInCents)
        {
            if (amountToSubtractInCents < 0)
            {
                throw new InvalidCurrencyAmountException(amountToSubtractInCents, ExceptionMessages.ProvidedValueIsInvalidForCurrentOperation);
            }

            if (BalanceInCents < long.MinValue + amountToSubtractInCents)
            {
                throw new BalanceOverflowException(amountToSubtractInCents, ExceptionMessages.ValueIsTooLargeToSubtractBalance);
            }

            BalanceInCents -= amountToSubtractInCents;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddAmountToBalance(long amountToAddInCents)
        {
            if (amountToAddInCents < 0)
            {
                throw new InvalidCurrencyAmountException(amountToAddInCents, ExceptionMessages.ProvidedValueIsInvalidForCurrentOperation);
            }

            if (BalanceInCents >= 0 && amountToAddInCents > long.MaxValue - BalanceInCents)
            {
                throw new BalanceOverflowException(amountToAddInCents, ExceptionMessages.ValueIsTooLargeToAddBalance);
            }

            BalanceInCents += amountToAddInCents;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
