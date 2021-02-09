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
    public class DepartmentController : Controller
    {

        private DepartmentService dService;
        private readonly ILogger<DepartmentController> _logger;
        private Employee emp;

        public DepartmentController(ILogger<DepartmentController> logger, DepartmentService dService)
        {
            _logger = logger;
            this.dService = dService;
        }
        [Route("department")]
        public IActionResult FindDepartmentForm()
        {
            emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;

            DepartmentViewModel dVModel = new DepartmentViewModel();
            dVModel.employee = emp;
            dVModel.deptList = dService.FindDepartmentList();

            return View("DepartmentSummary", dVModel);
        }
    }
}
