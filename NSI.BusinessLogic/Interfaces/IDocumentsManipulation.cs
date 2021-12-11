using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSI.DataContracts.Models;

namespace NSI.BusinessLogic.Interfaces
{
    public interface IDocumentsManipulation
    {
        Task<IList<Document>> GetDocumentsByUserIdAndType(Guid id, string type);
    }
}
