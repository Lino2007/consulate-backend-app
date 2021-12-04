using System.Collections.Generic;
using System.Linq;
using NSI.DataContracts.Models;
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
    }
}
