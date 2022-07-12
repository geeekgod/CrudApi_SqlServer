using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using SqlServerRestApi.Models;
using Utils.Messages;
using Utils.EmployeeMethods;

namespace SqlServerRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _configuration;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public IActionResult Get()
        {
            EmployeeMethods Emp = new EmployeeMethods(_configuration);
            return new JsonResult(Emp.listAllData());
        }


        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            EmployeeMethods Emp = new EmployeeMethods(_configuration);
            var EmpById = Emp.listDataById(Id);
            if (EmpById != null)
            {
                return new JsonResult(EmpById);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Post(Employee Emp)
        {
            EmployeeMethods EmpMeth = new EmployeeMethods(_configuration);

            bool isCreated = EmpMeth.addEmployee(Emp);

            if (isCreated)
            {
                return new JsonResult(new Created());
            }
            else
            {
                return StatusCode(500);
            }
        }


        [HttpPut]
        public IActionResult Update(Employee Emp)
        {

            EmployeeMethods EmpMeth = new EmployeeMethods(_configuration);
            var DeptById = EmpMeth.listDataById(Emp.Id);
            if (DeptById != null)
            {
                bool isUpdated = EmpMeth.updateEmployee(Emp);
                if (isUpdated)
                {
                    return new JsonResult(new Updated());
                }
                else
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return NotFound();
            }

        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            EmployeeMethods EmpMeth = new EmployeeMethods(_configuration);
            var EmpById = EmpMeth.listDataById(Id);
            if (EmpById != null)
            {
                bool isDeleted = EmpMeth.deleteEmployee(Id);
                if (isDeleted)
                {
                    return new JsonResult(new Deleted());
                }
                else
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return NotFound();
            }

        }

    }
}
