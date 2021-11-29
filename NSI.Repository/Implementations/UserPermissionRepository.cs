using Microsoft.EntityFrameworkCore;
using NSI.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSI.Common.Enumerations;

namespace NSI.Repository.Implementations
{
    public class UserPermissionRepository: IUserPermissionRepository
    {
        private readonly DataContext _context;

        public UserPermissionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, List<PermissionEnum>>> GetUsersAndPermissionsAsync()
        {
            var usersAndPermissions = await (
                         from user in _context.User
                         from userRole in _context.UserRole
                         from role in _context.Role
                         from rolePerm in _context.RolePermission
                         from perm in _context.Permission
                         where userRole.RoleId == role.Id && rolePerm.RoleId == role.Id
                            && rolePerm.PermissionId == perm.Id && user.Id == userRole.UserId
                         select new { user.Email, perm.Name}).ToListAsync();

            Dictionary<string, List<PermissionEnum>> dict = new Dictionary<string, List<PermissionEnum>>();
            foreach (var item in usersAndPermissions)
            {
                var key = item.Email;
                if (dict.ContainsKey(key)) {
                    var l = dict[key];
                    l.Add(PermissionEnumExtension.GetEnumByPermissionName(item.Name));
                    dict[key] = l;
                }
                else
                {
                    dict.Add(key, new List<PermissionEnum>() { PermissionEnumExtension.GetEnumByPermissionName(item.Name)});
                }
            }
            return dict;
        }

        public async Task<List<PermissionEnum>> GetUserPermissionsAsync(string email)
        {
            var result = await (from ur in _context.UserRole
                                from usr in _context.User
                                from role in _context.Role
                                from rolePerm in _context.RolePermission
                                from perm in _context.Permission
                                where usr.Email == email && ur.UserId == usr.Id && role.Id == ur.RoleId
                                && rolePerm.RoleId == role.Id && rolePerm.PermissionId == perm.Id
                                select new { perm.Name }).ToListAsync();
            var permissionList = new List<PermissionEnum>();
            System.Diagnostics.Debug.WriteLine("*********");
            System.Diagnostics.Debug.WriteLine(result.Count);
            foreach (var item in result)
            {
                var permEnum = PermissionEnumExtension.GetEnumByPermissionName(item.Name);
                permissionList.Add(permEnum);  
            }
            foreach (var item in permissionList)
            {
                System.Diagnostics.Debug.WriteLine(item);
            }
            return permissionList;
        }
    }
}
