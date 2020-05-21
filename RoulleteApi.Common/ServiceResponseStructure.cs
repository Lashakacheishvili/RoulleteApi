namespace RoulleteApi.Common
{
    public class ServiceResponse
    {
        public bool IsSuccess { get; protected set; }
        public bool Failed => !IsSuccess;
        public ServiceErrorMessage ServiceErrorMessage { get; protected set; }

        public ServiceResponse Ok() => MapResult(true, null);
        public ServiceResponse Fail(ServiceErrorMessage serviceErrorMessage) => MapResult(false, serviceErrorMessage);

        private ServiceResponse MapResult(bool isSuccess, ServiceErrorMessage serviceErrorMessage)
        {
            IsSuccess = isSuccess;
            ServiceErrorMessage = serviceErrorMessage;

            return this;
        }
    }

    public class ServiceResponse<T> : ServiceResponse
    {
        public T Result { get; private set; }

        public ServiceResponse<T> Ok(T successObj) => MapResult(true, successObj, default);

        public new ServiceResponse<T> Fail(ServiceErrorMessage errorMessage) => MapResult(false, default, errorMessage);

        private ServiceResponse<T> MapResult(bool isSuccess, T successObj, ServiceErrorMessage serviceErrorMessage)
        {
            IsSuccess = isSuccess;
            Result = successObj;
            ServiceErrorMessage = serviceErrorMessage;

            return this;
        }
    }

    public class ServiceErrorMessage
    {
        public ErrorStatusCodes Code { get; set; }
        public string Description { get; set; }

        public ServiceErrorMessage NotFound(string subject) => MapResult(ErrorStatusCodes.NOT_FOUND, $"{subject} Was not found");
        public ServiceErrorMessage InvalidValue(string subject) => MapResult(ErrorStatusCodes.INVALID_VALUE, $"{subject} Was Invalid");
        public ServiceErrorMessage AlreadyExists(string subject) => MapResult(ErrorStatusCodes.ALREADY_EXISTS, $"{subject} Already exists");
        public ServiceErrorMessage ChangesNotSaved(string source) => MapResult(ErrorStatusCodes.CHANGES_NOT_SAVED, $"Changes were not saved in {source}");
        public ServiceErrorMessage BadRequest(string description) => MapResult(ErrorStatusCodes.DEFAULT, description);

        private ServiceErrorMessage MapResult(ErrorStatusCodes statusCode, string description)
        {
            Code = statusCode;
            Description = description;

            return this;
        }

    }

    public enum ErrorStatusCodes
    {
        DEFAULT,
        NOT_FOUND,
        INVALID_VALUE,
        ALREADY_EXISTS,
        INTERNAL_SERVER_ERROR,
        CHANGES_NOT_SAVED
    }
}
