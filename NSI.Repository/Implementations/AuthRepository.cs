using System.Linq;
using NSI.DataContracts.Models;
using NSI.Repository.Interfaces;

namespace NSI.Repository.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        
        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        
        public User GetByEmail(string email)
        {
            return _context.User.FirstOrDefault(u => u.Email.Equals(email));
        }
    }
}
