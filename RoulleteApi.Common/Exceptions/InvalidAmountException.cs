using System;
using System.Collections.Generic;
using System.Text;

namespace RoulleteApi.Common.Exceptions
{
    [Serializable]
    public class InvalidAmountArumentException : ArgumentException
    {
        public readonly long Amount;

        public InvalidAmountArumentException(long amount, string message) : base(message)
        {
            Amount = amount;
        }
    }
}
