using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSI.Common.DataContracts.Enumerations;
using NSI.DataContracts.Models;
using NSI.DataContracts.Request;
using NSI.Repository.Interfaces;

namespace NSI.Repository.Implementations
{
    [ExcludeFromCodeCoverage]
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
                if (user.Active)
                {
                    var userRoles = _context.UserRole
                        .Where(r => r.UserId.Equals(user.Id))
                        .ToList();

                    foreach (var userRole in userRoles)
                    {
                        var role = _context.Role.FirstOrDefault(r => r.Id.Equals(userRole.RoleId));
                        if (role is {Name: "Employee"})
                        {
                            employees.Add(user);
                        }
                    }
                }
            }

            return employees;
        }

        public User SaveEmployee(User employee)
        {
            var savedEmployee = _context.User.Add(employee).Entity;
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

            employee.Active = false;

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

        public List<User> GetEmployeesAndUsers()
        {
            var users = _context.User
                .Where(u => u.Active)
                .ToList();
            return users;
        }
    }
}
