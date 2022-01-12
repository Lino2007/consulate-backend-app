using System.Collections.Generic;
using NSI.BusinessLogic.Interfaces;
using NSI.Common.DataContracts.Enumerations;
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

        public ResponseStatus DeleteEmployee(string email)
        {
            var response = _employeeRepository.DeleteEmployee(email);
            return response;
        }

        public User UpdateEmployee(string mail, UpdateEmployeeRequest newEmployeeRequest)
        {
            return _employeeRepository.UpdateEmployee(mail, newEmployeeRequest);
        }

        public List<User> GetAllEmployeesAndUsers()
        {
            return _employeeRepository.GetEmployeesAndUsers();
        }
    }
}
