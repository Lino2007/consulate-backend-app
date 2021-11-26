using System.Linq;
using NSI.DataContracts.Models;
using NSI.Repository.Interfaces;

namespace NSI.Repository.Implementations
{
    public class UsersRepository: IUsersRepository
    {
        private readonly DataContext _context;

        public UsersRepository(DataContext context)
        {
            _context = context;
        }

        public User GetByEmail(string email)
        {
            return _context.User.First(u => u.Email.Equals(email));
        }
    }
}
