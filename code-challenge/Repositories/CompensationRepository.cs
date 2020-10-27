using challenge.Data;
using challenge.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly IEmployeeRepository _employeeRepository;

        public CompensationRepository(EmployeeContext employeeContext, IEmployeeRepository employeeRepository)
        {
            _employeeContext = employeeContext;
            _employeeRepository = employeeRepository;
        }

        public Compensation Add(Compensation compensation)
        {
            var existingEmployee = _employeeRepository.GetById(compensation.Employee.EmployeeId);
            if (existingEmployee == null)
                return null;
            compensation.Employee = existingEmployee;

            compensation.Id = Guid.NewGuid();
            _employeeContext.Compensations
                .Add(compensation);
            _employeeContext.Entry(compensation).CurrentValues.SetValues(compensation.Employee);
            return compensation;
        }

        public Compensation GetByEmployeeId(string id)
        {
            return _employeeContext.Compensations
                .Include(c => c.Employee)
                .SingleOrDefault(e => e.Employee.EmployeeId == id);
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }
    }
}
