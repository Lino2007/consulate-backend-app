using NSI.DataContracts.Models;

namespace NSI.BusinessLogic.Interfaces
{
    public interface IDocumentTypesManipulation
    {
        DocumentType GetByName(string name);
    }
}
