using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductCode { get; set; }
        [Required]
        public virtual ProductCategory ProductCategory { get; set; }
        public string ProductDescription { get; set; }
        public string Units { get; set; }
        public string ProductImage { get; set; }
        [NotMapped]
        public int ProductRequested { get; set; }
        public int InventoryQuantity { get; set; }
        public int ReorderLevel { get; set; }
        public int ReorderQuantity { get; set; }
        public string InventoryLocation { get; set; }
        public int MLReorderQuantity { get; set; }
        public int MLReorderLevel { get; set; }
        [NotMapped]
        public int ProductCountCheck { get; set; }



    }
}
