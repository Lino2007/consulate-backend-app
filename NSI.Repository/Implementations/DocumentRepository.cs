using Microsoft.EntityFrameworkCore;
using NSI.DataContracts.Models;
using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Implementations
{
    [ExcludeFromCodeCoverage]
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DataContext _context;

        public DocumentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IList<Document>> getDocumentsByRequests(List<Request> req)
        {
            if (req == null || req.Count == 0)
            {
                return null;
            }
            var idList = req.Select(x => x.Id).ToList();
            var result = await (from document in _context.Document
                                 from dt in _context.DocumentType
                                 where idList.Contains(document.RequestId) && document.TypeId == dt.Id
                                 select new { document, dt }).ToListAsync();

            return result.Select((item) => 
                                    {   // document klasa ima DocumentType property (Type) koji nakon prvog querija bude null
                                        item.document.Type = item.dt;
                                        return item.document; }).ToList(); 
        }
    }
}
