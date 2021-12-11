using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSI.Common.Enumerations;
using NSI.DataContracts.Models;
using NSI.DataContracts.Request;

namespace NSI.BusinessLogic.Interfaces
{
    public interface IRequestsManipulation
    {
        Request SaveRequest(Guid userId, string requestReason, RequestType requestType);
        Task<IList<Request>> GetRequestsAsync();
        Task<IList<Request>> GetEmployeeRequestsAsync(string employeeId);
        Task<Request> UpdateRequestAsync(ReqItemRequest item);
    }
}
