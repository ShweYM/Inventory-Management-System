using Inventory_Management_System.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class SupplierProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Product Product { get; set; }
        public float ProductPrice { get; set; }
        public SPAvailStatus SPAvailStatus { get; set; }
        public int PriorityLevel { get; set; }
    }
}
