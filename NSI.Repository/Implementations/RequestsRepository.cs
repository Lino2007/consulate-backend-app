using NSI.DataContracts.Models;
using NSI.Repository.Interfaces;

namespace NSI.Repository.Implementations
{
    public class RequestsRepository: IRequestsRepository
    {
        private readonly DataContext _context;

        public RequestsRepository(DataContext context)
        {
            _context = context;
        }
        
        public Request SaveRequest(Request request)
        {
            var savedRequest = _context.Request.Add(request).Entity;
            _context.SaveChanges();
            return savedRequest;
        }
    }
}
