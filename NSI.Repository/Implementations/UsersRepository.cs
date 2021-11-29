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

        public User SaveUser(User user)
        {
            User savedUser = _context.User.Add(user).Entity;

            Role userRole = _context.Role.FirstOrDefault(r => r.Name.Equals("User"));

            UserRole ur = new UserRole(savedUser.Id, userRole.Id);

            UserRole ur2 = _context.UserRole.Add(ur).Entity;

            _context.SaveChanges();

            return savedUser;
        }
    }
}
