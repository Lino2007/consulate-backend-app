using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSI.DataContracts.Models;

namespace NSI.Repository.Interfaces
{
    public interface IDocumentsRepository
    {
        Task<IList<Document>> GetDocumentsByUserIdAndType(Guid id, string type);
    }
}
