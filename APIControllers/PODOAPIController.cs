using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory_Management_System.Models;
using Inventory_Management_System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management_System.APIControllers
{
    [Route("api/podo")]
    [ApiController]
    public class PODOAPIController : ControllerBase
    {
        PurchaseOrderService poService;
        DeliveryOrderService doService;
        public PODOAPIController(PurchaseOrderService poService, DeliveryOrderService doService)
        {
            this.poService = poService;
            this.doService = doService;
        }

        [Route("GetPurchaseOrderList")]
        [HttpGet]
        public IActionResult GetPurchaseOrderList()
        {
            List<PurchaseOrder> poList = poService.FindPurchaseOrders();
            if (poList != null)
                return Ok(poList);
            else
                return null;
        }

        [Route("GetDeliveryOrderList")]
        [HttpGet]

        public IActionResult GetDeliveryOrderList()
        {
            List<DeliveryOrder> doList = doService.FindDeliveryOrders();
            return Ok(doList);
        }


    }
}
