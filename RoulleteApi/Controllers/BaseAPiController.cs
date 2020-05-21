using Microsoft.AspNetCore.Mvc;
using RoulleteApi.Common;
using System;

namespace RoulleteApi.Controllers
{
    public class BaseAPiController : Controller
    {
        protected IActionResult TransformServiceErrorToHttpStatusCode(ServiceErrorMessage serviceErrorMessage)
            => serviceErrorMessage.Code switch
            {
                ErrorStatusCodes.NOT_FOUND => NotFound(serviceErrorMessage),
                ErrorStatusCodes.ALREADY_EXISTS => Conflict(serviceErrorMessage),
                ErrorStatusCodes.INVALID_VALUE => UnprocessableEntity(serviceErrorMessage),
                _ => BadRequest(serviceErrorMessage)
            };

        protected Guid UserId => Guid.Parse(User.GetUserIdFromPrincipal());

        protected IActionResult HandleResult<T>(ServiceResponse<T> serviceResult)
            => serviceResult.IsSuccess
             ? Ok(serviceResult.Result)
            : TransformServiceErrorToHttpStatusCode(serviceResult.ServiceErrorMessage);
    }
}
