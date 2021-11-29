using System.Collections.Generic;
using System.Threading.Tasks;
using NSI.Common.Enumerations;

namespace NSI.Repository.Interfaces
{
    public interface IUserPermissionRepository
    {
        Task<Dictionary<string, List<PermissionEnum>>> GetUsersAndPermissionsAsync();
        
        Task<List<PermissionEnum>> GetUserPermissionsAsync(string email);
    }
}
