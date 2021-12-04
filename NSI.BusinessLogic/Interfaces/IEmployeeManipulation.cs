using System.Collections.Generic;
using NSI.DataContracts.Models;

namespace NSI.BusinessLogic.Interfaces
{
    public interface IEmployeeManipulation
    {
        List<User> GetAllEmployees();
    }
}
