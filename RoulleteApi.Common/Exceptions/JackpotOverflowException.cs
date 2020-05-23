using System;
using System.Collections.Generic;
using System.Text;

namespace RoulleteApi.Common.Exceptions
{
    [Serializable]
    public class JackpotOverflowException : OverflowException
    {
        public readonly long Amount;

        public JackpotOverflowException(long amount, string message) : base(message)
        {
            Amount = amount;
        }
    }
}
