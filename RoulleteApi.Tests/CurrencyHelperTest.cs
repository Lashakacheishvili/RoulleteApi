using RoulleteApi.Common;
using RoulleteApi.Common.Exceptions;
using System;
using Xunit;

namespace RoulleteApi.Tests
{
    public class CurrencyHelperTest
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(99, 0)]
        [InlineData(100, 1)]
        [InlineData(150000, 1500)]
        [InlineData(1000040, 10000)]
        [InlineData(1000099, 10000)]
        [InlineData(long.MaxValue, 92233720368547758)]
        public void MillyCents_To_Cents_Are_Converted_Correctly(long valueInMillyCents, long expectedResult)
           => Assert.Equal(valueInMillyCents.ConvertMillyCentsToCents(), expectedResult);

        [Theory]
        [InlineData(-1)]
        [InlineData(-100000)]
        public void MillyCents_To_Cents_Throws_An_Error_If_TargetValue_Is_Less_Than_Zero(long targetValue)
         => Assert.Throws<InvalidCurrencyAmountException>(() => { targetValue.ConvertMillyCentsToCents(); });

        [Theory]
        [InlineData(1, 100)]
        [InlineData(0, 0)]
        [InlineData(123456789, 12345678900)]
        [InlineData(92233720368547758, 9223372036854775800)] // It is the maximum value that can be converted
        public void Cents_To_MillyCents_Are_Converted_Correctly_When_Value_Between_Calculative_Range(long valueInCents, long expectedResult)
            => Assert.Equal(valueInCents.ConvertCentsToMillyCents(), expectedResult);

        [Theory]
        [InlineData(-1)]
        [InlineData(-100000)]
        public void Cents_To_MillyCents_Throws_An_Error_If_TargetValue_Is_Less_Than_Zero(long targetValue)
            => Assert.Throws<InvalidCurrencyAmountException>(() => { targetValue.ConvertMillyCentsToCents(); });

        [Theory]
        [InlineData(long.MaxValue)]
        [InlineData(92233720368547759)] // it is the minimum value that will throw an error
        public void Cents_To_MillyCents_Throws_An_Error_If_TargetValue_Is_Greater_Than_Should_Be(long targetValue)
            => Assert.Throws<CurrencyOverflowException>(() => { targetValue.ConvertCentsToMillyCents(); });
    }
}
