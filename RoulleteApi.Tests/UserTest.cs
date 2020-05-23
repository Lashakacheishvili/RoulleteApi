using RoulleteApi.Common.Exceptions;
using RoulleteApi.Core;
using Xunit;

namespace RoulleteApi.Tests
{
    public class UserTest
    {
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 0, 1)]
        [InlineData(100, 1, 101)]
        [InlineData(1, 1000000, 1000001)]
        [InlineData(1000000, 1000000, 2000000)]
        [InlineData(-1, 1, 0)]
        [InlineData(-50, 100, 50)]
        [InlineData(-1000000, 2000000, 1000000)]
        [InlineData(-1000000000, 2000000000, 1000000000)]
        public void Balance_Is_Added_Correctly_If_Value_Is_In_Range(long currenctBalance, long valueToAdd, long expectedBalance)
        {
            var user = new User(currenctBalance);
            user.AddAmountToBalance(valueToAdd);
            Assert.Equal(user.BalanceInCents, expectedBalance);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 0, 1)]
        [InlineData(100, 1, 99)]
        [InlineData(1, 1000000, -999999)]
        [InlineData(1000000, 1000000, 0)]
        [InlineData(-1, 1, -2)]
        [InlineData(-50, 100, -150)]
        [InlineData(-1000000, 2000000, -3000000)]
        [InlineData(-1000000000, 2000000000, -3000000000)]
        public void Balance_Is_Subtracted_Correctly_If_Value_Is_In_Range(long currenctBalance, long valueToSubtract, long expectedBalance)
        {
            var user = new User(currenctBalance);
            user.SubtractAmountFromBalance(valueToSubtract);
            Assert.Equal(user.BalanceInCents, expectedBalance);
        }

        [Theory]
        [InlineData(1, long.MaxValue)]
        [InlineData(long.MaxValue, 1)]
        public void Balance_Overflow_Exception_Is_Thrown_Correctly_On_Adding(long currenctBalance, long valueToSubtract)
        {
            var user = new User(currenctBalance);
            Assert.Throws<BalanceOverflowException>(() => user.AddAmountToBalance(valueToSubtract));
        }

        [Theory]
        [InlineData(-2, long.MaxValue)]
        [InlineData(long.MinValue, 1)]
        public void Balance_Overflow_Exception_Is_Thrown_Correctly_On_Subtcrating(long currenctBalance, long valueToSubtract)
        {
            var user = new User(currenctBalance);
            Assert.Throws<BalanceOverflowException>(() => user.SubtractAmountFromBalance(valueToSubtract));
        }
    }
}