using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NSI.BusinessLogic.Interfaces;
using NSI.Common.Enumerations;
using NSI.Common.Utilities;
using NSI.DataContracts.Models;
using NSI.DataContracts.Response;
using NSI.Repository.Interfaces;

namespace NSI.BusinessLogic.Implementations
{
    public class DocumentsManipulation : IDocumentsManipulation
    {
        private readonly IDocumentsRepository _documentsRepository;
        private readonly IBlockchainManipulation _blockchainManipulation;
        private readonly IFilesManipulation _filesManipulation;

        public DocumentsManipulation(IDocumentsRepository documentsRepository, IBlockchainManipulation blockchainManipulation, IFilesManipulation filesManipulation)
        {
            _documentsRepository = documentsRepository;
            _blockchainManipulation = blockchainManipulation;
            _filesManipulation = filesManipulation;
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

        public async Task<DocumentStatusResponse> GetDocumentWithStatus(Guid documentId)
        {
            Document document = _documentsRepository.GetDocumentById(documentId);
            
            if (document?.BlockchainId == null)
            {
                return new DocumentStatusResponse(null, DocumentStatus.Invalid);
            }

            if (DateTime.Compare(document.DateOfExpiration, DateTime.Now) < 0)
            {
                return new DocumentStatusResponse(document, DocumentStatus.Expired);
            }

            var stream = await _filesManipulation.DownloadFile(Path.GetFileName(document.Url));
            var documentHash = HashHelper.ComputeFileHash(stream);
            bool valid = await _blockchainManipulation.ValidateDocument(document.BlockchainId, documentHash);
            return new DocumentStatusResponse(document, valid ? DocumentStatus.Valid : DocumentStatus.Invalid);
        }
    }
}
