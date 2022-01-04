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
    public class RolesManipulation : IRolesManipulation
    {
        private readonly IRolesRepository _rolesRepository;

        public RolesManipulation(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public async Task<IList<Role>> GetRolesAsync(Guid userId, Paging paging, IList<SortCriteria> sortCriteria, IList<FilterCriteria> filterCriteria)
        {
            var results = await _rolesRepository.GetRolesAsync(userId);

            if (paging != null)
            {
                results = results.AsQueryable().DoPaging(paging).ToList();
            }

            return results;
        }

        public Role SaveRole(string name)
        {
            return _rolesRepository.SaveRole(new Role(name));
        }
        
        public ResponseStatus DeleteRole(string id)
        {
            return (ResponseStatus) _rolesRepository.DeleteRole(Guid.Parse(id));
        }

        public UserRole SaveRoleToUser(Guid roleId, Guid userId)
        {
            return _rolesRepository.SaveRoleToUser(new UserRole(userId, roleId));
        }

        public ResponseStatus RemoveRoleFromUser(Guid roleId, Guid userId)
        {
            return (ResponseStatus) _rolesRepository.RemoveRoleFromUser(new UserRole(userId, roleId));
        }
    }
}
