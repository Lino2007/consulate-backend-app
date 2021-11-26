using NSI.BusinessLogic.Interfaces;
using NSI.DataContracts.Models;
using NSI.Repository.Interfaces;

namespace NSI.BusinessLogic.Implementations
{
    public class UsersManipulation : IUsersManipulation
    {
        private readonly IUsersRepository _usersRepository;

        public UsersManipulation(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public User GetByEmail(string email)
        {
            return _usersRepository.GetByEmail(email);
        }
    }
}
