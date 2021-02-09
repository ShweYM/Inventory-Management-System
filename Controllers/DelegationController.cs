using Castle.Core.Internal;
using Inventory_Management_System.Models;
using Inventory_Management_System.Services;
using Inventory_Management_System.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Inventory_Management_System.Controllers
{
    [Authorize]
    public class DelegationController : Controller
    {
        private readonly ILogger<DelegationController> _logger;
        private EmployeeService empService;
        private DelegationService delService;
        private Employee empCheck;

        public DelegationController(ILogger<DelegationController> logger, EmployeeService empService, DelegationService dService)
        {
            _logger = logger;
            this.empService = empService;
            this.delService = dService;
        }

        [Authorize(Roles = "Department Head, Employee")]
        [Route("delegatedeptrep")]
        public IActionResult DelegateDeptRep()
        {

            empCheck = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            if (empCheck.EmployeeType.EmployeeTypeName == "Department Head" || empCheck.TempDeptHeadType.EmployeeTypeName == "Temporary Department Head")
            {
                DelegationViewModel dVModel = new DelegationViewModel();
                dVModel.DepartmentHead = empService.FindDeptHead(empCheck);
                dVModel.DeptEmployeeList = empService.GetEmpListExcludingDeptHeadAndTempDeptHeadandDeptRep(empCheck.Department);
                dVModel.employee = empCheck;
                return View("DelegateDeptRep", dVModel);
            }
            return RedirectToAction("Dashboard", "Home");
        }

        [Authorize(Roles = "Department Head")]
        [Route("delegation/assigntempdepthead")]
        public IActionResult ViewDelegateTempDeptHead()
        {
            if (!HttpContext.Session.GetString("username").IsNullOrEmpty())
            {
                empCheck = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;

                if (empCheck.EmployeeType.EmployeeTypeName == "Department Head")
                {
                    DelegationViewModel dVModel = new DelegationViewModel();
                    dVModel.employee = empCheck;
                    dVModel.DepartmentHead = empCheck;
                    dVModel.DeptEmployeeList = empService.GetEmployeeListByDepartmentExcludingDeptHeadandDeptRep(empCheck.Department);
                    dVModel.dForm = new DelegationForm() { startDate = System.DateTime.Now.Date.AddDays(1), endDate = System.DateTime.Now.Date.AddDays(2) };

                    return View("DelegateTempDeptHead", dVModel);
                }
                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                return RedirectToAction("View", "Home");
            }
        }


        [Route("delegation/summary")]
        public IActionResult ViewDelegateSummary()
        {
            DelegationViewModel dVModel = new DelegationViewModel();
            empCheck = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            dVModel.employee = empCheck;
            dVModel.delegationList = delService.FindAllDelegations(empCheck.Department);
            dVModel.DepartmentHead = empService.FindDeptHead(empCheck);
            return View("DelegateSummary", dVModel);
        }

        [HttpPost("delegation/savetempdepthead/")]
        public IActionResult SaveTempDeptHead([FromBody] DelegationViewModel dVModel)
        {
            
            bool status = delService.AssignTempDeptHead(dVModel.delegateComment, dVModel.delegatee, dVModel.employee, dVModel.startDate,dVModel.endDate );
            if (status is true)
            {
                return new JsonResult(new { success = "Success" });
            }
            else
            {
                return new JsonResult(new { success = "Failure" });
            }
        }

        [HttpPost("delegation/savedeptrep/")]
        public IActionResult SaveDeptRep2([FromBody] DelegationViewModel dVModel)
        {

            bool status = delService.AssignDeptRep2(dVModel.delegateComment, dVModel.delegatee, dVModel.employee);
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
        public IActionResult UpdateDLForm()
        {

            empCheck = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            if (empCheck.EmployeeType.EmployeeTypeName == "Department Head" || empCheck.TempDeptHeadType.EmployeeTypeName == "Temporary Department Head")
            {
                DelegationViewModel dVModel = new DelegationViewModel();
                dVModel.DepartmentHead = empService.FindDeptHead(empCheck);
                dVModel.DeptEmployeeList = empService.GetEmpListExcludingDeptHeadAndTempDeptHeadandDeptRep(empCheck.Department);
                dVModel.employee = empCheck;
                return View("DelegateDeptRep", dVModel);
            }
            return RedirectToAction("Dashboard", "Home");
        }
        [Route("dl/{id}")]
        public IActionResult ViewDLForm([FromRoute] int id)
        {

            empCheck = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;

            DelegationViewModel dVModel = new DelegationViewModel();
            dVModel.DepartmentHead = empService.FindDeptHead(empCheck);
            dVModel.employee = empCheck;
            dVModel.dForm = delService.FindDelegationForm(id);
            return View("DelegateView", dVModel);
        }

        [HttpPost("dl/updatedform")]
        public IActionResult UpdateDLForm([FromBody] DelegationViewModel dVModel)
        {
            bool status = delService.UpdateDLFormStartandEndDate(dVModel.dForm);
            if (status is true)
            {
                return new JsonResult(new { success = "Success" });
            }
            else
            {
                return new JsonResult(new { success = "Failure" });
            }
        }
        [HttpPost("dl/cancel/{id}")]
        public IActionResult CancelDL([FromRoute] int id)
        {
            delService.CancelDLBasedOnId(id);

            return RedirectToAction("ViewDelegateSummary", "Home");

        }
    }
}
