using System.Collections.Generic;
using NSI.DataContracts.Models;
using NSI.DataContracts.Request;

namespace NSI.BusinessLogic.Interfaces
{
    public interface IEmployeeManipulation
    {
        List<User> GetAllEmployees();

        User SaveEmployee(NewEmployeeRequest newEmployeeRequest);
    }
}
