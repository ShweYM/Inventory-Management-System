using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inventory_Management_System.Services;
using Inventory_Management_System.Models;

namespace Inventory_Management_System.APIControllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        ProductService pservice;
        public ProductAPIController(ProductService pservice)
        {
            this.pservice = pservice;
        }

        [Route("GetProductList")]
        [HttpGet]
        public IActionResult GetProductList()
        {
            List<Product> productlist = pservice.FindProducts();
            return Ok(productlist);
        }
    }
}