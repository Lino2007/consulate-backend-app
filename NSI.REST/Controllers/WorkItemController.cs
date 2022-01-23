using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSI.Common.DataContracts.Base;
using NSI.Common.DataContracts.Enumerations;
using NSI.Common.Exceptions;
using NSI.DataContracts.Request;
using NSI.REST.Helpers;
using NSI.DataContracts.Response;
using NSI.BusinessLogic.Implementations;
using NSI.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace NSI.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ExcludeFromCodeCoverage]
    public class WorkItemController : Controller
    {

        private readonly IWorkItemsManipulation _workItemsManipulation;

        public WorkItemController(IWorkItemsManipulation workItemsManipulation)
        {
            _workItemsManipulation = workItemsManipulation;
        }

        [Authorize]
        [HttpPost("workitems")]
        public async Task<SearchWorkItemsResponse> SearchWorkItems(SearchWorkItemsRequest request)
        {
            if (!ModelState.IsValid)
                return new SearchWorkItemsResponse() { 
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed 
                };

            return new SearchWorkItemsResponse()
            {
                Data = await _workItemsManipulation.GetWorkItemsAsync(request.WorkItemIds, request.Paging, request.SortCriteria, request.FilterCriteria),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }
    }
}
