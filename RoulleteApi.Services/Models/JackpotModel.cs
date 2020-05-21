using System;

namespace RoulleteApi.Services.Models
{
    public class JackpotModel
    {
        public int Id { get; set; }
        public long AmountInMillyCents { get; set; }
        public byte[] ConcurrencyStamp { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public JackpotModel(int id, long amountInMillyCents, byte[] concurrencyStamp, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            AmountInMillyCents = amountInMillyCents;
            ConcurrencyStamp = concurrencyStamp;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
