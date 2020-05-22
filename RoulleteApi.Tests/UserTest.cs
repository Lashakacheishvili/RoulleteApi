using RoulleteApi.Common;
using RoulleteApi.Core;
using System;
using Xunit;

namespace RoulleteApi.Tests
{
    public class UserTest
    {
        [Theory]
        [InlineData(100, 1, 101)]
        [InlineData(51165161, 5, 51165166)]
        [InlineData(0, 0, 0)]
        [InlineData(1, 0, 1)]
        public void Positive_Balance_Is_Added_Correctly(long currenctBalance, long valueToAdd, long expectedBalance)
        {
            var user = new User();

            // Initialize users current balance
            user.AddAmountToBalance(currenctBalance);
            user.AddAmountToBalance(valueToAdd);

            Assert.Equal(user.BalanceInCents, expectedBalance);
        }

        [Fact]
        public void Negative_Balance_Is_Added_Correctly()
        {
            var user = new User();
            var initialBalance = -50;
            var amountToAdd = 100;
            var expectedResult = 50;

            user.SubtractAmountFromBalance(initialBalance * -1);
            user.AddAmountToBalance(amountToAdd);

            Assert.Equal(user.BalanceInCents, expectedResult);
        }

        [Theory]
        [InlineData(100, 1, 99)]
        [InlineData(long.MaxValue, long.MaxValue, 0)]
        [InlineData(0, long.MaxValue - 1, long.MinValue)]
        [InlineData(1, 0, 1)]
        public void Positive_Balance_Is_Subtracted_Correctly(long currenctBalance, long valueToSubtract, long expectedBalance)
        {
            var user = new User();

            // Initialize users current balance
            user.AddAmountToBalance(currenctBalance);

            user.SubtractAmountFromBalance(valueToSubtract);

            Assert.Equal(user.BalanceInCents, expectedBalance);
        }

     
        [Fact]
        public void Negative_Balance_Is_Subtracted_Correctly()
        {
            var user = new User();
            var initialBalance = -50;
            var amountSubtract = 100;
            var expectedResult = -150;

            user.SubtractAmountFromBalance(initialBalance * -1);
            user.SubtractAmountFromBalance(amountSubtract);

            Assert.Equal(user.BalanceInCents, expectedResult);
        }
    }
}
