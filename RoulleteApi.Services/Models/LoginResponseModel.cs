namespace RoulleteApi.Services.Models
{
    public class LoginResponseModel
    {
        public string AccessToken { get; set; }

        public LoginResponseModel(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}
