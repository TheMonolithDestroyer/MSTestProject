using Microsoft.EntityFrameworkCore;

namespace MSTestProject.Mocking
{
    public interface IEmployeeStorage
    {
        void DeleteEmployee(int employeeId);
    }

    public class EmployeeStorage : IEmployeeStorage
    {
        private readonly EmployeeContext _context;
        public EmployeeStorage(EmployeeContext context = null)
        {
            _context = new EmployeeContext();
        }

        public void DeleteEmployee(int employeeId)
        {
            var employee = _context.Employees.Find(employeeId);
            if (employee == null) return;

            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }
    }

    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public void SaveChanges()
        {

        }
    }

    public class Employee
    {
        public int Id { get; set; }
    }
}
