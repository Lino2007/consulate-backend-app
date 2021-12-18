using NSI.DataContracts.Models;

namespace NSI.Repository.Interfaces
{
    public interface IDocumentTypesRepository
    {
        public DocumentType GetByName(string name);
    }
}
