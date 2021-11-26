using System;
using NSI.BusinessLogic.Interfaces;
using NSI.Common.Enumerations;
using NSI.DataContracts.Models;
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

        public Request SaveRequest(Guid userId, string reason, RequestType type)
        {
            return _requestsRepository.SaveRequest(new Request(userId, reason, type));
        }
    }
}
