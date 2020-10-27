using challenge.Models;
using challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace challenge.Controllers
{
    [Route("api/compensation")]
    public class CompensationController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;
        private readonly IEmployeeService _employeeService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService, IEmployeeService employeeService)
        {
            _compensationService = compensationService;
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "getCompensationById")]
        public IActionResult getCompensationById(string id)
        {
            _logger.LogDebug($"Received Compensation GET request for '{id}'");

            var comp = _compensationService.getByEmployeeId(id);

            if (comp == null)
                return NotFound();

            return Ok(comp);
        }

        [HttpPost]
        public IActionResult createCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received Compensation POST request for '{compensation.Employee.EmployeeId}'");

            var existingEmployee = _employeeService.GetById(compensation.Employee.EmployeeId);
            if (existingEmployee == null)
                return NotFound();
            compensation.Employee = existingEmployee;

            _compensationService.Create(compensation);

            return CreatedAtRoute("getCompensationById", new { id = compensation.Employee.EmployeeId }, compensation);
        }

    }
}
