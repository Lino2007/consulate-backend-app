using System.Collections.Generic;
using NSI.BusinessLogic.Interfaces;
using NSI.DataContracts.Models;
using NSI.DataContracts.Request;
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

        public User SaveEmployee(NewEmployeeRequest newEmployeeRequest)
        {
            var gender = newEmployeeRequest.Gender == "Male"
                ? Common.Enumerations.Gender.Male
                : Common.Enumerations.Gender.Female;
            var newEmployee = new User(newEmployeeRequest.FirstName, newEmployeeRequest.LastName, gender,
                newEmployeeRequest.Email, newEmployeeRequest.Username, newEmployeeRequest.PlaceOfBirth,
                newEmployeeRequest.DateOfBirth, newEmployeeRequest.Country);

            return _employeeRepository.SaveEmployee(newEmployee);
        }
    }
}
