using Inventory_Management_System.Models;
using Inventory_Management_System.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.APIControllers
{
    [Route("api/Supplier")]
    [ApiController]
    public class SupplierAPIController : ControllerBase
    {
        SupplierService supplierService;
        public SupplierAPIController(SupplierService supplierService)
        {
            this.supplierService = supplierService;
        }

        [Route("GetSupplierList")]
        [HttpGet]
        public IActionResult GetSupplierList()
        {
            List<Supplier> supplierlist = supplierService.FindSuppliers();
            return Ok(supplierlist);
        }
    }
}
