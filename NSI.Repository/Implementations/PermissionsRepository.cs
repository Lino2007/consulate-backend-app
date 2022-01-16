using System;
using Microsoft.EntityFrameworkCore;
using NSI.DataContracts.Models;
using NSI.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.Repository.Implementations
{
    public class PermissionsRepository : IPermissionsRepository
    {
        private readonly DataContext _context;

        public PermissionsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IList<Permission>> GetPermissionsAsync(string role)
        {
            if (role == null)
            {
                return await _context.Permission.ToListAsync();
            }

            return await _context.RolePermission
                .Where(r => r.Role.Name.Equals(role))
                .Select(r => r.Permission)
                .ToListAsync();
        }

        public Permission SavePermission(Permission permission)
        {
            Permission savedPermission = _context.Permission.Add(permission).Entity;
            _context.SaveChanges();
            return savedPermission;
        }

        public RolePermission SavePermissionToRole(RolePermission rolePermission)
        {
            RolePermission existingRolePermission = FindRolePermission(rolePermission);

            if (existingRolePermission == null)
            {
                RolePermission savedRolePermission = _context.RolePermission.Add(rolePermission).Entity;
                _context.SaveChanges();
                return savedRolePermission;
            }
            else
            {
                return existingRolePermission;
            }
        }

        public int RemovePermissionFromRole(RolePermission rolePermission)
        {
            RolePermission existingRolePermission = FindRolePermission(rolePermission);

            if (existingRolePermission != null)
            {
                _context.RolePermission.Remove(existingRolePermission);
                return _context.SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public async Task<IList<Permission>> GetPermissionsByUserId(Guid id)
        {
            var roleIds = _context.UserRole
                .Where(r => r.UserId.Equals(id))
                .Select(r => r.Role.Id);

            return await _context.RolePermission
                .Where(r => roleIds.Contains(r.Role.Id))
                .Select(r => r.Permission)
                .Distinct()
                .ToListAsync();
        }

        private RolePermission FindRolePermission(RolePermission rolePermission)
        {
            return _context.RolePermission
                .SingleOrDefault(rp => rp.RoleId.Equals(rolePermission.RoleId) && rp.PermissionId.Equals(rolePermission.PermissionId));
        }
    }
}
