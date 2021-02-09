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
    public class InventoryController : Controller
    {
        private readonly ILogger<InventoryController> _logger;
        InventoryTransactionService invtransService;
        private ProductService pService;
        private InventoryTransactionService itService;
        private Employee emp;

        public InventoryController(ILogger<InventoryController> logger, InventoryTransactionService invtransService, ProductService pService, InventoryTransactionService itService)
        {
            _logger = logger;
            this.invtransService = invtransService;
            this.pService = pService;
            this.itService = itService;
        }

        [Route("inventory/summary")]
        [Authorize]
        public IActionResult ListInventorySummary()
        {
            InventoryTransactionViewModel invVModel = new InventoryTransactionViewModel();
            invVModel.ProductList = pService.FindProducts();
            return View("InventorySummary", invVModel);
        }

        [Route("inventory/transactionsummary")]
        [Authorize]
        public IActionResult ListInventoryTransactionSummary()
        {
            emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            InventoryTransactionViewModel itVModel = new InventoryTransactionViewModel();

            itVModel.invTransList = invtransService.ListInventoryTransaction();
            if (emp.EmployeeType.EmployeeTypeName == "Store Supervisor")
            {
                itVModel.invPendApprList = invtransService.ListITWithPendingApprovalLessThan250();
            }
            else
            {
                itVModel.invPendApprList = invtransService.ListITWithPendingApprovalMoreThanOrEqual250();
            }
            itVModel.employee = emp;

            return View("InventoryTransactionSummary", itVModel);
        }
        [Route("inventory/transaction/create")]
        public IActionResult CreateInventoryTransactions()
        {
            InventoryTransactionViewModel itVModel = new InventoryTransactionViewModel();

            int idCheck = (int)HttpContext.Session.GetInt32("id");
            if (HttpContext.Session.GetInt32("id") != null)
            {
                emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
                if (emp != null)
                {
                    itVModel.employee = emp;
                }
            }
            List<Product> products = pService.FindProducts();
            itVModel.ProductList = products;
            return View("InventoryTransaction", itVModel);
        }

        [Route("inventory/transaction/save")]
        public IActionResult Save([FromBody] InventoryTransactionViewModel itVModel)
        {
            bool status = invtransService.CreateTransactionsManual2(itVModel.ProductList,itVModel.invQuantityTransList, itVModel.employee, itVModel.comments);

            if (status is true)
            {
                return new JsonResult(new { success = "Success" });
            }
            else
            {
                return new JsonResult(new { success = "Failure" });
            }
        }
        [Route("inventory/manage")]
        public IActionResult ManageInventory()
        {
            InventoryTransactionViewModel invVModel = new InventoryTransactionViewModel();
            invVModel.ProductList = pService.FindProducts();
            return View("InventorySummaryUpdate", invVModel);
        }

        [Route("inventory/update")]
        public IActionResult UpdateInventoryLevels([FromBody] InventoryTransactionViewModel itVModel)
        {
            //bool status = invtransService.UpdateInventoryLevels(itVModel.ProductList, itVModel.invQuantityTransList, itVModel.employee);
            bool status = true;
            if (status is true)
            {
                return new JsonResult(new { success = "Success" });
            }
            else
            {
                return new JsonResult(new { success = "Failure" });
            }
        }
        [Route("it/approval/{id}")]
        public IActionResult InventoryTransactionApproval([FromRoute] int id)
        {
            InventoryTransactionViewModel itVModel = new InventoryTransactionViewModel();
            emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            itVModel.employee = emp;
            itVModel.it = itService.FindInventoryTransaction(id);

            return View("InventoryTransactionApprove", itVModel);
        }
        [HttpPost]
        [Route("it/approve")]
        public IActionResult ApproveInventoryTransaction([FromBody] InventoryTransactionViewModel itVModel)
        {
            bool status = itService.ApproveInventoryApproval(itVModel.it, itVModel.employee);
            if (status)
            {
                return new JsonResult(new { success = "Success" });
            }
            else
            {
                return new JsonResult(new { success = "Failure" });
            }
        }

        [HttpPost("it/reject/")]
        public IActionResult RejectInventoryTransaction([FromBody] InventoryTransactionViewModel itVModel)
        {
            bool status = itService.RejectInventoryApproval(itVModel.it, itVModel.employee);
            if (status)
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

