using System.Collections.Generic;
using System.Linq;
using NSI.Common.DataContracts.Enumerations;
using NSI.DataContracts.Models;
using NSI.DataContracts.Request;
using NSI.Repository.Interfaces;

namespace NSI.Repository.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;

        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }

        public List<User> GetAllEmployees()
        {
            var employees = new List<User>();
            foreach (var user in _context.User.ToList())
            {
                var userRole = _context.UserRole.FirstOrDefault(u => u.UserId.Equals(user.Id));
                var role = _context.Role.FirstOrDefault(r => r.Id.Equals(userRole.RoleId));
                if (role is {Name: "Employee"})
                {
                    employees.Add(user);
                }
            }

            return employees;
        }

        public User SaveEmployee(User newEmployee)
        {
            var savedEmployee = _context.User.Add(newEmployee).Entity;
            var roleEmployee = _context.Role.FirstOrDefault(r => r.Name.Equals("Employee"));
            if (roleEmployee != null)
            {
                var userRole = new UserRole(savedEmployee.Id, roleEmployee.Id);
                var savedEmployeeRole = _context.UserRole.Add(userRole).Entity;
            }

            _context.SaveChanges();
            return savedEmployee;
        }

        public ResponseStatus DeleteEmployee(string email)
        {
            var employee = _context.User.FirstOrDefault(u => u.Email.Equals(email));
            if (employee == null)
            {
                return ResponseStatus.Failed;
            }

            var userRole = _context.UserRole.FirstOrDefault(ur => ur.UserId.Equals(employee.Id));
            _context.User.Remove(employee);
            if (userRole != null)
            {
                _context.UserRole.Remove(userRole);
            }

            _context.SaveChanges();
            return ResponseStatus.Succeeded;
        }

        public User UpdateEmployee(string mail, UpdateEmployeeRequest employeeRequest)
        {
            var employee = _context.User.FirstOrDefault(emp => emp.Email == mail);
            var employeeWithSameUsername =
                _context.User.FirstOrDefault(emp => emp.Username == employeeRequest.Username);
            var usernameAlreadyExists = employeeWithSameUsername != null;
            if (employee != null && !usernameAlreadyExists)
            {
                employee.FirstName = employeeRequest.FirstName;
                employee.LastName = employeeRequest.LastName;
                employee.Username = employeeRequest.Username;
                employee.DateOfBirth = employeeRequest.DateOfBirth;
                employee.PlaceOfBirth = employeeRequest.PlaceOfBirth;
                employee.Country = employeeRequest.Country;
                employee.Gender = employeeRequest.Gender == "Male"
                    ? Common.Enumerations.Gender.Male
                    : Common.Enumerations.Gender.Female;
                var updatedEmployee = _context.User.Update(employee).Entity;
                _context.SaveChanges();
                return updatedEmployee;
            }

            return null;
        }
    }
}
