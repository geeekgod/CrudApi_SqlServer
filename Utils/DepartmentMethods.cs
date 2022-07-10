using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using SqlServerRestApi.Models;

namespace Utils.DepartmentMethods
{
    public class DepartmentMethods
    {

        private readonly IConfiguration _configuration;

        public DepartmentMethods(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // To get list of all departments
        public IList<Department> listAllData()
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
            var departments = JsonConvert.DeserializeObject<IList<Department>>(jsonStr);

            return departments;
        }


        // To get a single department using Id
        public Department listDataById(int Id)
        {
            string query = @"select * from dbo.Department where Id = @Id";

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
            var departments = JsonConvert.DeserializeObject<IList<Department>>(jsonStr);

            if (departments.Count > 0)
                {
                    return departments[0];
                }
                else
                {
                    return null;
                }

        }

        // To create a new Dept
        public bool addDepartment(string DeptName)
        {
            try
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
                        myCommand.Parameters.AddWithValue("@DeptName", DeptName);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }

                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

        }


        // To Update a Dept
        public bool updateDepartment(Department dep)
        {
            try
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

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

        }



        // To delete a Dept
        public bool deleteDepartment(int Id)
        {
            try
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
