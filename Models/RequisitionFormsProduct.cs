using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class RequisitionFormsProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public virtual RequisitionForm RequisitionForm { get; set; }
        [Required]
        public virtual Product Product { get; set; }
        [Required]
        public int ProductRequested { get; set; }
        public int ProductApproved { get; set; }
        public int ProductCollectedTotal { get; set; }
        [NotMapped]
        public int ProductBalanceForSR { get; set; }
    }
}
