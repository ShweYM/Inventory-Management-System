using Inventory_Management_System.Models;
using Inventory_Management_System.Services;
using Inventory_Management_System.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Inventory_Management_System.Controllers
{
    [Authorize]
    public class DisbursementController : Controller
    {
        private Employee emp;

        private EmployeeService eService;
        private DisbursementFormService dfService;
        private DisbursementFormProductService dfpService;
        private DisbursementFormRequisitionFormProductService dfrfpService;
        private StationeryRetrievalRequisitionFormService srrfService;
        private readonly ILogger<DisbursementController> _logger;


        public DisbursementController(ILogger<DisbursementController> logger, 
            EmployeeService eService, 
            DisbursementFormService dfService, 
            DisbursementFormProductService dfpService, 
            StationeryRetrievalRequisitionFormService srrfService, 
            DisbursementFormRequisitionFormProductService dfrfpService)
        {
            _logger = logger;
            this.eService = eService;
            this.dfService = dfService;
            this.dfpService = dfpService;
            this.srrfService = srrfService;
            this.dfrfpService = dfrfpService;
        }
        

        [Authorize(Roles = "Store Clerk, Store Supervisor, Store Manager")]
        [Route("df/create")]
        public IActionResult CreateDisbursement()
        {
            emp  = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;

            DisbursementViewModel dVModel = new DisbursementViewModel();
            dVModel.employee = emp;
            dVModel.srrfAssignedList = srrfService.FindStationeryRetrievalRequisitionFormByStatusOrderByDept();

            return View("DisbursementForm", dVModel);
        }

        [Authorize(Roles = "Store Clerk, Department Representative, Store Supervisor, Store Manager")]
        [Route("df/summary")]
        public IActionResult Summary()
        {
            emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;

            DisbursementViewModel dVModel = new DisbursementViewModel();

            if (emp.Department.DepartmentName != "Store")
            {
                dVModel.employee = emp;
                dVModel.dfCompletedList = dfService.FindCompletedDisbursementFormsByDept(emp);
                dVModel.dfPendingDeliveryList = dfService.FindPendingDeliveryDisbursementFormsByDept(emp);
                dVModel.dFCreatedList = dfService.FindCreatedDisbursementFormsByDept(emp);
                dVModel.dfPendingAssignList = dfService.FindPendingAssignDisbursementFormsByDept(emp);
            }
            else
            {
                dVModel.employee = emp;
                dVModel.dfCompletedList = dfService.FindCompletedDisbursementForms();
                dVModel.dfPendingDeliveryList = dfService.FindPendingDeliveryDisbursementForms();
                dVModel.dFCreatedList = dfService.FindCreatedDisbursementForms();
                dVModel.dfPendingAssignList = dfService.FindPendingAssignDisbursementForms();
            }
            return View("DFSummary", dVModel);
        }

        [Route("df/save")]
        public IActionResult CreateDF([FromBody] DisbursementViewModel dVModel)
        {
            bool status = dfService.CreateDisbursementForm(dVModel.srrfAssignedList, dVModel.employee, dVModel.comment);
            if (status is true)
            {
                return new JsonResult(new { success = "Success" });
            }
            else
            {
                return new JsonResult(new { success = "Failure" });
            }
        }

       

        [Route("df/approve/{id}")]
        public IActionResult ApproveDFByDeptRep([FromBody] DisbursementViewModel dVModel, [FromRoute] int id)
        {
            bool status = dfService.ConfirmDFDelivery(dVModel.employee, dVModel.disbursementForm);
            if (status is true)
            {
                return new JsonResult(new { success = "Success" });
            }
            else
            {
                return new JsonResult(new { success = "Failure" });
            }
        }

        [Authorize(Roles = "Store Clerk, Store Supervisor, Store Manager, Department Representative")]
        [Route("df/transaction/{id}")]
        public IActionResult CollectDF([FromRoute] int id)
        {
            emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;

            DisbursementViewModel dVModel = new DisbursementViewModel();
            dVModel.employee = emp;
            dVModel.disbursementForm = dfService.FindDisbursementFormById(id);
            dVModel.DisbursementFormProducts = dfpService.FindDisbursementFormProductsByDFId(id);
            dVModel.DisbursementFormRequisitionFormProducts = dfrfpService.FindDisbursementFormRequisitionFormProductsByDFId(id);
            dVModel.DisbursementFormRequisitionForms = dfrfpService.FindDFRFList(id);
            return View("DisbursementFormViewTransaction", dVModel);

        }

        

        [Route("df/confirmdelivery")]
        public IActionResult ConfirmDelivery([FromBody] DisbursementViewModel dVModel)
        {
            bool status = dfService.ConfirmDFTransactionWithDeptRep(dVModel.disbursementForm, dVModel.DisbursementFormProducts, dVModel.storeclerk, dVModel.deptrep, dVModel.comment);
            if (status is true)
            {
                return new JsonResult(new { success = "Success" });
            }
            else
            {
                return new JsonResult(new { success = "Failure" });
            }

        }

        [Route("df/transactionassign/{id}")]
        public IActionResult DFRFASSIGN([FromRoute] int id)
        {
            emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;

            DisbursementViewModel dVModel = new DisbursementViewModel();

            dVModel.employee = emp;
            dVModel.disbursementForm = dfService.FindDisbursementFormById(id);
            dVModel.DisbursementFormProducts = dfpService.FindDisbursementFormProductsByDFId(id);
            dVModel.DisbursementFormRequisitionFormProducts = dfrfpService.FindDFRFPByDFOrderByProduct(id);
            dVModel.DisbursementFormRequisitionForms = dfrfpService.FindDFRFList(id);
            return View("DisbursementFormViewAssign", dVModel);
        }

        [Route("df/transactionassigncomplete")]
        public IActionResult DFAssignComplete([FromBody] DisbursementViewModel dVModel)
        {
            bool status = dfService.SaveDFRFPAssignment(
                dVModel.DisbursementFormRequisitionFormProducts, 
                dVModel.employee, 
                dVModel.disbursementForm,
                dVModel.DisbursementFormProducts,
                dVModel.DisbursementFormRequisitionForms);
            if (status is true)
            {
                return new JsonResult(new { success = "Success" });
            }
            else
            {
                return new JsonResult(new { success = "Failure" });
            }
        }

        [Authorize(Roles = "Store Clerk, Department Representative, Store Supervisor, Store Manager")]
        [Route("df/approval/{id}")]
        public IActionResult ViewApprovalDF([FromRoute] int id)
        {
            emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            DisbursementViewModel dVModel = new DisbursementViewModel();

            dVModel.employee = emp;
            dVModel.disbursementForm = dfService.FindDisbursementFormById(id);
            dVModel.DisbursementFormProducts = dfpService.FindDisbursementFormProductsByDFId(id);
            dVModel.DisbursementFormRequisitionFormProducts = dfrfpService.FindDFRFPByDFId(id);
            dVModel.DisbursementFormRequisitionForms = dfrfpService.FindDFRFList(id);
            return View("DisbursementFormViewApprove", dVModel);
        }

        [Authorize(Roles = "Store Clerk, Department Representative, Store Supervisor, Store Manager")]
        [Route("df/view/{id}")]
        public IActionResult ViewDF([FromRoute] int id)
        {
            emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            DisbursementViewModel dVModel = new DisbursementViewModel();

            dVModel.employee = emp;
            dVModel.disbursementForm = dfService.FindDisbursementFormById(id);
            dVModel.DisbursementFormProducts = dfpService.FindDisbursementFormProductsByDFId(id);
            dVModel.DisbursementFormRequisitionFormProducts = dfrfpService.FindDFRFPByDFId(id);
            dVModel.DisbursementFormRequisitionForms = dfrfpService.FindDFRFList(id);

            return View("DisbursementFormView", dVModel);
        }



    }
}
