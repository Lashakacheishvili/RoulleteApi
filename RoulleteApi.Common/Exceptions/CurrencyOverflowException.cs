using System;
using System.Collections.Generic;
using System.Text;

namespace RoulleteApi.Common.Exceptions
{
    [Serializable]
    public class CurrencyOverflowException : OverflowException
    {
        public readonly long Amount;

        public CurrencyOverflowException(long amount, string message) : base(message)
        {
            Amount = amount;
        }
    }
}
