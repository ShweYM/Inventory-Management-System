using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory_Management_System.Models;
using Inventory_Management_System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management_System.APIControllers
{
    [Route("api/Department")]
    [ApiController]
    public class DepartmentAPIController : ControllerBase
    {
        DepartmentService deptservice;
        public DepartmentAPIController(DepartmentService deptservice)
        {
            this.deptservice = deptservice;
        }

        [Route("GetDepartmentList")]
        [HttpGet]
        public IActionResult GetDepartmentList()
        {
            List<Department> deptlist = deptservice.FindDepartmentList();
            return Ok(deptlist);
        }

        [Route("GetDepartmentInfo")]
        [HttpGet]
        public IActionResult GetDepartment(string deptname)
        {
            Department department = deptservice.GetDepartment(deptname);
            return Ok(department);
        }
    }
}