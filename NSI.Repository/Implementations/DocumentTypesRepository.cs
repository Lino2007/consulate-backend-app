using System.Linq;
using NSI.DataContracts.Models;
using NSI.Repository.Interfaces;

namespace NSI.Repository.Implementations
{
    public class DocumentTypesRepository : IDocumentTypesRepository
    {
        private readonly DataContext _context;
        
        public DocumentTypesRepository(DataContext context)
        {
            _context = context;
        }
        
        public DocumentType GetByName(string name)
        {
            return _context.DocumentType.FirstOrDefault(doc => doc.Name.Equals(name));
        }
    }
}
