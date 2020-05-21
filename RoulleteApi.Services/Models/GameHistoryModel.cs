using System;

namespace RoulleteApi.Services.Models
{
    public class GameHistoryModel
    {
        public Guid Id { get; set; }
        public readonly Guid SpinId;
        public string BetString { get; set; }
        public int WinningNumber { get; set; }
        public long BetAmountInCents { get; set; }
        public long WonAmountInCents { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public GameHistoryModel(Guid id, Guid spinId, string betString, int winningNumber,
            long betAmountInCents, long wonAmountInCents, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            SpinId = spinId;
            BetString = betString;
            WinningNumber = winningNumber;
            BetAmountInCents = betAmountInCents;
            WonAmountInCents = wonAmountInCents;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
