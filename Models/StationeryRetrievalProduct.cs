using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class StationeryRetrievalProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual StationeryRetrieval StationeryRetrieval { get; set; }
        public virtual Product Product { get; set; }
        public int ProductRequestedTotal { get; set; }
        public int ProductReceivedTotal { get; set; }
        [NotMapped]
        public int ProductCount { get; set; }
        [NotMapped]
        public int ProductApprovedCount { get; set; }

    }
}
