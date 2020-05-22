using System;
using System.Collections.Generic;
using System.Text;

namespace RoulleteApi.Common.Exceptions
{
    [Serializable]
    public class InvalidBetAmountException : ArgumentException
    {
        public readonly long Amount;

        public InvalidBetAmountException(long amount, string message) : base(message)
        {
            Amount = amount;
        }
    }
}
