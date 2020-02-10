using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Models.Contract;
using Demo.Models.Poco;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Demo.WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeService _service;
        public EmployeeController(
            ILogger<EmployeeController> logger, IEmployeeService service) {
            this._logger = logger;
            this._service = service;
        }

        // GET: api/Employee
        /// <summary>
        /// 查詢所有員工資訊
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await this._service.GetAll());

        // GET: api/Employee/5
        /// <summary>
        /// 查詢一個員工資訊
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet("{guid}", Name = "Get")]
        public async Task<IActionResult> Get(string guid) {
            var res = await this._service.GetByGuid(guid);
            if (res == null) {
                return new ObjectResult(
                    new ProblemDetails { Detail = $"Employee not exist.({guid})"}) {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 200,
                };
            }
            return Ok(res);
        }

        /// <summary>
        /// 刪除所有員工資訊
        /// </summary>
        /// <returns></returns>
        [HttpDelete()]
        public async Task<IActionResult> Delete() {
            var success = await this._service.DeleteEmployees();
            if (!success) {
                return new ObjectResult(
                    new ProblemDetails { Detail = "All employe delete failed or internal error."}) {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 200,
                };
            }
            return Ok(success);
        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// 刪除一個員工資訊
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpDelete("{guid}")]
        public async Task<IActionResult> Delete(string guid) {
            var success = await this._service.DeleteEmployee(guid);
            if (!success) {
                return new ObjectResult(
                    new ProblemDetails { Detail = "Employe delete failed or internal error." }) {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 200,
                };
            }
            return Ok(success);
        }


        // POST: api/Employee
        /// <summary>
        /// 新增員工資訊
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IEnumerable<Employee> e)
        {
            var success = await this._service.AddNewEmployee(e);
            if (!success) {
                return new ObjectResult(
                    new ProblemDetails { Detail = "Employee delete failed or internal error." }) {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 200,
                };
            }
            return Ok(success);
        }

        // PUT: api/Employee/5
        /// <summary>
        /// 更新員工資訊
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] Employee e)
        {
            var success = await this._service.UpdataEmployeeInfo(e);
            if (!success) {
                return new ObjectResult(
                    new ProblemDetails { Detail = "Employee update failed or not exist." }) {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 200,
                };
            }
            return Ok(success);
        }
    }
}
