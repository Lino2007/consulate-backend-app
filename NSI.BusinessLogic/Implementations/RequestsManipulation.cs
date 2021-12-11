using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSI.BusinessLogic.Interfaces;
using NSI.Common.Enumerations;
using NSI.DataContracts.Models;
using NSI.DataContracts.Request;
using NSI.Repository.Interfaces;

namespace NSI.BusinessLogic.Implementations
{
    public class RequestsManipulation : IRequestsManipulation
    {
        private readonly IRequestsRepository _requestsRepository;

        public RequestsManipulation(IRequestsRepository requestsRepository)
        {
            _requestsRepository = requestsRepository;
        }

        public async Task<IList<Request>> GetRequestsAsync()
        {
            return await _requestsRepository.GetRequestsAsync();
        }

        public async Task<IList<Request>> GetEmployeeRequestsAsync(string employeeId)
        {
            return await _requestsRepository.GetEmployeeRequestsAsync(employeeId);
        }

        public Request SaveRequest(Guid userId, string reason, RequestType type)
        {
            return _requestsRepository.SaveRequest(new Request(userId, reason, type));
        }

        public async Task<Request> UpdateRequestAsync(ReqItemRequest item)
        {
            return await _requestsRepository.UpdateRequestAsync(item);
        }
    }
}
