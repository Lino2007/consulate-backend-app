using NSI.DataContracts.Models;
using NSI.DataContracts.Request;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces
{
    public interface IRequestsRepository
    {
        Request SaveRequest(Request request);
        Task<IList<Request>> GetRequestsAsync();
        IQueryable<Request> GetEmployeeRequestsAsync(string employeeId);
        Task<Request> UpdateRequestAsync(ReqItemRequest item);
        IQueryable<Request> GetRequestQueryWithFilters();
        
    }
}
