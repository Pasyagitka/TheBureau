using System.Collections.Generic;
using System.Data.Entity;

namespace TheBureau.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private Model _context = new Model();
        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees;
        }
        public Employee Get(int id)
        {
            return _context.Employees.Find(id);
        }
        public void Add(Employee employee)
        {
            _context.Employees.Add(employee);
        }
        public void Update(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
        }
    }
}