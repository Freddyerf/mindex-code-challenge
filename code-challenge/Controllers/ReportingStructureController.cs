using challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace challenge.Controllers
{
    [Route("api/reporting-structure")]
    public class ReportingStructureController : Controller
    {
        private readonly ILogger _logger;
        private readonly IReportingStructureService _reportingStructureService;

        public ReportingStructureController(ILogger<ReportingStructureController> logger, IReportingStructureService reportingStructureService)
        {
            _reportingStructureService = reportingStructureService;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "getEmployeeReportStructById")]
        public IActionResult GetEmployeeReportStructById(string id)
        {
            _logger.LogDebug($"Received ReportingStructure GET request for '{id}'");

            var strc = _reportingStructureService.GetByEmployeeId(id);

            if (strc == null)
                return NotFound();

            return Ok(strc);
        }

    }
}
