using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NSI.Common.Collation;
using NSI.Common.Enumerations;
using NSI.DataContracts.Dto;
using NSI.DataContracts.Models;
using NSI.DataContracts.Request;

namespace NSI.BusinessLogic.Interfaces
{
    public interface IRequestsManipulation
    {
        Task<Request> SaveRequest(Guid userId, string requestReason, RequestType requestType, IEnumerable<IFormFile> attachments, string[] attachmentTypes);
        Task<IList<Request>> GetRequestsAsync();
        Task<IList<RequestItemDto>> GetEmployeeRequestsAsync(string employeeId, Paging paging);
        Task<Request> UpdateRequestAsync(ReqItemRequest item);
        Task<IList<RequestItemDto>> GetRequestPage(Paging paging);
    }
}