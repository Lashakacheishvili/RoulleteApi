using System;

namespace RoulleteApi.Core
{
    public class GameHistory : BaseEntity<Guid>
    {
        public readonly Guid SpinId;
        public string BetString { get; protected set; }
        public int WinningNumber { get; protected set; }
        public long BetAmountInCents { get; protected set; }
        public long WonAmountInCents { get; protected set; }
        public Guid? UserId { get; set; }
        public User User { get; set; }

        protected GameHistory() { }

        public GameHistory(string betString, int winningNumber, long betAmountInCents, long wonAmountInCents)
        {
            SpinId = Guid.NewGuid();
            BetString = betString;
            WinningNumber = winningNumber;
            BetAmountInCents = betAmountInCents;
            WonAmountInCents = wonAmountInCents;
        }
    }
}
