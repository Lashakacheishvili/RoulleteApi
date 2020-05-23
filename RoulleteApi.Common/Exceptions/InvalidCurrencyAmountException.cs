using System;
using System.Collections.Generic;
using System.Text;

namespace RoulleteApi.Common.Exceptions
{
    [Serializable]
    public class InvalidCurrencyAmountException : ArgumentException
    {
        public readonly long Amount;

        public InvalidCurrencyAmountException(long amount, string message) : base(message)
        {
            Amount = amount;
        }
    }
}
