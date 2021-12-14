using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSI.Common.Collation;
using NSI.Common.Enumerations;
using NSI.DataContracts.Dto;
using NSI.DataContracts.Models;
using NSI.DataContracts.Request;

namespace NSI.BusinessLogic.Interfaces
{
    public interface IRequestsManipulation
    {
        Request SaveRequest(Guid userId, string requestReason, RequestType requestType);
        Task<IList<Request>> GetRequestsAsync();
        Task<IList<RequestItemDto>> GetEmployeeRequestsAsync(string employeeId, Paging paging);
        Task<Request> UpdateRequestAsync(ReqItemRequest item);
        Task<IList<RequestItemDto>> GetRequestPage(Paging paging);
    }
}
