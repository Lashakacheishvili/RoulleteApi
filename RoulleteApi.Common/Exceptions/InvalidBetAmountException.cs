using System;
using System.Collections.Generic;
using System.Text;

namespace RoulleteApi.Common.Exceptions
{
    public class InvalidBetAmountException : ArgumentException
    {
        public InvalidBetAmountException(string message) : base(message)
        {

        }
    }
}
