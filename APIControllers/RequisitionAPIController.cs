using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inventory_Management_System.Services;
using Inventory_Management_System.Models;
using Inventory_Management_System.ViewModels;
using Inventory_Management_System.APIControllers;

namespace Inventory_Management_System.APIControllers
{
    [Route("api/Requisition")]
    [ApiController]
    public class RequisitionAPIController : ControllerBase
    {
        RequisitionService rpservice;
        EmployeeService eservice;
        ProductService pservice;

        public RequisitionAPIController(RequisitionService rpservice,EmployeeService eservice,ProductService pservice)
        {
            this.rpservice = rpservice;
            this.eservice = eservice;
            this.pservice = pservice;
        }

        [Route("GetRequisitionById")]
        [HttpGet]
        public IActionResult GetRequisitionById(int reqId)
        {
            System.Diagnostics.Debug.WriteLine("This is inside get req by Id : " + reqId);
            RequisitionForm rqform = rpservice.FindRequisitionFormById(reqId);
            return Ok(rqform);
        }

        [Route("GetReqSummary")]
        [HttpGet]
        public IActionResult GetReqSummary(string Username)
        {
            Employee emp;
            RequisitionSummaryViewModel srViewModel = new RequisitionSummaryViewModel();

            emp = eservice.GetEmployee(Username);
            
            srViewModel.employee = emp;
            if (emp.EmployeeType.EmployeeTypeName == "Department Head" || emp.EmployeeType.EmployeeTypeName == "Department Representative")
            {
                srViewModel.rfListPending = rpservice.FindPendingRequisitionsByEmployeeAsHead(emp.Department);
                srViewModel.rfListProcessed = rpservice.FindRequisitionsInDept(emp.Department);
            }
            else
            {
                srViewModel.rfListPending = rpservice.FindPendingRequisitionsByEmployee(emp);
                srViewModel.rfListProcessed = rpservice.FindRequisitionsOtherThanSubmittedByEmployee(emp);
            }
            return Ok(srViewModel);
        }

        [Route("ViewRequisitionFormById")]
        [HttpGet]
        public IActionResult ViewRF(int reqId, String Username)
        {
            RequisitionViewModel vmRequisition = new RequisitionViewModel();

            //emp = eservice.GetEmployeeById(6);  //hard-coded id for test
            Employee emp = eservice.GetEmployee(Username);
            RequisitionForm rf = rpservice.FindRequisitionFormById(reqId);
            List<RequisitionFormsProduct> rfpList = rpservice.FindRequisitionFormProductListById(reqId);

            vmRequisition.employee = emp;
            vmRequisition.requisitionForm = rf;
            vmRequisition.rfpList = rfpList;
            return Ok(vmRequisition);
        }

        [Route("ViewApprovalRF")]
        [HttpGet]
        public IActionResult ViewApprovalRF(int id)
        {
            RequisitionViewModel rVModel = new RequisitionViewModel();
            //emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            RequisitionForm rf = rpservice.FindRequisitionFormById(id);
            List<RequisitionFormsProduct> rfpList = rpservice.FindRequisitionFormProductListById(id);
            //rVModel.employee = emp;
            rVModel.requisitionForm = rf;
            rVModel.rfpList = rfpList;
            return Ok(rVModel);
        }

        [Route("ApplyRequisitionForm")]
        [HttpGet]
        public IActionResult CreateRF()
        {
            RequisitionViewModel vmRequisition = new RequisitionViewModel();
            //int idCheck = (int)HttpContext.Session.GetInt32("id");
            /*if (HttpContext.Session.GetInt32("id") != null)
            {
                emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
                if (emp != null)
                {
                    System.Diagnostics.Debug.WriteLine("This is inside view Id : " + emp.Id);
                    vmRequisition.employee = emp;
                }
            }*/
            List<Product> products = pservice.FindProducts();
            vmRequisition.productList = products;
            return Ok(vmRequisition);
        }

        [Route("SaveRf")]
        [HttpPost]
        public IActionResult Save([FromBody] RequisitionViewModel rfViewModel)
        {
            bool status = rpservice.UpdateRequistionProduct(rfViewModel.employee, rfViewModel.Comment, rfViewModel.productList);
            if (status)
            {
                return Ok(rfViewModel);
            }
            else
            {
                return null;
            }


        }

        [Route("Approve")]
        [HttpPost]
        public IActionResult Approve([FromBody] RequisitionViewModel rVModel)
        {
            bool status = rpservice.ChangeRFStatusToApprovedById(rVModel.requisitionForm, rVModel.rfpList, rVModel.employee, rVModel.RFHeadReply);
            if (status is true)
            {
                return Ok(rVModel);
            }
            else
            {
                return null;
            }
        }

        [Route("Reject")]
        [HttpPost]
        public IActionResult Reject([FromBody]RequisitionViewModel rVModel)
        {
            bool status = rpservice.ChangeRFStatusToRejectedById(rVModel.requisitionForm, rVModel.employee, rVModel.RFHeadReply);
            if (status is true)
            {
                return Ok(rVModel);
            }
            else
            {
                return null;
            }
        }
        [Route("Cancel")]
        [HttpPost]
        public IActionResult Cancel(int id)
        {
            rpservice.ChangeRFStatusToCancelledById(id);
            RequisitionForm rf = rpservice.FindRequisitionFormById(id);
            return Ok(rf);

        }
    }
}