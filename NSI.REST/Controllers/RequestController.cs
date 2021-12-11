using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSI.BusinessLogic.Interfaces;
using NSI.Common.Collation;
using NSI.Common.DataContracts.Base;
using NSI.Common.DataContracts.Enumerations;
using NSI.DataContracts.Models;
using NSI.DataContracts.Request;
using NSI.DataContracts.Response;
using NSI.REST.Filters;
using NSI.REST.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<ReqResponse> GetRequests([FromQuery]BasicRequest basicRequest) {
            
            if(!ModelState.IsValid)
            {
                return new ReqResponse()
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            return new ReqResponse()
            {
                Data = await _requestsManipulation.GetRequestsAsync(),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }

        [HttpGet("employee/{id}")]
        public async Task<ReqResponse> getRequestsByEmployeeId(string id)
        {
            if (!ModelState.IsValid)
            {
                return new ReqResponse()
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            var requests = await _requestsManipulation.GetEmployeeRequestsAsync(id);
            return new ReqResponse()
            {
                Data = requests,
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }

        [HttpPut]
        public async Task<ReqResponse> updateRequest(ReqItemRequest req)
        {
            if (!ModelState.IsValid)
            {
                return new ReqResponse()
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            var request = await _requestsManipulation.UpdateRequestAsync(req);
            if (request == null)
            {
                Error newErr = new Error();
                newErr.Message = $"Request with id {req.id} does not exist";
                return new ReqResponse()
                {
                    Data = null,
                    Error = newErr,
                    Success = ResponseStatus.Failed
                };
            }

            return new ReqResponse()
            {
                Data = new List<Request> () {request} ,
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }
    }
}
