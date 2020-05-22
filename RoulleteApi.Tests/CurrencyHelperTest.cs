using RoulleteApi.Common;
using System;
using Xunit;

namespace RoulleteApi.Tests
{
    public class CurrencyHelperTest
    {
        [Theory]
        [InlineData(1012300123, 10123001)]
        [InlineData(1012300199, 10123001)]
        [InlineData(1, 0)]
        [InlineData(150000, 1500)]
        [InlineData(99, 0)]
        [InlineData(100, 1)]
        [InlineData(long.MaxValue, 92233720368547758)]
        public void MillyCents_To_Cents_Are_Converted_Correctly(long valueInMillyCents, long expectedResult)
           => Assert.Equal(valueInMillyCents.ConvertMillyCentsToCents(), expectedResult);

        [Theory]
        [InlineData(-1)]
        [InlineData(-1000)]
        [InlineData(long.MinValue)]
        public void MillyCents_To_Cents_Throws_An_Error_If_TargetValue_Is_Less_Than_Zero(long targetValue)
         => Assert.Throws<ArgumentException>(() => { targetValue.ConvertMillyCentsToCents(); });

        [Theory]
        [InlineData(1, 100)]
        [InlineData(0, 0)]
        [InlineData(1321356, 132135600)]
        public void Cents_To_MillyCents_Are_Converted_Correctly_When_Value_Between_Calculative_Range(long valueInCents, long expectedResult)
            => Assert.Equal(valueInCents.ConvertCentsToMillyCents(), expectedResult);

        [Theory]
        [InlineData(-1)]
        [InlineData(-1651561231)]
        [InlineData(long.MinValue)]
        public void Cents_To_MillyCents_Throws_An_Error_If_TargetValue_Is_Less_Than_Zero(long targetValue)
            => Assert.Throws<ArgumentException>(() => { targetValue.ConvertMillyCentsToCents(); });

        [Theory]
        [InlineData(long.MaxValue)]
        [InlineData(92233720368547759)]
        public void Cents_To_MillyCents_Throws_An_Error_If_TargetValue_Is_Greater_Than_Should_Be(long targetValue)
            => Assert.Throws<ArgumentException>(() => { targetValue.ConvertCentsToMillyCents(); });
    }
}
