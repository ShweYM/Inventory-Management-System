using Inventory_Management_System.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class DeliveryOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string DeliveryOrderNo { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public string DOComments { get; set; }
        public DateTime DOReceivedDate { get; set; }
        //public DateTime DOActualTime{ get; set; }
        public virtual Employee ReceivedBy { get; set; }
        public string DOCode { get; set; }
        public DOStatus DOStatus { get; set; }



    }
}
