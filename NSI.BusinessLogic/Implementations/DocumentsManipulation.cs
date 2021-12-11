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

        public Task<IList<Document>> GetDocumentsByUserIdAndType(Guid id, string type)
        {
            return _documentsRepository.GetDocumentsByUserIdAndType(id, type);
        }
    }
}
