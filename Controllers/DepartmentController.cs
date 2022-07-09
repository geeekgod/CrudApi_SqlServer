using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using SqlServerRestApi.Models;
using Utils.Messages;

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
        public JsonResult Get()
        {
            string query = @"select * from dbo.Department";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("SqlDataBase");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            var jsonStr = JsonConvert.SerializeObject(table);
            var department = JsonConvert.DeserializeObject<IList<Department>>(jsonStr);
            return new JsonResult(department);

        }



        [HttpPost]
        public JsonResult Post(Department dep)
        {
            string query = @"insert into dbo.Department
                            values(@DeptName)
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("SqlDataBase");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DeptName", dep.DeptName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(new Created());

        }

        [HttpPut]
        public JsonResult Update(Department dep)
        {
            string query = @"update dbo.Department
                            set DeptName = @DeptName
                            where Id = @Id
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("SqlDataBase");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", dep.Id);
                    myCommand.Parameters.AddWithValue("@DeptName", dep.DeptName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(new Updated());

        }


        [HttpDelete("{Id}")]
        public JsonResult Delete(int Id)
        {
            string query = @"delete dbo.Department
                            where Id = @Id
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("SqlDataBase");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", Id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(new Deleted());

        }

    }
}
