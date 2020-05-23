using System;
using System.Collections.Generic;
using System.Text;

namespace RoulleteApi.Common.Exceptions
{
    [Serializable]
    public class BalanceOverflowException : OverflowException
    {
        public readonly long Amount;

        public BalanceOverflowException(long amount, string message) : base(message)
        {
            Amount = amount;
        }
    }
}
