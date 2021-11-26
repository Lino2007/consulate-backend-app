using NSI.DataContracts.Models;

namespace NSI.BusinessLogic.Interfaces
{
    public interface IUsersManipulation
    {
        User GetByEmail(string email);
    }
}
