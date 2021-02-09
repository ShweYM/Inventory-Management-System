using Inventory_Management_System.Models;
using Inventory_Management_System.Services;
using Inventory_Management_System.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace Inventory_Management_System.Controllers
{
    [Authorize]
    public class RequisitionController : Controller
    {
        private Employee emp;
        private ProductService pService;
        private RequisitionService rpService;
        private EmployeeService empService;
        private readonly ILogger<RequisitionController> _logger;
        private DisbursementFormService dfService;

        public RequisitionController(ILogger<RequisitionController> logger, ProductService pService, EmployeeService empService, RequisitionService rpService, DisbursementFormService dfService)
        {
            _logger = logger;
            this.pService = pService;
            this.rpService = rpService;
            this.empService = empService;
            this.dfService = dfService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Department Representative, Employee")]
        [Route("rf/apply")]
        public IActionResult CreateRequisitionForm()
        {
            emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            RequisitionViewModel rVModel = new RequisitionViewModel();
            rVModel.employee = emp;
            rVModel.productList = pService.FindProducts();
            return View("RequisitionFormApply", rVModel);
        }

        [HttpPost("rf/save")]
        public IActionResult Save([FromBody] RequisitionViewModel rfVModel)
        {
            bool status = rpService.UpdateRequistionProduct(rfVModel.employee, rfVModel.Comment, rfVModel.productList);

            if (status)
            {
                return new JsonResult(new { success = "Success" });
            }
            else
            {
                return new JsonResult(new { success = "Failure" });
            }
        }

        [Authorize(Roles = "Employee, Department Representative, Department Head")]
        [Route("rf/summary")]
        public IActionResult Summary()
        {
            RequisitionSummaryViewModel srViewModel = new RequisitionSummaryViewModel();

            emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            srViewModel.employee = emp;

            if (emp.EmployeeType.EmployeeTypeName == "Department Head" || emp.EmployeeType.EmployeeTypeName == "Department Representative")
            {
                srViewModel.rfListPending = rpService.FindPendingRequisitionsByEmployeeAsHead(emp.Department);
                srViewModel.rfListProcessed = rpService.FindRequisitionsInDept(emp.Department);
            }
            else
            {
                srViewModel.rfListPending = rpService.FindPendingRequisitionsByEmployee(emp);
                srViewModel.rfListProcessed = rpService.FindRequisitionsOtherThanSubmittedByEmployee(emp);
            }

            return View("RequisitionFormSummary", srViewModel);
        }

        [Authorize(Roles = "Employee, Department Representative")]
        [Route("rf/cancel/{id}")]
        public IActionResult Cancel([FromRoute] int id)
        {
            System.Diagnostics.Debug.WriteLine(id);
            rpService.ChangeRFStatusToCancelledById(id);
            return RedirectToAction("Summary");
        }

        [Authorize(Roles = "Employee, Department Representative, Department Head")]
        [Route("rf/{id}")]
        public IActionResult ViewRF([FromRoute] int id)
        {
            RequisitionViewModel rVModel = new RequisitionViewModel();
            emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;

            RequisitionForm rf = rpService.FindRequisitionFormById(id);
            List<RequisitionFormsProduct> rfpList = rpService.FindRequisitionFormProductListById(id);
            List<DisbursementFormRequisitionFormProduct> dfrfpList = dfService.FindDFRFPListByRFId(id);
            List<DisbursementFormRequisitionForm> dfrfList = dfService.FindDFRFListByRFId(id);

            rVModel.employee = emp;
            rVModel.requisitionForm = rf;
            rVModel.rfpList = rfpList;
            rVModel.dfrfpList = dfrfpList;
            rVModel.dfrfList = dfrfList;

            return View("RequisitionFormView", rVModel);
        }

        [Authorize(Roles = "Department Head")]
        [Route("rf/approval/{id}")]
        public IActionResult ViewApprovalRF([FromRoute] int id)
        {
            RequisitionViewModel rVModel = new RequisitionViewModel();
            emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            RequisitionForm rf = rpService.FindRequisitionFormById(id);
            List<RequisitionFormsProduct> rfpList = rpService.FindRequisitionFormProductListById(id);
            rVModel.employee = emp;
            rVModel.requisitionForm = rf;
            rVModel.rfpList = rfpList;
            return View("ViewRequisitionFormPendingApproval", rVModel);
        }

        [Authorize(Roles = "Department Head")]
        [HttpPost("rf/approve/{id}")]
        public IActionResult Approve([FromBody] RequisitionViewModel rVModel)
        {
            bool status = rpService.ChangeRFStatusToApprovedById(rVModel.requisitionForm,rVModel.rfpList, rVModel.employee, rVModel.RFHeadReply);
            if (status is true)
            {
                return new JsonResult(new { success = "Success" });
            }
            else
            {
                return new JsonResult(new { success = "Failure" });
            }
        }
        [Authorize(Roles = "Department Head")]
        [HttpPost("rf/reject/{id}")]
        public IActionResult Reject([FromBody] RequisitionViewModel rVModel)
        {
            bool status = rpService.ChangeRFStatusToRejectedById(rVModel.requisitionForm, rVModel.employee, rVModel.RFHeadReply);
            if (status is true)
            {
                return new JsonResult(new { success = "Success" });
            }
            else
            {
                return new JsonResult(new { success = "Failure" });
            }
        }

    }
}
