using NSI.DataContracts.Models;

namespace NSI.Repository.Interfaces
{
    public interface IUsersRepository
    {
        User GetByEmail(string email);
    }
}
