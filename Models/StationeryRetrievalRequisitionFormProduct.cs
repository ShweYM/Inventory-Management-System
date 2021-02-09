using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class StationeryRetrievalRequisitionFormProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int ProductAssigned { get; set; }
        public virtual StationeryRetrieval SR { get; set; }
        public virtual RequisitionFormsProduct RFP { get; set; }

    }
}
