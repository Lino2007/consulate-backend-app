using NSI.BusinessLogic.Interfaces;
using NSI.DataContracts.Models;
using NSI.Repository.Interfaces;

namespace NSI.BusinessLogic.Implementations
{
    public class DocumentTypesManipulation : IDocumentTypesManipulation
    {
        private readonly IDocumentTypesRepository _documentTypesRepository;

        public DocumentTypesManipulation(IDocumentTypesRepository documentTypesRepository)
        {
            _documentTypesRepository = documentTypesRepository;
        }

        public DocumentType GetByName(string name)
        {
            return _documentTypesRepository.GetByName(name);
        }
    }
}
