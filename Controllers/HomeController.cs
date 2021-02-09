using Castle.Core.Internal;
using Inventory_Management_System.Enums;
using Inventory_Management_System.Models;
using Inventory_Management_System.Services;
using Inventory_Management_System.Utils;
using Inventory_Management_System.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;



namespace Inventory_Management_System.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private EmployeeService empService;
        private RequisitionService rfService;
        private StationeryRetrievalFormService srfService;
        private DisbursementFormProductService dfpService;
        private DashboardService dashService;
        private DelegationService delService;
        private CollectionPointService cpService;
        private Employee empCheck;

        public HomeController(ILogger<HomeController> logger,
            EmployeeService empService,
            RequisitionService rfService,
            StationeryRetrievalFormService srfService,
            DisbursementFormProductService dfpService,
            DashboardService dashService,
            DelegationService delService,
            CollectionPointService cpService)
        {
            _logger = logger;
            this.empService = empService;
            this.rfService = rfService;
            this.srfService = srfService;
            this.dfpService = dfpService;
            this.dashService = dashService;
            this.delService = delService;
            this.cpService = cpService;
        }

        //Redirect to Login Page
        public IActionResult Index()
        {
            RemoveCookie("InventoryManagement.AuthCookie");
            
            return RedirectToAction("login");
        }

        [AllowAnonymous]
        //Method for Login Page
        [Route("login")]
        public IActionResult Login(string username, string password)
        {

            if (HttpContext.Session.GetString("username").IsNullOrEmpty()) //used to transfer viewbag data back to view
            {
                StartSession();
            }
            else
            {
                TempData["empCheck"] = JsonConvert.SerializeObject(empCheck);
            }

            if (username != null || !HttpContext.Session.GetString("username").IsNullOrEmpty())
            {
                if (!HttpContext.Session.GetString("username").IsNullOrEmpty())
                {
                    empCheck = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
                    return RedirectToAction("Dashboard");
                }
                empCheck = empService.GetEmployee(username, password);
                if (empCheck != null)
                {

                    HttpContext.Session.SetString("firstName", empCheck.Firstname);
                    HttpContext.Session.SetString("lastName", empCheck.Lastname);
                    HttpContext.Session.SetString("username", empCheck.Username);
                    HttpContext.Session.SetInt32("id", empCheck.Id);
                    HttpContext.Session.SetString("empType", empCheck.EmployeeType.EmployeeTypeName);
                    HttpContext.Session.SetString("employee", JsonConvert.SerializeObject(empCheck));
                    bool isTempDeptHead = delService.CheckEmpIsTempDeptHeadToday(empCheck);
                    if (isTempDeptHead == true)
                    {
                        HttpContext.Session.SetString("tempDeptHead", empCheck.TempDeptHeadType.EmployeeTypeName);
                    }

                    //Add AuthCookie

                    var userClaims = new List<Claim>(){
                        new Claim(ClaimTypes.Name, empCheck.Username),
                        new Claim(ClaimTypes.Role, empCheck.EmployeeType.EmployeeTypeName),
                         };

                    var grandmaIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });

                    HttpContext.SignInAsync(userPrincipal);

                    return RedirectToAction("Dashboard");
                }
            }

            return View("Login");
        }

        [Route("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            Response.Cookies.Delete(".AspNetCore.Session");
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("access-denied")]
        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }

        public IActionResult StartSession()
        {
            string sessionId = Guid.NewGuid().ToString();

            HttpContext.Session.SetString("sessionId", sessionId);

            return RedirectToAction("login");
        }
        [Route("Home/Dashboard")]
        public IActionResult Dashboard()
        {
            DashboardViewModel dVModel = new DashboardViewModel();

            empCheck = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            dVModel.emp = empCheck;
            if (empCheck.Department.DepartmentName != "Store")
            {
                dVModel.deptRep = empService.FindEmployeeWhoIsDeptRep(empCheck.Department);
            }
            dVModel.cpList = cpService.GetCPList();
            if (empCheck.EmployeeType.EmployeeTypeName == "Employee" || empCheck.EmployeeType.EmployeeTypeName == "Department Representative")
            {
                dVModel.TotalRFSubmitted = dashService.TotalRFNosByEmployee(empCheck, RFStatus.Submitted);
                dVModel.TotalRFApproved = dashService.TotalRFNosByEmployee(empCheck, RFStatus.Approved);
                dVModel.TotalRFRejected = dashService.TotalRFNosByEmployee(empCheck, RFStatus.Rejected);
                dVModel.TotalRFNotCompleted = dashService.TotalRFNosByEmployee(empCheck, RFStatus.NotCompleted);
                dVModel.TotalRFCompleted = dashService.TotalRFNosByEmployee(empCheck, RFStatus.Completed);
                dVModel.TotalRFOngoing = dashService.TotalRFNosByEmployee(empCheck, RFStatus.Ongoing);
            }
            if (empCheck.EmployeeType.EmployeeTypeName == "Department Representative")
            {
                dVModel.TotalDFCreated = dashService.TotalDFNosByDept(empCheck, DFStatus.Created);
                dVModel.TotalDFPendingAssignment = dashService.TotalDFNosByDept(empCheck, DFStatus.PendingAssignment);
                dVModel.TotalDFPendingDelivery = dashService.TotalDFNosByDept(empCheck, DFStatus.PendingDelivery);
                dVModel.TotalDFCompleted = dashService.TotalDFNosByDept(empCheck, DFStatus.Completed);
            }
            if (empCheck.EmployeeType.EmployeeTypeName == "Department Head" || empCheck.TempDeptHeadType != null)
            {
                dVModel.TotalRFSubmittedDept = dashService.TotalRFNosByDept(empCheck, RFStatus.Submitted);
                dVModel.TotalRFApprovedDept = dashService.TotalRFNosByDept(empCheck, RFStatus.Approved);
                dVModel.TotalRFRejectedDept = dashService.TotalRFNosByDept(empCheck, RFStatus.Rejected);
                dVModel.TotalRFNotCompletedDept = dashService.TotalRFNosByDept(empCheck, RFStatus.NotCompleted);
                dVModel.TotalRFCompletedDept = dashService.TotalRFNosByDept(empCheck, RFStatus.Completed);
                dVModel.TotalRFOngoingDept = dashService.TotalRFNosByDept(empCheck, RFStatus.Ongoing);
            }

            if (empCheck.EmployeeType.EmployeeTypeName == "Store Clerk" || empCheck.EmployeeType.EmployeeTypeName == "Store Supervisor" || empCheck.EmployeeType.EmployeeTypeName == "Store Manager")
            {
                dVModel.TotalRFApproved = dashService.TotalRFNos(empCheck, RFStatus.Approved);
                dVModel.TotalRFNotCompleted = dashService.TotalRFNos(empCheck, RFStatus.NotCompleted);

                dVModel.TotalSROpen = dashService.TotalSRNos(empCheck, SRStatus.Open);
                dVModel.TotalSRPendingAssignment = dashService.TotalSRNos(empCheck, SRStatus.PendingAssignment);
                dVModel.TotalSRAssigned = dashService.TotalSRNos(empCheck, SRStatus.Assigned);

                dVModel.TotalDFCreated = dashService.TotalDFNos(empCheck, DFStatus.Created);
                dVModel.TotalDFPendingAssignment  = dashService.TotalDFNos(empCheck, DFStatus.PendingAssignment);
                dVModel.TotalDFPendingDelivery = dashService.TotalDFNos(empCheck, DFStatus.PendingDelivery);
                dVModel.TotalDFCompleted = dashService.TotalDFNos(empCheck, DFStatus.Completed);
            }
            if (empCheck.EmployeeType.EmployeeTypeName == "Store Supervisor" || empCheck.EmployeeType.EmployeeTypeName == "Store Manager")
            {
                dVModel.TotalITPendingApproval = dashService.TotalITNos(empCheck, ITStatus.PendingApproval);
                dVModel.TotalITApproved = dashService.TotalITNos(empCheck, ITStatus.Approved);
            }
            return View("Dashboard", dVModel);
        }

        [HttpPost("cpUpdate")]
        public IActionResult ChangeCP([FromBody] DashboardViewModel dVModel)
        {
            bool status = cpService.UpdateCollectionPoint(dVModel.emp, dVModel.cpUpdated);
            if (status is true)
            {
                return new JsonResult(new { success = "Success" });
            }
            else
            {
                return new JsonResult(new { success = "Failure" });
            }
        }
        

        /// Delete the key  
        /// </summary>  
        /// <param name="key">Key</param>  
        public void RemoveCookie(string key)
        {
            Response.Cookies.Delete(key);
        }


    }
}
