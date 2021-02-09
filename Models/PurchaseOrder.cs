using Inventory_Management_System.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class PurchaseOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual Supplier supplier { get; set; }
        public String DeliverTo { get; set; }
        public DateTime expectedDeliveryDate { get; set; }
        public virtual Employee IssuedBy { get; set; }
        public POStatus  POStatus { get; set; }
        public string POComments { get; set; }
        public DateTime POIssueDate { get; set; }
        public string POCode { get; set; }



    }
}
