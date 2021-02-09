using Inventory_Management_System.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class StationeryRetrievalRequisitionForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual StationeryRetrieval StationeryRetrieval { get; set; }
        public virtual RequisitionForm RequisitionForm { get; set; }
        public SRRFStatus SRRFStatus { get; set; }
    }
}
