using NSI.DataContracts.Models;

namespace NSI.Repository.Interfaces
{
    public interface IAuthRepository
    {
        User GetByEmail(string email);
    }
}
