namespace SqlServerRestApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmpName { get; set; }

        public int DeptId { get; set; }

        public string DateOfJoining { get; set; }
    }
}