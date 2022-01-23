using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NSI.BusinessLogic.Interfaces;
using NSI.Common.Collation;
using NSI.Common.Enumerations;
using NSI.Common.Extensions;
using NSI.DataContracts.Models;
using NSI.DataContracts.Request;
using NSI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using NSI.DataContracts.Dto;
using NSI.Cache.Interfaces;

namespace NSI.BusinessLogic.Implementations
{
    public class RequestsManipulation : IRequestsManipulation
    {
        private readonly IRequestsRepository _requestsRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentTypesRepository _documentTypesRepository;
        private readonly IFilesManipulation _filesManipulation;
        private readonly ICacheProvider _cacheProvider;

        public RequestsManipulation(IRequestsRepository requestsRepository, IAttachmentRepository attachmentRepository, 
            IDocumentRepository documentRepository, IDocumentTypesRepository documentTypesRepository, ICacheProvider cacheProvider, IFilesManipulation filesManipulation)
        {
            _requestsRepository = requestsRepository;
            _attachmentRepository = attachmentRepository;
            _documentRepository = documentRepository;
            _documentTypesRepository = documentTypesRepository;
            _cacheProvider = cacheProvider;
            _filesManipulation = filesManipulation;
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

        [ExcludeFromCodeCoverage]
        public async Task<Request> SaveRequest(Guid userId, string requestReason, RequestType requestType,  IEnumerable<IFormFile> attachments, string[] attachmentTypes)
        {
            Request savedRequest = _requestsRepository.SaveRequest(new Request(userId, requestReason, requestType));
            int i = 0;
            foreach (var file in attachments)
            {
                Attachment attachment = _attachmentRepository.SaveAttachment(new Attachment(savedRequest.Id, _documentTypesRepository.GetByName(attachmentTypes[i]).Id));
                string url = await _filesManipulation.UploadFile(file, attachment.Id.ToString());
                attachment.Url = url;
                _attachmentRepository.UpdateAttachment(attachment);
                i++;
            }
            return savedRequest;
        }

        public async Task<Request> UpdateRequestAsync(ReqItemRequest item, User user)
        {
            return await _requestsRepository.UpdateRequestAsync(item, user);
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
            var AttachmentList = await (_attachmentRepository.GetAttachmentsByRequests(RequestList));
            var DocumentList = await (_documentRepository.getDocumentsByRequests(RequestList));
           
            List<RequestItemDto> requestItemDtos = RequestList.Select(request =>
            { 
                return new RequestItemDto(
                   new SimplifiedRequestDto(request, idToMailMap[request.UserId.ToString()], request.EmployeeId == null ? null : idToMailMap[request.EmployeeId.ToString()]),
                   DocumentList.Where(doc => doc.RequestId == request.Id).Select(doc => new DocumentDto(doc)).ToList(),
                   AttachmentList.Where(att => att.RequestId == request.Id).Select(att => new AttachmentDto(att)).ToList());
            }).ToList();

            return requestItemDtos;
        }

    }
}
