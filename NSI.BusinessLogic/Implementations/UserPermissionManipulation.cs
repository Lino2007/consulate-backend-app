using System.Collections.Generic;
using NSI.Repository.Interfaces;
using NSI.BusinessLogic.Interfaces;
using System.Threading.Tasks;
using NSI.Common.Enumerations;

namespace NSI.BusinessLogic.Implementations
{
    public class UserPermissionManipulation : IUserPermissionManipulation
    {
        private readonly IUserPermissionRepository _userPermissionRepository;

        public UserPermissionManipulation(IUserPermissionRepository userPermissionRepository)
        {
            _userPermissionRepository = userPermissionRepository;
        }

        public async Task<List<PermissionEnum>> GetPermissionsForUserAsync(string email)
        {
            return await _userPermissionRepository.GetUserPermissionsAsync(email);
        }

        public async Task<Dictionary<string, List<PermissionEnum>>> GetUsersAndTheirPermissionsAsync()
        {
            return await _userPermissionRepository.GetUsersAndPermissionsAsync();
        }
    }
}
