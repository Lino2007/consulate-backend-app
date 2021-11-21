using NSI.Common.Collation;
using NSI.Common.DataContracts.Enumerations;
using NSI.DataContracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Interfaces
{
    public interface IPermissionsManipulation
    {
        Task<IList<Permission>> GetPermissionsAsync(string role, Paging paging, IList<SortCriteria> sortCriteria, IList<FilterCriteria> filterCriteria);
        
        Permission SavePermission(string name);
        
        RolePermission SavePermissionToRole(Guid permissionId, Guid roleId);

        ResponseStatus RemovePermissionFromRole(Guid permissionId, Guid roleId);
    }
}
