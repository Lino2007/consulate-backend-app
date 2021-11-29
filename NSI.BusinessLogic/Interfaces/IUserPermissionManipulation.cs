using System.Collections.Generic;
using System.Threading.Tasks;
using NSI.Common.Enumerations;

namespace NSI.BusinessLogic.Interfaces
{
    public interface IUserPermissionManipulation
    {
         Task<Dictionary<string, List<PermissionEnum>>> GetUsersAndTheirPermissionsAsync();
         Task<List<PermissionEnum>> GetPermissionsForUserAsync(string email);
    }
}
