using NSI.DataContracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces
{
    public interface IDocumentRepository
    {
        public Task<IList<Document>> getDocumentsByRequests(List<Request> req);
    }
}
