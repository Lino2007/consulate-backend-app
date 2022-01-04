using NSI.DataContracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces
{
    public interface IRolesRepository
    {
        Task<IList<Role>> GetRolesAsync(Guid userId);

        Role GetRoleByUserId(Guid userId);

        Role SaveRole(Role role);
        
        int DeleteRole(Guid id);

        UserRole SaveRoleToUser(UserRole userRole);

        int RemoveRoleFromUser(UserRole userRole);

        Task<IList<Role>> GetRolesByUserId(Guid userId);
    }
}
