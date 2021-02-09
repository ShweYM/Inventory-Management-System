using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class CSVRequisitionFormProduct
    {
        public string RFCode { get; set; }
        public string Product { get; set; }
        public int ProductRequested { get; set; }
        public int ProductApproved { get; set; }
        public int ProductCollectedTotal { get; set; }

    }
}
