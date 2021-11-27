using NSI.BusinessLogic.Interfaces;
using NSI.DataContracts.Models;
using NSI.Repository.Interfaces;

namespace NSI.BusinessLogic.Implementations
{
    public class AuthManipulation : IAuthManipulation
    {
        private readonly IAuthRepository _authRepository;

        public AuthManipulation(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public User GetByEmail(string email)
        {
            return _authRepository.GetByEmail(email);
        }
    }
}
