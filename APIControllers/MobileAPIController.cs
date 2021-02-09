using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Inventory_Management_System.DB;
using Inventory_Management_System.Models;
using Inventory_Management_System.ViewModels;
using Inventory_Management_System.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Inventory_Management_System.Controllers
{

    [ApiController]
    public class MobileAPIController : ControllerBase
    {
        //private Employee emp;
        EmployeeService eservice;
        private RequisitionService rpService;
        private ProductService pService;
        protected InventoryManagementSystemContext db;
        public MobileAPIController(InventoryManagementSystemContext db, EmployeeService eservice, RequisitionService rpService, ProductService pService)
        {
            this.eservice = eservice;
            this.rpService = rpService;
            this.pService = pService;
            this.db = db;
        }
        [Route("api/GetEmpList")]
        [HttpGet]
        public IActionResult GetEmpList()
        {
            List<Employee> emplist = eservice.GetEmployeeList();
            return Ok(emplist);
        }

        [Route("api/GetProductList")]
        [HttpGet]
        public IActionResult GetProductList()
        {
            List<Product> productlist = db.Products.ToList();
            return Ok(productlist);
        }

        [Route("api/GetEmpObj")]
        [HttpGet]
        public IActionResult GetEmployeeByUserName(string Username, string Password)
        {
            Employee emp = eservice.GetEmployee(Username, Password);
            if(emp != null)
            {
                return Ok(emp);
            }
            else
            {
                return null;
            }
            
        }

        [Route("api/reqsummary")]
        [HttpGet]
        public IActionResult GetReqSummary()
        {
            Employee emp;
            RequisitionSummaryViewModel srViewModel = new RequisitionSummaryViewModel();

            /* emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
             srViewModel.employee = emp;*/
            emp = eservice.GetEmployeeById(6);  //hard-coded id for test

            if (emp.EmployeeType.EmployeeTypeName == "Store Manager" || emp.EmployeeType.EmployeeTypeName == "Department Head")
            {
                List<RequisitionForm> rfListPending = rpService.FindPendingRequisitionsByEmployeeAsHead(emp.Department);
                List<RequisitionForm> rfListProcessed = rpService.FindRequisitionsInDept(emp.Department);
                srViewModel.rfListPending = rfListPending;
                srViewModel.rfListProcessed = rfListProcessed;
            }
            else
            {
                List<RequisitionForm> rfListPending = rpService.FindPendingRequisitionsByEmployee(emp);
                List<RequisitionForm> rfListProcessed = rpService.FindRequisitionsOtherThanSubmittedByEmployee(emp);
                srViewModel.rfListPending = rfListPending;
                srViewModel.rfListProcessed = rfListProcessed;
            }
            return Ok(srViewModel);
        }

        [Route("api/rf/{id}")]
        [HttpGet]
        public IActionResult ViewRF(int id)
        {
            RequisitionViewModel vmRequisition = new RequisitionViewModel();
            //emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;

            Employee emp = eservice.GetEmployeeById(6);  //hard-coded id for test
            RequisitionForm rf = rpService.FindRequisitionFormById(id);
            List<RequisitionFormsProduct> rfpList = rpService.FindRequisitionFormProductListById(id);

            vmRequisition.employee = emp;
            vmRequisition.requisitionForm = rf;
            vmRequisition.rfpList = rfpList;

            if (emp.EmployeeType.EmployeeTypeName.Equals("Store Manager") || emp.EmployeeType.EmployeeTypeName.Equals("Department Head"))
            {
                if (!rf.RFStatus.Equals(Enums.RFStatus.Submitted))
                {

                    return Ok(vmRequisition);
                }
                else
                {
                    for (int i = 0; i < rfpList.Count; i++)
                    {
                        rfpList[i].ProductApproved = rfpList[i].ProductRequested;
                    }
                    return Ok(vmRequisition);
                }
            }
            else
            {
                return Ok(vmRequisition);
            }
        }

        [Route("api/rf/apply")]
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
            List<Product> products = pService.FindProducts();
            vmRequisition.productList = products;
            return Ok(vmRequisition);
        }
    }
}
