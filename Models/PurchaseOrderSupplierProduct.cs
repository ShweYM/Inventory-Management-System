using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class PurchaseOrderSupplierProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual SupplierProduct SupplierProduct { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public float POUnitPrice { get; set; }
        public int POQuantityRequested { get; set; }
    }
}
