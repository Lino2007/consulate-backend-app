using NSI.DataContracts.Models;

namespace NSI.BusinessLogic.Interfaces
{
    public interface IAuthManipulation
    {
        User GetByEmail(string email);
        Role GetRoleFromEmail(string email);
    }
}
