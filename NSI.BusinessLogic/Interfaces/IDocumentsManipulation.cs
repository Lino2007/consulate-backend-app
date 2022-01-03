using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSI.DataContracts.Models;
using NSI.DataContracts.Response;

namespace NSI.BusinessLogic.Interfaces
{
    public interface IDocumentsManipulation
    {
        Document SaveDocument(Guid requestId, Guid typeId, DateTime dateOfExpiration, string url, string title);
        Document UpdateDocument(Document document);
        Task<IList<Document>> GetDocumentsByUserIdAndType(Guid id, string type);
        Task<DocumentStatusResponse> GetDocumentWithStatus(Guid documentId);
    }
}
