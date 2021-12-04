using System.Collections.Generic;
using NSI.BusinessLogic.Interfaces;
using NSI.DataContracts.Models;
using NSI.Repository.Interfaces;

namespace NSI.BusinessLogic.Implementations
{
    public class EmployeeManipulation : IEmployeeManipulation
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeManipulation(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public List<User> GetAllEmployees()
        {
            return _employeeRepository.GetAllEmployees();
        }
    }
}
