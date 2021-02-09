using Inventory_Management_System.Models;
using Inventory_Management_System.Services;
using Inventory_Management_System.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace Inventory_Management_System.Controllers
{
    public class InventoryTransactivonController : Controller
    {
        private readonly ILogger<InventoryTransactivonController> _logger;
        private InventoryTransactionService itService;

        public InventoryTransactivonController(ILogger<InventoryTransactivonController> logger, InventoryTransactionService itService)
        {
            _logger = logger;
            this.itService = itService;
        }

        
        

    }
    
}
