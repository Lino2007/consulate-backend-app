using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSI.BusinessLogic.Interfaces;
using NSI.DataContracts.Models;
using NSI.Repository.Interfaces;

namespace NSI.BusinessLogic.Implementations
{
    public class DocumentsManipulation: IDocumentsManipulation
    {
        private readonly IDocumentsRepository _documentsRepository;

        public DocumentsManipulation(IDocumentsRepository documentsRepository)
        {
            _documentsRepository = documentsRepository;
        }
        
        public Document SaveDocument(Guid requestId, Guid typeId, DateTime dateOfExpiration, string url, string title)
        {
            return _documentsRepository.SaveDocument(new Document(requestId, typeId, dateOfExpiration, url, title));
        }

        public Document UpdateDocument(Document document)
        {
            return _documentsRepository.UpdateDocument(document);
        }

        public Task<IList<Document>> GetDocumentsByUserIdAndType(Guid id, string type)
        {
            return _documentsRepository.GetDocumentsByUserIdAndType(id, type);
        }
    }
}
