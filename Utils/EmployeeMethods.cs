using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using SqlServerRestApi.Models;

namespace Utils.EmployeeMethods
{
    public class EmployeeMethods
    {

        private readonly IConfiguration _configuration;

        public EmployeeMethods(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // To get list of all employees
        public IList<Employee> listAllData()
        {
            string query = @"select * from dbo.Employee";

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
            var employees = JsonConvert.DeserializeObject<IList<Employee>>(jsonStr);

            return employees;
        }


        // To get a single employees using Id
        public Employee listDataById(int Id)
        {
            string query = @"select * from dbo.Employee where Id = @Id";

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
            var jsonStr = JsonConvert.SerializeObject(table);
            var employees = JsonConvert.DeserializeObject<IList<Employee>>(jsonStr);

            if (employees.Count > 0)
            {
                return employees[0];
            }
            else
            {
                return null;
            }

        }


        // To create a new Employee
        public bool addEmployee(Employee Emp)
        {
            try
            {
                string query = @"insert into dbo.Employee
                            values(@EmpName, @DeptId, @DateOfJoining)
                            ";

                DataTable table = new DataTable();

                string sqlDataSource = _configuration.GetConnectionString("SqlDataBase");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@EmpName", Emp.EmpName);
                        myCommand.Parameters.AddWithValue("@DeptId", Emp.DeptId);
                        myCommand.Parameters.AddWithValue("@DateOfJoining", Emp.DateOfJoining);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

        }


        // To Update a Emp
        public bool updateEmployee(Employee Emp)
        {
            try
            {
                string query = @"update dbo.Employee
                            set EmpName = @EmpName, DeptId = @DeptId, DateOfJoining = @DateOfJoining
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
                        myCommand.Parameters.AddWithValue("@Id", Emp.Id);
                        myCommand.Parameters.AddWithValue("@EmpName", Emp.EmpName);
                        myCommand.Parameters.AddWithValue("@DeptId", Emp.DeptId);
                        myCommand.Parameters.AddWithValue("@DateOfJoining", Emp.DateOfJoining);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

        }


        // To delete a Emp
        public bool deleteEmployee(int Id)
        {
            try
            {
                string query = @"delete dbo.Employee
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

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

        }

    }
}
