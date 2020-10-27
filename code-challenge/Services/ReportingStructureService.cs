using challenge.Models;
using challenge.Repositories;

namespace challenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public ReportingStructureService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Get the total number of employees reporting to a provided employee.
        /// </summary>
        /// <param name="employee">Employee to calculate the number of reports</param>
        /// <returns>The number of reports for the provided employee</returns>
        private int GetNoReports(Employee employee)
        {
            if (employee.DirectReports == null) return 0;
            var count = 0;
            foreach (var item in employee.DirectReports)
            {
                count += (1 + GetNoReports(item));
            }
            return count;
            

        }

        public ReportingStructure GetByEmployeeId(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var employee = _employeeRepository.GetById(id);
                ReportingStructure structure = new ReportingStructure {
                    Employee = employee,
                    NumberOfReports = GetNoReports(employee)       
                } ;

                return structure;
            }

            return null;

    }
    }
}
