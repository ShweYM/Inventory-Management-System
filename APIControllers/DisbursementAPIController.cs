using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inventory_Management_System.Services;
using Inventory_Management_System.Models;
using Inventory_Management_System.ViewModels;

namespace Inventory_Management_System.APIControllers
{

    [Route("api/disbursement")]
    [ApiController]
    public class DisbursementAPIController : ControllerBase
    {
        DisbursementFormService disbursementFormService;
        DisbursementFormRequisitionFormService disbursementFormRequisitionFormService;
        DisbursementFormProductService disbursementFormProductService;
        StationeryRetrievalRequisitionFormService stationeryRetrievalRequisitionFormService;
        DepartmentService departmentService;
        EmployeeService employeeService;


        public DisbursementAPIController(DisbursementFormService disbursementservice, DisbursementFormRequisitionFormService disbursementFormRequisitionFormService, DisbursementFormProductService disbursementFormProductService, StationeryRetrievalRequisitionFormService stationeryRetrievalRequisitionFormService, DepartmentService departmentService, EmployeeService employeeService)
        {
            this.disbursementFormService = disbursementservice;
            this.disbursementFormRequisitionFormService = disbursementFormRequisitionFormService;
            this.disbursementFormProductService = disbursementFormProductService;
            this.stationeryRetrievalRequisitionFormService = stationeryRetrievalRequisitionFormService;
            this.departmentService = departmentService;
            this.employeeService = employeeService;
        }

        [Route("GetCreatedDisbursementList")]
        [HttpGet]
        public IActionResult GetCreatedDisbursementList()
        {
            List<DisbursementForm> dflist = disbursementFormService.FindCreatedDisbursementForms();
            return Ok(dflist);
        }

        [Route("GetPendingAssignmentDisbursementList")]
        [HttpGet]
        public IActionResult GetPendingDisbursementList()
        {
            List<DisbursementForm> dflist = disbursementFormService.FindPendingAssignDisbursementForms();
            return Ok(dflist);
        }


        [Route("GetPendingDeliveryDisbursementList")]
        [HttpGet]
        public IActionResult GetPendingDeliveryDisbursementList()
        {
            List<DisbursementForm> dflist = disbursementFormService.FindPendingDeliveryDisbursementForms();
            return Ok(dflist);
        }

        [Route("GetCompleteDisbursementList")]
        [HttpGet]
        public IActionResult GetCompleteDisbursementList()
        {
            List<DisbursementForm> dflist = disbursementFormService.FindCompletedDisbursementForms();
            return Ok(dflist);
        }

        [Route("GetDisbursementFormFullDetails")]
        [HttpGet]
        public IActionResult GetDisbursementFormFullDetailsByDFCode(string dfCode)
        {
            DisbursementViewModel disbursementViewModel = new DisbursementViewModel();
            disbursementViewModel.disbursementForm = disbursementFormService.FindDisbursementFormByDFCode(dfCode);
            disbursementViewModel.DisbursementFormRequisitionForms = disbursementFormRequisitionFormService.FindDisbursementFormRequisitionFormsByDFCode(dfCode);
            disbursementViewModel.DisbursementFormProducts = disbursementFormProductService.FindDisbursementFormProductsByDFCode(dfCode);
            return Ok(disbursementViewModel);
        }

        [Route("GetAssignedSRRForms")]
        [HttpGet]
        public IActionResult GetSRRFAssignedFormsToCreateDisbursement()
        {
            return Ok(stationeryRetrievalRequisitionFormService.FindStationeryRetrievalRequisitionFormByStatusOrderByDept());
        }

        [Route("SaveDF")]
        [HttpPost]
        public IActionResult SaveDF([FromBody] DisbursementViewModel dfViewModel)
        {
            bool status = disbursementFormService.CreateDisbursementForm(dfViewModel.srrfAssignedList, dfViewModel.employee, dfViewModel.comment);
            if (status)
            {
                return Ok(dfViewModel);
            }
            else
            {
                return null;
            }


        }                

        [Route("DeliverDF")]
        [HttpPost]
        public IActionResult DeliverDF([FromBody] DisbursementViewModel dfViewModel)
        {
            dfViewModel.comment = "ok";
            bool status = disbursementFormService.ConfirmDFTransactionWithDeptRep(dfViewModel.disbursementForm, dfViewModel.DisbursementFormProducts, dfViewModel.storeclerk, dfViewModel.deptrep, dfViewModel.comment);
            if (status)
            {
                return Ok(dfViewModel);
            }
            else
            {
                return new JsonResult(new { success = "Failure" });
            }


        }

        [Route("AssignDF")]
        [HttpPost]
        public IActionResult AssignDF([FromBody] DisbursementViewModel dfViewModel)
        {
            bool status = disbursementFormService.SaveDFRFPAssignment(
                dfViewModel.DisbursementFormRequisitionFormProducts,
                dfViewModel.employee,
                dfViewModel.disbursementForm,
                dfViewModel.DisbursementFormProducts,
                dfViewModel.DisbursementFormRequisitionForms);
            if (status is true)
            {
                return new JsonResult(new { success = "Success" });
            }
            else
            {
                return new JsonResult(new { success = "Failure" });
            }

        }

        [Route("ApproveDF")]
        [HttpPost]
        public IActionResult ApproveDFByDeptRep([FromBody] DisbursementViewModel dfViewModel)
        {
            bool status = disbursementFormService.ConfirmDFDelivery(dfViewModel.employee, dfViewModel.disbursementForm);
            if (status)
            {
                return Ok(dfViewModel);
            }
            else
            {
                return new JsonResult(new { success = "Failure" });
            }


        }

        [Route("CeatedDF")]
        [HttpGet]
        public IActionResult getCreatedDFByDepRep(int id)
        {
            Employee employee = employeeService.GetEmployeeById(id);
            List<DisbursementForm> disbursementForms = disbursementFormService.FindCreatedDisbursementFormsByDept(employee);
            
            return Ok(disbursementForms);

        }

    }
}