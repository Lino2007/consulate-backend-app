using NSI.DataContracts.Models;
using NSI.DataContracts.Request;

namespace NSI.BusinessLogic.Interfaces
{
    public interface IUsersManipulation
    {
        User GetByEmail(string email);

        User saveUser(NewUserRequest userRequest);
    }
}
