using System.Collections.Generic;
using NSI.Common.DataContracts.Enumerations;
using NSI.DataContracts.Models;

namespace NSI.Repository.Interfaces
{
    public interface IEmployeeRepository
    {
        List<User> GetAllEmployees();

        User SaveEmployee(User employee);
        
        ResponseStatus DeleteEmployee(string email);
    }
}
