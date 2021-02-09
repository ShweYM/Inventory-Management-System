using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class CSVSupplier
    {
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string ContactName { get; set; }

        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string Address { get; set; }
        public string GSTregistrationNumber { get; set; }
        public string SupplierEmail { get; set; }


    }
}
