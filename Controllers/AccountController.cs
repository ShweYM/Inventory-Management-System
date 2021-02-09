using Inventory_Management_System.Models;
using Inventory_Management_System.Services;
using Inventory_Management_System.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Inventory_Management_System.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private EmployeeService empService;
        private CollectionPointService cpService;
        private Employee emp;

        public AccountController(ILogger<AccountController> logger, EmployeeService empService, CollectionPointService cpService)
        {
            _logger = logger;
            this.empService = empService;
            this.cpService = cpService;
        }

        //Redirect to a display page for a user's account 

        public IActionResult Index()
        {
            emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            AccountViewModel aVModel = new AccountViewModel();
            aVModel.employee = emp;
            aVModel.cList = cpService.GetCPList();

            //int idCheck = (int)HttpContext.Session.GetInt32("id");
            //if (HttpContext.Session.GetInt32("id") != null)
            //{
            //    Employee empCheck = empService.GetEmployeeById(idCheck);
            //    if (empCheck != null)
            //    {
            //        System.Diagnostics.Debug.WriteLine("This is inside view Id : " + empCheck.Id);
            //        return View("Index", empCheck);
            //    }
            //}

            return View("AccountSummary", aVModel);
        }

        //[Route("acc/save")]
        //public IActionResult Save(string email, string phonenum)
        //{
        //    //System.Diagnostics.Debug.WriteLine("I am here");
        //    //return RedirectToAction("Dashboard", "Home");
        //    if (email != null || phonenum != null)
        //    {


        //        Debug.WriteLine(email);
        //        return new JsonResult(new { success = "Success" });
        //        //return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        return new JsonResult(new { success = "Fail" });
        //    }
        //}

        [Route("acc/save")]
        public IActionResult SaveAcc([FromBody] AccountViewModel aVModel)
        {
            bool status = empService.UpdateEmployeeDetails(aVModel.employee, aVModel.collectionPoint);
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
