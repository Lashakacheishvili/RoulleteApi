using System;
using System.Collections.Generic;
using System.Text;

namespace RoulleteApi.Common.Exceptions
{
    public class InvalidJackpotIncreaseAmountException : ArgumentException
    {
        public InvalidJackpotIncreaseAmountException(string message) : base(message)
        {

        }
    }
}
