using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory_Management_System.Models;
using Inventory_Management_System.Services;
using Inventory_Management_System.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management_System.APIControllers
{
    [Route("api/Delegation")]
    [ApiController]
    public class DelegationAPIController : Controller
    {
        DelegationService delegationService;
        EmployeeService empService;

        public DelegationAPIController(DelegationService delegationService, EmployeeService employeeService)
        {
            this.delegationService = delegationService;
            this.empService = employeeService;
        }

        [Route("delegatedeptrep")]
        public IActionResult DelegateDeptRep(int id)
        {
            Employee employee = empService.GetEmployeeById(id);
            if (employee.EmployeeType.EmployeeTypeName.Equals("Department Head"))
            {
                DelegationViewModel dVModel = new DelegationViewModel();
                dVModel.DepartmentHead = empService.FindDeptHead(employee);
                dVModel.DeptEmployeeList = empService.GetEmpListExcludingDeptHeadAndTempDeptHeadandDeptRep(employee.Department);
                dVModel.employee = employee;
                return Ok(dVModel);
            }
            else
            {
                return null;
            }
        }

        [Route("GetEmpListForDelegate")]
        public IActionResult ViewDelegateTempDeptHead(int id)
        {

            Employee employee = empService.GetEmployeeById(id);
            if (employee.EmployeeType.EmployeeTypeName.Equals("Department Head"))
            {
                DelegationViewModel dVModel = new DelegationViewModel();
                dVModel.employee = employee;
                dVModel.DepartmentHead = employee;
                dVModel.DeptEmployeeList = empService.GetEmployeeListByDepartmentExcludingDeptHeadandDeptRep(employee.Department);
                return Ok(dVModel);
            }
            else
            {
                return null;
            }

        }


        [Route("summary")]
        public IActionResult ViewDelegateSummary(int id)
        {
            Employee employee = empService.GetEmployeeById(id);
            if (employee.EmployeeType.EmployeeTypeName.Equals("Department Head"))
            {
                DelegationViewModel dVModel = new DelegationViewModel();
                dVModel.employee = employee;
                dVModel.delegationList = delegationService.FindAllDelegations(employee.Department);
                dVModel.DepartmentHead = empService.FindDeptHead(employee);
                return Ok(dVModel);
            }
            else
            {
                return null;
            }

        }

        [HttpPost("SaveEmpDepHead")]
        public IActionResult SaveTempDeptHead([FromBody] DelegationViewModel dvm)
        {
            bool status = delegationService.AssignTempDeptHead(dvm.delegateComment, dvm.delegatee, dvm.employee, dvm.startDate, dvm.endDate);
            if (status == true)
            {
                return Ok(dvm);
            }
            else
            {
                return null;
            }
        }

        [HttpPost("savedeptrep")]
        public IActionResult SaveDeptRep2([FromBody] DelegationViewModel dVModel)
        {

            bool status = delegationService.AssignDeptRep2(dVModel.delegateComment, dVModel.delegatee, dVModel.employee);
            if (status is true)
            {
                return new JsonResult(new { success = "Success" });
            }
            else
            {
                return new JsonResult(new { success = "Failure" });
            }   
        }

        [Route("dl/update/{id}")]
        public IActionResult UpdateDLForm(int id)
        {
            Employee employee = empService.GetEmployeeById(id);
            if (employee.EmployeeType.EmployeeTypeName.Equals("Department Head"))
            {

                DelegationViewModel dVModel = new DelegationViewModel();
                dVModel.DepartmentHead = empService.FindDeptHead(employee);
                dVModel.DeptEmployeeList = empService.GetEmpListExcludingDeptHeadAndTempDeptHeadandDeptRep(employee.Department);
                dVModel.employee = employee;
                return Ok(dVModel);
            }
            else
            {
                return null;
            }

        }
        [Route("dl/{id}")]
        public IActionResult ViewDLForm([FromRoute] int id)
        {

            Employee employee = empService.GetEmployeeById(id);

            DelegationViewModel dVModel = new DelegationViewModel();
            dVModel.DepartmentHead = empService.FindDeptHead(employee);
            dVModel.employee = employee;
            dVModel.dForm = delegationService.FindDelegationForm(id);
            return View("DelegateView", dVModel);
        }
    }
}
