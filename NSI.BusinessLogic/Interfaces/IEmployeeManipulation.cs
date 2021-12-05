using System.Collections.Generic;
using NSI.Common.DataContracts.Enumerations;
using NSI.DataContracts.Models;
using NSI.DataContracts.Request;

namespace NSI.BusinessLogic.Interfaces
{
    public interface IEmployeeManipulation
    {
        List<User> GetAllEmployees();

        User SaveEmployee(NewEmployeeRequest newEmployeeRequest);
        
        ResponseStatus DeleteEmployee(string email);
    }
}
