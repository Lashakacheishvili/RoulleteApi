using System;
using System.Collections.Generic;
using System.Text;

namespace RoulleteApi.Common
{
    public static class CurrencyHelper
    {
        // 1 cent is 100 milly cents.
        // Here is helper methods that will multiply and divide by 100 for us.
        // Magic numbers not to appear anywhere.

        public const long CentUnitInMillyCents = 100;

        public static long ConvertCentsToMillyCents(this long targetValue)
        {
            if (long.MaxValue / CentUnitInMillyCents < targetValue)
            {
                throw new ArgumentException($"{targetValue} is too large to be converted in millycents");
            }

            if (targetValue < 0)
            {
                throw new ArgumentException($"{targetValue} is less than zero, take positive number");
            }

            return targetValue * CentUnitInMillyCents;
        }

        public static long ConvertMillyCentsToCents(this long targetValue)
        {
            if (targetValue < 0)
            {
                throw new ArgumentException($"{targetValue} is less than zero, take positive number");
            }

            return targetValue / CentUnitInMillyCents;
        }
    }
}
