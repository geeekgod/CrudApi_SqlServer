using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using SqlServerRestApi.Models;
using Utils.Messages;
using Utils.DepartmentMethods;

namespace SqlServerRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public IActionResult Get()
        {
            DepartmentMethods Dept = new DepartmentMethods(_configuration);
            return new JsonResult(Dept.listAllData());
        }


        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            DepartmentMethods Dept = new DepartmentMethods(_configuration);
            var DeptById = Dept.listDataById(Id);
            if(DeptById != null)
            {
                return new JsonResult(DeptById);
            }
            else
            {
                return NotFound();
            }
        }



        [HttpPost]
        public IActionResult Post(Department dep)
        {
            DepartmentMethods Dept = new DepartmentMethods(_configuration);

            bool isCreated =  Dept.addDepartment(dep.DeptName);

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
        public IActionResult Update(Department dep)
        {

            DepartmentMethods Dept = new DepartmentMethods(_configuration);
            var DeptById = Dept.listDataById(dep.Id);
            if (DeptById != null)
            {
                bool isUpdated = Dept.updateDepartment(dep);
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
            DepartmentMethods Dept = new DepartmentMethods(_configuration);
            var DeptById = Dept.listDataById(Id);
            if (DeptById != null)
            {
                bool isDeleted = Dept.deleteDepartment(Id);
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
