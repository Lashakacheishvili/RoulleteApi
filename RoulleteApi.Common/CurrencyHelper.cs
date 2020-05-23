using RoulleteApi.Common.Exceptions;
using RoulleteApi.Common.Resources;
using System;

namespace RoulleteApi.Common
{
    public static class CurrencyHelper
    {
        // 1 cent is 100 milly cents.
        // Here is helper methods that will multiply and divide by 100 for us.
        // Magic numbers not to appear anywhere.

        public const long CentUnitInMillyCents = 100;

        public static long ConvertCentsToMillyCents(this long amountToConvertInCents)
        {
            if (amountToConvertInCents < 0)
            {
                throw new InvalidCurrencyAmountException(amountToConvertInCents, ExceptionMessages.ProvidedValueIsInvalidForCurrentOperation);
            }

            if (long.MaxValue / CentUnitInMillyCents < amountToConvertInCents)
            {
                throw new CurrencyOverflowException(amountToConvertInCents, ExceptionMessages.ValueIsTooLargeToConvert);
            }

            return amountToConvertInCents * CentUnitInMillyCents;
        }

        public static long ConvertMillyCentsToCents(this long amountToConvertInMillyCents)
        {
            if (amountToConvertInMillyCents < 0)
            {
                throw new InvalidCurrencyAmountException(amountToConvertInMillyCents, ExceptionMessages.ProvidedValueIsInvalidForCurrentOperation);
            }

            return amountToConvertInMillyCents / CentUnitInMillyCents;
        }
    }
}
