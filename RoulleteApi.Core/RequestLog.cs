namespace RoulleteApi.Core
{
    public class RequestLog : BaseEntity<int>
    {
        // UserId can be empty, e.x. when logging login attempts. We don't have user yet.
        public string UserId { get; set; }

        public string IpAddress { get; set; }
        public string Request { get; set; }

        protected RequestLog() { }

        public RequestLog(string userId, string ipAddress, string requestObject)
        {
            UserId = userId;
            IpAddress = ipAddress;
            Request = requestObject;
        }
    }
}
