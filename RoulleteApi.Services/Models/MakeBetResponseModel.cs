namespace RoulleteApi.Services.Models
{
    public class MakeBetResponseModel
    {
        public long WonAmountInCents { get; set; }

        public MakeBetResponseModel(long wonAmountInCents)
        {
            WonAmountInCents = wonAmountInCents;
        }
    }
}
