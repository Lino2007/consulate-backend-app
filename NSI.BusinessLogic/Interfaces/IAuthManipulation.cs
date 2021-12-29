using NSI.DataContracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Interfaces
{
    public interface IAuthManipulation
    {
        User GetByEmail(string email);
        Role GetRoleFromEmail(string email);
        Task<IList<Role>> GetRoles(string email);
    }
}
