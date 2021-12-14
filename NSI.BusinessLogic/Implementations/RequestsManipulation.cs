using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSI.BusinessLogic.Interfaces;
using NSI.Common.Collation;
using NSI.Common.Enumerations;
using NSI.Common.Extensions;
using NSI.DataContracts.Models;
using NSI.DataContracts.Request;
using NSI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using NSI.DataContracts.Dto;
using Microsoft.Extensions.Caching.Memory;
using NSI.Cache.Interfaces;

namespace NSI.BusinessLogic.Implementations
{
    public class RequestsManipulation : IRequestsManipulation
    {
        private readonly IRequestsRepository _requestsRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly ICacheProvider _cacheProvider;

        public RequestsManipulation(IRequestsRepository requestsRepository, IAttachmentRepository attachmentRepository, 
            IDocumentRepository documentRepository, ICacheProvider cacheProvider)
        {
            _requestsRepository = requestsRepository;
            _attachmentRepository = attachmentRepository;
            _documentRepository = documentRepository;
            _cacheProvider = cacheProvider;
        }

        public async Task<IList<Request>> GetRequestsAsync()
        {
            return await _requestsRepository.GetRequestsAsync();
        }

        public async Task<IList<RequestItemDto>> GetEmployeeRequestsAsync(string employeeId, Paging paging)
        {
            var query = _requestsRepository.GetEmployeeRequestsAsync(employeeId);
            var RequestList = await (await query.DoPagingAsync(paging)).ToListAsync();
            return await CreateRequestItemDto(RequestList);
        }

        public Request SaveRequest(Guid userId, string reason, RequestType type)
        {
            return _requestsRepository.SaveRequest(new Request(userId, reason, type));
        }

        public async Task<Request> UpdateRequestAsync(ReqItemRequest item)
        {
            return await _requestsRepository.UpdateRequestAsync(item);
        }

        public async Task<IList<RequestItemDto>> GetRequestPage(Paging paging)
        {
            var query = _requestsRepository.GetRequestQueryWithFilters();
            var RequestList = await (await query.DoPagingAsync(paging)).ToListAsync();
            return await CreateRequestItemDto(RequestList);
        }

        public async Task<IList<RequestItemDto>> CreateRequestItemDto(List<Request> RequestList)
        {
            var idToMailMap = _cacheProvider.Get<Dictionary<string, string>>("idToMail");
            var AttachmentList = await (_attachmentRepository.getAttachmentsByRequests(RequestList));
            var DocumentList = await (_documentRepository.getDocumentsByRequests(RequestList));
           
            List<RequestItemDto> requestItemDtos = RequestList.Select(request =>
            { 
                return new RequestItemDto(
                   new SimplifiedRequestDto(request, idToMailMap[request.UserId.ToString()], idToMailMap[request.EmployeeId.ToString()]),
                   DocumentList.Where(doc => doc.RequestId == request.Id).Select(doc => new DocumentDto(doc)).ToList(),
                   AttachmentList.Where(att => att.RequestId == request.Id).Select(att => new AttachmentDto(att)).ToList());
            }).ToList();

            return requestItemDtos;
        }

    }
}
