using NSI.DataContracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces
{
    public interface IPermissionsRepository
    {
        Task<IList<Permission>> GetPermissionsAsync(string role);

        Permission SavePermission(Permission permission);

        RolePermission SavePermissionToRole(RolePermission rolePermission);

        int RemovePermissionFromRole(RolePermission rolePermission);
    }
}
