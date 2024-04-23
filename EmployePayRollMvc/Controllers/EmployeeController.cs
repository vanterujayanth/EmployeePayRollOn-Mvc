using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployePayRollMvc.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeBusiness employeeBusiness;
        public EmployeeController(IEmployeeBusiness employeeBusiness)
        {
            this.employeeBusiness = employeeBusiness;
        }

        public IActionResult Index()
        {
            List<EmployeeModel> lstEmployee = new List<EmployeeModel>();
            lstEmployee = employeeBusiness.GetAllEmployees().ToList();

            return View(lstEmployee);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] EmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                employeeBusiness.AddEmployee(employee);
                return RedirectToAction("Index");
            } 
            return View(employee);
        }
        // to edit or update the employee details
        [HttpGet]
        [Route("Update/{id}")]
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EmployeeModel employee = employeeBusiness.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [Route("Update/{id}")]
        public IActionResult Edit(int id, [Bind] EmployeeModel employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    employeeBusiness.EmployeeUpdate(employee);
                    return RedirectToAction("Index");
                }
                return View();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "error occured on processing request");
                return View(employee);
            }
        }

        // to get details of a employee from database
        [HttpGet]
        public IActionResult GetEmpById(int id)
        {
            id = (int)HttpContext.Session.GetInt32("Id");

            if (id == null)
            {
                return NotFound();
            }
            EmployeeModel employee = employeeBusiness.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound("Something went Wrong....");
            }
            return View(employee);
        }
        // to delete the data of an employee from database

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if(id ==0)
            {
                return NotFound("no id  will strat from  one  or somenthing went wrong");
            }
            EmployeeModel employee = employeeBusiness.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound("somenthing went wrong");
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
           var result = employeeBusiness.DeleteEmployee(id);
            if(result)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
           
        }
        //Login 
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Login(LoginModel model)
        {
            var result = employeeBusiness.Login(model);
            if (result == null)
            {
                return Content("Invalid Credentials");
            }
            else
            {

                HttpContext.Session.SetInt32("Id", result.EmployeeId);
                return RedirectToAction("GetEmpById");
               // return RedirectToAction("GetEmpById", new { Id = result.EmployeeId});
            }
        }
        // getbyname
        //[HttpGet]
        //public IActionResult GetEmpByName()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult GetEmpByName(string name)
        //{

        //    if (name == null)
        //    {
        //        return NotFound();
        //    }
        //    EmployeeModel employee = employeeBusiness.GetEmployeeByName(name);
        //    if (employee == null)
        //    {
        //        return NotFound("Something went Wrong....");
        //    }
        //    return View(employee);
        //}
        [HttpGet("getallbyname")]   /*//https://localhost:7218/getallbyname?name=Teja*/
        public IActionResult GetAllEmployeeByName(string name)
        {
            List<EmployeeModel> lst = new List<EmployeeModel>();
            lst = employeeBusiness.GetEmployeesByName(name).ToList();
            return View(lst);
        }

        [HttpPost]
        public IActionResult GetAllEmploYeeByName(string name)
        {
            if (name == null)
            {
                return NotFound();
            }
            EmployeeModel employee = (EmployeeModel)employeeBusiness.GetEmployeesByName(name);
            if (employee == null)
            {
                return NotFound("something went wrong");
            }
            return View(employee);

        }
        //[HttpGet]

        //public IActionResult GetByName(string name)
        //{
        //    List<EmployeeModel> employees = new List<EmployeeModel>();
        //    employees = employeeBusiness.GetEmployeesByName(name).ToList();
        //    if (employees == null)
        //    {
        //        return Content("Not Found");
        //    }
        //    return View(employees);
        //}
    }
}
