using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class CSVSupplierProduct
    {
        public string SupplierName { get; set; }
        public string ProductCode { get; set; }
        public float ProductPrice { get; set; }
        public string SPAvailStatus { get; set; }
        public int PriorityLevel { get; set; }


    }
}
