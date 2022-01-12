using System.Collections.Generic;
using System.Threading.Tasks;
using NSI.Common.DataContracts.Enumerations;
using NSI.DataContracts.Models;
using NSI.DataContracts.Request;

namespace NSI.Repository.Interfaces
{
    public interface IEmployeeRepository
    {
        List<User> GetAllEmployees();

        User SaveEmployee(User employee);
        
        ResponseStatus DeleteEmployee(string email);
        
        User UpdateEmployee(string mail, UpdateEmployeeRequest employeeRequest);

        List<User> GetEmployeesAndUsers();
    }
}
