using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory_Management_System.Models;
using Inventory_Management_System.Services;
using Inventory_Management_System.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace Inventory_Management_System.Controllers
{
    [Authorize]
    public class PODOController : Controller
    {
        private Employee emp;
        private DeliveryOrderService doService;
        private PurchaseDeliveryProductService pdpService;
        private ProductService pService;
        private SupplierProductService spService;
        private SupplierService supService;
        private DeliveryOrderSupplierProductService dospService;
        private PurchaseOrderService poService;

        public PODOController(DeliveryOrderService doService, PurchaseDeliveryProductService pdpService, PurchaseOrderService poService,
             ProductService pService, SupplierProductService spService, SupplierService supService, DeliveryOrderSupplierProductService dospService)
        {
            this.doService = doService;
            this.poService = poService;
            this.pdpService = pdpService;
            this.pService = pService;
            this.spService = spService;
            this.supService = supService;
            this.dospService = dospService;
        }
        public IActionResult Index()
        {
            return View();
        }

               

        [Route("podo/summary")]
        public IActionResult POSummary()
        {
            emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            PODOViewModel pdVModel = new PODOViewModel();
            pdVModel.poListIssued = poService.FindAllIssuedPOs();
            pdVModel.poListCompleted = poService.FindAllPOsCompleted();
            pdVModel.poListNotCompleted = poService.FindAllPOsNotCompleted();
            pdVModel.doListNotCompleted = doService.FindAllDOsCreated();
            pdVModel.doListCompleted = doService.FindAllCompletedDOs();

            return View("PODOSummary", pdVModel);
        }

        [Route("po/create")]
        [Authorize]
        public IActionResult CreatePurchaseOrder()
        {
            emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            PODOViewModel pdVModel = new PODOViewModel();
            pdVModel.supplierList = supService.FindSupplierList();
            pdVModel.Employee = emp;
            pdVModel.productList = pService.FindProducts();

            return View("PurchaseOrderFormCreate", pdVModel);
        }

        [Route("po/save")]
        [Authorize]
        public IActionResult Save([FromBody] PODOViewModel pdVModel)
        {
            bool status = poService.CreatePurchaseOrder(pdVModel.Employee, pdVModel.comment, pdVModel.productList);
            if (status)
            {
                return new JsonResult(new { success = "Success" });
            }
            else
            {
                return new JsonResult(new { success = "Failure" });
            }
        }

        [Route("po/{id}")]
        [Authorize]
        public IActionResult ViewPurchaseOrder([FromRoute] int id)
        {
            emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            PODOViewModel pdVModel = new PODOViewModel();
            pdVModel.Employee = emp;
            pdVModel.posrList = poService.FindPOSRByPOId(id);
            pdVModel.PO = poService.FindPO(id);
            pdVModel.doList = doService.FindDOListByPOID(id);
            pdVModel.dosrList = dospService.FindDOSRByPOId(id);

            return View("PurchaseOrderFormView", pdVModel);
        }

        [Route("do/create")]
        [Authorize]
        public IActionResult CreateDeliveryOrder()
        {
            emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            PODOViewModel pdVModel = new PODOViewModel();
            pdVModel.Employee = emp;
            pdVModel.poListIssued = poService.FindPOListThatIsNotCompleted();

            return View("DeliveryOrderFormCreate", pdVModel);
        }
        [Route("do/save")]
        [Authorize]
        public IActionResult SaveDeliveryOrder([FromQuery] int poid, string DoComments)
        {
            emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            doService.CreateDeliveryOrder(poid, DoComments, emp.Id);
            return RedirectToAction("UpdateDeliveryOrder", new { poid = poid });
        }

        [Route("do/update/{id}")]
        public IActionResult UpdateDeliveryOrder2([FromRoute] int id)
        {

            PODOViewModel pdVModel = new PODOViewModel();
            emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            pdVModel.Employee = emp;
            pdVModel.DO = doService.FindDOByPOIdWithDOStatusCreated(id);
            pdVModel.dosrList = dospService.FindBalanceValuestoInput(doService.FindDOByPOIdWithDOStatusCreated(id).Id);

            return View("DeliveryOrderFormUpdate",pdVModel);
        }

        public IActionResult UpdateDeliveryOrder(int poid)
        {

            PODOViewModel pdVModel = new PODOViewModel();
            emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            pdVModel.Employee = emp;
            pdVModel.DO = doService.FindDOByPOIdWithDOStatusCreated(poid);
            pdVModel.dosrList = dospService.FindBalanceValuestoInput(doService.FindDOByPOIdWithDOStatusCreated(poid).Id);

            return View("DeliveryOrderFormUpdate", pdVModel);
        }

        [HttpPost("do/save/{id}")]
        [Authorize]
        public IActionResult UpdateDOQuantityAssignment([FromBody] PODOViewModel pdVModel)
        {
            bool status = doService.UpdateDeliveryOrder(pdVModel.dosrList, pdVModel.Employee.Id, pdVModel.DO);
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