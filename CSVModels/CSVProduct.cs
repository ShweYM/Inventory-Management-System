using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class CSVProduct
    {
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string  ProductCategory { get; set; }
        public string ProductDescription { get; set; }
        public string Units { get; set; }
        public int ReorderLevel { get; set; }
        public int ReorderQuantity { get; set; }
        public string InventoryLocation { get; set; }
        public int InventoryQuantity { get; set; }
        public int MLReorderQuantity { get; set; }
        public int MLReorderLevel { get; set; }


    }
}
