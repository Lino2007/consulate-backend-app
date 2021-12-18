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
        //[Authorize]
        //[PermissionCheck("request:create")]
        [HttpPost]
        public async Task<BaseResponse<Request>> SaveRequest([FromForm] DocumentRequest request)
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
                Data = await _requestsManipulation.SaveRequest(
                    _usersManipulation.GetByEmail(AuthHelper.GetRequestEmail(HttpContext)).Id,
                    request.Reason,
                    request.Type,
                    request.Attachments,
                    request.AttachmentTypes
                ),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }

        [HttpGet]
        public async Task<ReqItemResponse> GetRequests([FromQuery]BasicRequest basicRequest) {
            
            if(!ModelState.IsValid)
            {
                return new ReqItemResponse()
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            return new ReqItemResponse()
            {
                Data = await _requestsManipulation.GetRequestsAsync(),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }

        [HttpGet("paging")]
        public async Task<ReqItemListResponse> GetRequestsWithPaging([FromQuery] BasicRequest basicRequest)
        {

            if (!ModelState.IsValid || basicRequest.Paging == null)
            {
                return new ReqItemListResponse()
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            if (basicRequest.Paging.RecordsPerPage < 0)
            {
                basicRequest.Paging.RecordsPerPage = 5;
            }

            if (basicRequest.Paging.Page < 0)
            {
                basicRequest.Paging.Page = 0;
            }

            return new ReqItemListResponse()
            {
                Paging = basicRequest.Paging,
                Data = await _requestsManipulation.GetRequestPage(basicRequest.Paging),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }

        [HttpGet("employee/{id}")]
        public async Task<ReqItemListResponse> getRequestsByEmployeeId(string id, [FromQuery] BasicRequest basicRequest)
        {
            if (!ModelState.IsValid || basicRequest.Paging == null)
            {
                return new ReqItemListResponse()
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            if (basicRequest.Paging.RecordsPerPage < 0)
            {
                basicRequest.Paging.RecordsPerPage = 5;
            }

            if (basicRequest.Paging.Page < 0)
            {
                basicRequest.Paging.Page = 0;
            }

            return new ReqItemListResponse()
            {
                Paging = basicRequest.Paging,
                Data = await _requestsManipulation.GetEmployeeRequestsAsync(id, basicRequest.Paging),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }

        [HttpPut]
        public async Task<ReqItemResponse> updateRequest(ReqItemRequest req)
        {
            if (!ModelState.IsValid)
            {
                return new ReqItemResponse()
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
                return new ReqItemResponse()
                {
                    Data = null,
                    Error = newErr,
                    Success = ResponseStatus.Failed
                };
            }

            return new ReqItemResponse()
            {
                Data = new List<Request> () {request} ,
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }
    }
}
