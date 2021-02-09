using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class DisbursementFormRequisitionFormProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual DisbursementForm DisbursementForm { get; set; }
        public virtual RequisitionFormsProduct RequisitionFormsProduct { get; set; }
        public int ProductCollected { get; set; }
    }
}
