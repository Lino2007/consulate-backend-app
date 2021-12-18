using NSI.DataContracts.Models;

namespace NSI.Repository.Interfaces
{
    public interface IDocumentTypesRepository
    {
        DocumentType GetByName(string name);
    }
}
