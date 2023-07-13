namespace FARMCENTRALPROTOTYPE.Models
{
    public class Employee
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeePassword { get; set; }

        public Employee()
        {
            
        }

        public Employee(string employeeID, string employeeName, string employeeSurname, string employeeEmail, string employeePassword)
        {
            EmployeeID = employeeID;
            EmployeeName = employeeName;
            EmployeeSurname = employeeSurname;
            EmployeeEmail = employeeEmail;
            EmployeePassword = employeePassword;
        }
    }
}
