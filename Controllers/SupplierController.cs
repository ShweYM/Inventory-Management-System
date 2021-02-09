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
    public class SupplierController : Controller
    {
        private SupplierService sService;
        private readonly ILogger<SupplierController> _logger;
        private Employee emp;

        public SupplierController(ILogger<SupplierController> logger, SupplierService sService)
        {
            _logger = logger;
            this.sService = sService;
        }
        [Route("supplier/summary")]

        public IActionResult FindSupplierForm()
        {
            emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;

            SupplierViewModel sVModel = new SupplierViewModel();
            sVModel.employee = emp;
            sVModel.supList = sService.FindSupplierList();
            return View("SupplierSummary", sVModel);
        }
    }
}
