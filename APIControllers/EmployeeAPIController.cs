using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inventory_Management_System.Services;
using Inventory_Management_System.Models;

namespace Inventory_Management_System.APIControllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeeAPIController : ControllerBase
    {
        EmployeeService eservice;
        public EmployeeAPIController(EmployeeService eservice)
        {
            this.eservice = eservice;
        }

        [Route("GetEmployeeList")]
        [HttpGet]
        public IActionResult GetEmployeeList()
        {
            List<Employee> emplist = eservice.GetEmployeeList();
            return Ok(emplist);
        }

        [Route("GetEmpObj")]
        [HttpGet]
        public IActionResult GetEmployeeByUserName(string Username, string Password)
        {
            Employee emp = eservice.GetEmployee(Username, Password);
            if (emp != null)
            {
                return Ok(emp);
            }
            else
            {
                return null;
            }

        }

        //Sheryl - testing something for dept summary in Android, pls leave this alone for now
        [Route("GetEmployeeById")]
        [HttpGet]
        public IActionResult GetEmployeeById(int id)
        {
            Employee employee = eservice.GetEmployeeById(id);
            return Ok(employee);
        }
    }
}