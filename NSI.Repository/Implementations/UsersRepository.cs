using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSI.Common.DataContracts.Enumerations;
using NSI.DataContracts.Models;
using NSI.Repository.Interfaces;

namespace NSI.Repository.Implementations
{
    [ExcludeFromCodeCoverage]
    public class UsersRepository: IUsersRepository
    {
        private readonly DataContext _context;

        public UsersRepository(DataContext context)
        {
            _context = context;
        }
        
        public User GetById(Guid id)
        {
            return _context.User.First(u => u.Id.Equals(id) && u.Active);
        }

        public User GetByEmail(string email)
        {
            return _context.User.First(u => u.Email.Equals(email) && u.Active);
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

        public ResponseStatus RemoveUser(string email)
        {
            User user = _context.User.FirstOrDefault(u => u.Email.Equals(email));

            if (user == null)
            {
                return ResponseStatus.Failed;
            }
            
            user.Active = false;
            _context.SaveChanges();
            return ResponseStatus.Succeeded;
        }

        public async Task<IList<User>> GetUsersAsync()
        {
            return await _context.UserRole
                .Where(ur => ur.Role.Name.Equals("User") && ur.User.Active)
                .Select(ur => ur.User)
                .ToListAsync();
        }

        public async Task<IList<User>> GetAllPersonAsync()
        {
            return await _context.User.Where(u => u.Active).ToListAsync();
        }
    }
}
