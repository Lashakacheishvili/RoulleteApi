using RoulleteApi.Common.Exceptions;
using RoulleteApi.Common.Resources;
using System;
using System.Resources;

namespace RoulleteApi.Core
{
    public class Jackpot : BaseEntity<int>
    {
        public long AmountInMillyCents { get; protected set; }
        public byte[] ConcurrencyStamp { get; set; }

        public Jackpot() { }

        public Jackpot(long amountInMillyCents, byte[] concurrencyStamp)
        {
            AmountInMillyCents = amountInMillyCents;
            ConcurrencyStamp = concurrencyStamp;
        }

        /// <summary>
        /// <paramref name="increaseAmountInMillyCents"/> 
        /// 100 millycent is 1 cent.  
        /// This is done because it is possible to bet 1 cent and we need to put certain percentage of that bet amount in jackpot. 
        /// </summary>
        /// <param name="increaseAmountInMillyCents"></param>
        public void IncreaseJackpotAmount(long increaseAmountInMillyCents)
        {
            if (increaseAmountInMillyCents < 0)
            {
                throw new InvalidCurrencyAmountException(increaseAmountInMillyCents, ExceptionMessages.ProvidedValueIsInvalidForCurrentOperation);
            }

            if (AmountInMillyCents > long.MaxValue - increaseAmountInMillyCents)
            {
                throw new JackpotOverflowException(increaseAmountInMillyCents, ExceptionMessages.ValueIsTooLargeToAddJackpot);
            }

            AmountInMillyCents += increaseAmountInMillyCents;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
