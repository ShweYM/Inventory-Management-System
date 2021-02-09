using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class DisbursementFormProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual DisbursementForm DisbursementForm { get; set; }
        public virtual Product Product { get; set; }
        public int ProductToDeliverTotal { get; set; }
        public int ProductDeliveredTotal { get; set; }
        [NotMapped]
        public int ProductCount { get; set; }
        [NotMapped]
        public int ProductApprovedCount { get; set; }
    }
}
