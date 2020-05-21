namespace RoulleteApi.Services.Models
{
    public class UserBalanceResponseModel
    {
        public long BalanceInCents { get; set; }

        public UserBalanceResponseModel(long balanceInCents)
        {
            BalanceInCents = balanceInCents;
        }
    }
}
