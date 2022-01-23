using NSI.BusinessLogic.Interfaces;
using NSI.DataContracts.Models;
using NSI.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Implementations
{
    public class AuthManipulation : IAuthManipulation
    {
        private readonly IAuthRepository _authRepository;

        private readonly IRolesRepository _rolesRepository;

        public AuthManipulation(IAuthRepository authRepository, IRolesRepository rolesRepository)
        {
            _authRepository = authRepository;
            _rolesRepository = rolesRepository;
        }

        public User GetByEmail(string email)
        {
            return _authRepository.GetByEmail(email);
        }

        public Role GetRoleFromEmail(string email) 
        {
            User user = _authRepository.GetByEmail(email);

            if(user == null)
            {
                return null;
            }

            var result = _rolesRepository.GetRoleByUserId(user.Id);

            return result;

        }

        public async Task<IList<Role>> GetRoles(string email)
        {
            User user = _authRepository.GetByEmail(email);

            if (user == null)
            {
                return null;
            }

            var result = await _rolesRepository.GetRolesByUserId(user.Id);

            return result;

        }
    }
}
