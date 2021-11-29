using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSI.BusinessLogic.Interfaces;
using NSI.Common.DataContracts.Base;
using NSI.Common.DataContracts.Enumerations;
using NSI.DataContracts.Models;
using NSI.DataContracts.Request;
using NSI.REST.Filters;
using NSI.REST.Helpers;

namespace NSI.REST.Controllers
{
    [ServiceFilter(typeof(CacheCheck))]
    [ApiController]
    [Route("api/[controller]")]
    public class RequestController : Controller
    {
        private readonly IRequestsManipulation _requestsManipulation;
        private readonly IUsersManipulation _usersManipulation;

        public RequestController(IRequestsManipulation requestsManipulation, IUsersManipulation usersManipulation)
        {
            _requestsManipulation = requestsManipulation;
            _usersManipulation = usersManipulation;
        }

        /// <summary>
        /// Save new request.
        /// </summary>
        [Authorize]
        [PermissionCheck("request:create")]
        [HttpPost]
        public BaseResponse<Request> SaveRequest(DocumentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new BaseResponse<Request>()
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            return new BaseResponse<Request>()
            {
                Data = _requestsManipulation.SaveRequest(
                    _usersManipulation.GetByEmail(AuthHelper.GetRequestEmail(HttpContext)).Id,
                    request.Reason,
                    request.Type
                ),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }
    }
}
