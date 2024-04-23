using ModelLayer.Models;

namespace RepositoryLayer.Interfaces
{
    public interface IEmployeeRepository
    {
        public IEnumerable<EmployeeModel> GetAllEmployees();
        public EmployeeModel AddEmployee(EmployeeModel employee);
        public bool DeleteEmployee(int id);
        public EmployeeModel EmployeeUpdate(EmployeeModel employee);
        public EmployeeModel GetEmployeeById(int id);
        public EmployeeModel Login(LoginModel loginModel);
        //public EmployeeModel GetEmployeeByName(string name);
        public IEnumerable<EmployeeModel> GetEmployeesByName(string name);


    }
}