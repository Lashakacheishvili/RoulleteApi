using RoulleteApi.Common.Exceptions;
using System;

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
                throw new InvalidJackpotIncreaseAmountException($"{increaseAmountInMillyCents} is less than zero, choose positive number");
            }

            if (AmountInMillyCents > long.MaxValue - increaseAmountInMillyCents)
            {
                throw new InvalidJackpotIncreaseAmountException($"{increaseAmountInMillyCents} is too large to store");
            }

            AmountInMillyCents += increaseAmountInMillyCents;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
