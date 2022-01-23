using Microsoft.EntityFrameworkCore;
using NSI.DataContracts.Models;
using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.Repository.Implementations
{
    [ExcludeFromCodeCoverage]
    public class RolesRepository : IRolesRepository
    {
        private readonly DataContext _context;

        public RolesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IList<Role>> GetRolesAsync(Guid userId)
        {
            if (userId.Equals(Guid.Empty))
            {
                return await _context.Role.ToListAsync();
            }

            return await _context.UserRole
                .Where(ur => ur.UserId.Equals(userId))
                .Select(ur => ur.Role)
                .ToListAsync();
        }

        public Role GetRoleByUserId(Guid userId) 
        {

            UserRole userRole = _context.UserRole.FirstOrDefault(u => u.UserId.Equals(userId));
            return _context.Role.FirstOrDefault(r => r.Id.Equals(userRole.RoleId));
        }

        public Role SaveRole(Role role)
        {
            Role savedRole = _context.Role.Add(role).Entity;
            _context.SaveChanges();
            return savedRole;
        }
        
        public int DeleteRole(Guid id)
        {
            Role existingRole = _context.Role.First(r => r.Id.Equals(id));
            
            if (existingRole == null || _context.UserRole.Any(ur => ur.RoleId.Equals(id)))
            {
                return 0;
            }

            var rolePermissions = _context.RolePermission.Where(rp => rp.RoleId.Equals(id));

            foreach (var rolePermission in rolePermissions)
            {
                _context.Remove(rolePermission);
            }

            _context.Role.Remove(existingRole);
            return _context.SaveChanges() > 0 ? 1 : 0;
        }

        public UserRole SaveRoleToUser(UserRole userRole)
        {
            UserRole existingUserRole = FindUserRole(userRole);

            if (existingUserRole == null)
            {
                UserRole savedUserRole = _context.UserRole.Add(userRole).Entity;
                _context.SaveChanges();
                return savedUserRole;
            }
            else
            {
                return existingUserRole;
            }
        }

        public int RemoveRoleFromUser(UserRole userRole)
        {
            UserRole existingUserRole = FindUserRole(userRole);

            if (existingUserRole != null)
            {
                _context.UserRole.Remove(existingUserRole);
                return _context.SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        private UserRole FindUserRole(UserRole userRole)
        {
            return _context.UserRole
                .Where(ur => ur.RoleId.Equals(userRole.RoleId) && ur.UserId.Equals(userRole.UserId))
                .SingleOrDefault();
        }

        public async Task<IList<Role>> GetRolesByUserId(Guid userId)
        {
            return await _context.UserRole
                .Where(ur => ur.User.Id.Equals(userId))
                .Select(ur => ur.Role)
                .ToListAsync();
        }
    }
}
