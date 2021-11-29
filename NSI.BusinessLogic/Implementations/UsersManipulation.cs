using NSI.BusinessLogic.Interfaces;
using NSI.DataContracts.Models;
using NSI.DataContracts.Request;
using NSI.Repository.Interfaces;
using NSI.Common.DataContracts.Enumerations;

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

        public User saveUser(NewUserRequest userRequest)
        {
            Common.Enumerations.Gender g = Common.Enumerations.Gender.Male;

            if (userRequest.Gender == 2) {
                g = Common.Enumerations.Gender.Female;
            }

            User newUser = new User(userRequest.FirstName, userRequest.LastName, g, userRequest.Email, userRequest.Username, userRequest.PlaceOfBirth, userRequest.DateOfBirth, userRequest.Country);

            return _usersRepository.SaveUser(newUser);
        }
    }
}
