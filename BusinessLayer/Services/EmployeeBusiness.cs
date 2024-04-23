using BusinessLayer.Interfaces;
using ModelLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class EmployeeBusiness:IEmployeeBusiness
    {
        private readonly IEmployeeRepository employeeRepository;
        public EmployeeBusiness(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        public IEnumerable<EmployeeModel> GetAllEmployees()
        {
            return employeeRepository.GetAllEmployees();
        }
        public EmployeeModel AddEmployee(EmployeeModel employee)
        {
            return employeeRepository.AddEmployee(employee);
        }
        public bool DeleteEmployee(int id)
        {
            return employeeRepository.DeleteEmployee(id);
        }
        
        public EmployeeModel EmployeeUpdate(EmployeeModel employee)
        {
            return employeeRepository.EmployeeUpdate(employee);

        }
        public EmployeeModel GetEmployeeById(int id)
        {
            return employeeRepository.GetEmployeeById(id);
        }
        public EmployeeModel Login(LoginModel loginModel)
        {
            return employeeRepository.Login(loginModel);
        }
        //public EmployeeModel GetEmployeeByName(string name)
        //{
        //    return employeeRepository.GetEmployeeByName(name);
        //}
        public IEnumerable<EmployeeModel> GetEmployeesByName(string name)
        {
            return employeeRepository.GetEmployeesByName(name);
        }
    }
}
