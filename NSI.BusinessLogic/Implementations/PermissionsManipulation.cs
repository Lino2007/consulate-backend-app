using NSI.BusinessLogic.Interfaces;
using NSI.Common.Collation;
using NSI.Common.DataContracts.Enumerations;
using NSI.Common.Extensions;
using NSI.DataContracts.Models;
using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Implementations
{
    public class PermissionsManipulation : IPermissionsManipulation
    {
        private readonly IPermissionsRepository _permissionsRepository;

        public PermissionsManipulation(IPermissionsRepository permissionsRepository)
        {
            _permissionsRepository = permissionsRepository;
        }

        public async Task<IList<Permission>> GetPermissionsAsync(string role, Paging paging, IList<SortCriteria> sortCriteria, IList<FilterCriteria> filterCriteria)
        {
            var results = await _permissionsRepository.GetPermissionsAsync(role);

            if (paging != null)
            {
                results = results.AsQueryable().DoPaging(paging).ToList();
            }

            return results;
        }

        public Permission SavePermission(string name)
        {
            return _permissionsRepository.SavePermission(new Permission(name));
        }

        public RolePermission SavePermissionToRole(Guid permissionId, Guid roleId)
        {
            return _permissionsRepository.SavePermissionToRole(new RolePermission(roleId, permissionId));
        }

        public ResponseStatus RemovePermissionFromRole(Guid permissionId, Guid roleId)
        {
            return (ResponseStatus) _permissionsRepository.RemovePermissionFromRole(new RolePermission(roleId, permissionId));
        }
    }
}
