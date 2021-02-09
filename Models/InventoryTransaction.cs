using Inventory_Management_System.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class InventoryTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //[ForeignKey("Employee")]
        [NotMapped]
        public int EmployeeId { get; set; }
        [ForeignKey("Product")]
        [NotMapped]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public ITStatus ITStatus { get; set; }
        [Required]
        public int InventoryChangeQuantity { get; set; }
        [Required]
        public string InventoryTransComments { get; set; }
        public DateTime InventoryTransDate { get; set; }
        [Required]
        public string ITCode { get; set; }
        //[NotMapped]
        public virtual Employee Employee { get; set; }
        public string ApprovalComment { get; set; }
        [NotMapped]
        public float TotalValue { get; set; }
        public DateTime ITApprovalDate { get; set; }
        public virtual Employee ITApprovalBy { get; set; }
        public float ProductPrice { get; set; }

    }
}
