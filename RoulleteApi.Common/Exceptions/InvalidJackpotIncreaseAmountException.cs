using System;
using System.Collections.Generic;
using System.Text;

namespace RoulleteApi.Common.Exceptions
{
    [Serializable]
    public class InvalidJackpotIncreaseAmountException : ArgumentException
    {
        public readonly long Amount;

        public InvalidJackpotIncreaseAmountException(long amount, string message) : base(message)
        {
            Amount = amount;
        }
    }
}
