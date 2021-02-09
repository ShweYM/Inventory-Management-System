using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class DeliveryOrderSupplierProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual DeliveryOrder DeliveryOrder { get; set; }
        public int DOQuantityReceived { get; set; }
        public virtual PurchaseOrderSupplierProduct PurchaseOrderSupplierProduct { get; set; }
    }
}
