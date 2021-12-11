using NSI.DataContracts.Models;
using NSI.DataContracts.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces
{
    public interface IRequestsRepository
    {
        Request SaveRequest(Request request);
        Task<IList<Request>> GetRequestsAsync();
        Task<IList<Request>> GetEmployeeRequestsAsync(string employeeId);
        Task<Request> UpdateRequestAsync(ReqItemRequest item);
    }
}
