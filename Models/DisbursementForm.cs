using Inventory_Management_System.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class DisbursementForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual Employee StoreClerk { get; set; }
        public virtual Employee DeptRep { get; set; }
        public DFStatus DFStatus { get; set; }
        public DateTime DFHandoverDate { get; set; }
        public DateTime DFDeliveryDate { get; set; }
        public DateTime DFDate { get; set; }
        public string DFComments { get; set; }
        public virtual CollectionPoint CollectionPoint { get; set; }
        public string DFCode { get; set; }
        public DateTime DFTransAssignDate { get; set; }
        public virtual Employee DFAssignedBy { get; set; }

    }
}
