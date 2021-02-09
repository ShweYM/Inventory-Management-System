using Inventory_Management_System.Services;
using Inventory_Management_System.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Inventory_Management_System.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private ProductService pService;
        private readonly ILogger<ProductController> _logger;


        public ProductController(ILogger<ProductController> logger, ProductService pService)
        {
            _logger = logger;
            this.pService = pService;
        }
        public IActionResult Index()
        {
            return View();
        }

        //show all products //by htp
        [Route("product/catalogue")]
        public IActionResult ProductCatalogue()
        {
            ViewData["products"] = pService.FindProducts();
            return View();
        }


        //show product form with details //by htp
        public IActionResult ProductForm(int Id)
        {
            ProductDetailViewModel prodDetailVM = new ProductDetailViewModel();

            prodDetailVM.product = pService.FindProductById(Id);

            return View("ProductForm", prodDetailVM);
        }
    }
}
