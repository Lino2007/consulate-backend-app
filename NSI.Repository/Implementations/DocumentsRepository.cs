using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSI.DataContracts.Models;
using NSI.Repository.Interfaces;

namespace NSI.Repository.Implementations
{
    public class DocumentsRepository : IDocumentsRepository
    {
        private readonly DataContext _context;

        public DocumentsRepository(DataContext context)
        {
            _context = context;
        }
        
        public Document SaveDocument(Document document)
        {
            Document savedDocument = _context.Document.Add(document).Entity;
            _context.SaveChanges();
            return savedDocument;
        }
        
        public Document UpdateDocument(Document document)
        {
            Document oldDocument = _context.Document.First(d => d.Id.Equals(document.Id));
            oldDocument.Url = document.Url;
            oldDocument.Title = document.Title;
            _context.SaveChanges();
            return oldDocument;
        }

        public async Task<IList<Document>> GetDocumentsByUserIdAndType(Guid id, string type)
        {
            return await (
                from user in _context.User
                from request in _context.Request
                from document in _context.Document
                from documentType in _context.DocumentType
                where user.Id == request.UserId && 
                      document.TypeId == documentType.Id &&
                      document.RequestId == request.Id && 
                      user.Id.Equals(id) &&
                      (type == null || documentType.Name.Equals(type))
                select document
            ).ToListAsync();
        }
    }
}
