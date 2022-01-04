using NSI.Common.Collation;
using NSI.Common.DataContracts.Enumerations;
using NSI.DataContracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Interfaces
{
    public interface IRolesManipulation
    {
        Task<IList<Role>> GetRolesAsync(Guid userId, Paging paging, IList<SortCriteria> sortCriteria, IList<FilterCriteria> filterCriteria);

        Role SaveRole(string name);
        
        ResponseStatus DeleteRole(string id);

        UserRole SaveRoleToUser(Guid roleId, Guid userId);

        ResponseStatus RemoveRoleFromUser(Guid roleId, Guid userId);
    }
}
