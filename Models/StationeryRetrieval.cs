using Inventory_Management_System.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class StationeryRetrieval
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public virtual Employee StoreClerk { get; set; }
        public virtual Employee WarehousePacker { get; set; }
        public string SRCode { get; set; } 
        public SRStatus SRStatus { get; set; }
        public string SRComments { get; set; }
        public DateTime SRDate { get; set; }
        public DateTime SRRetrievalDate { get; set; }
        public DateTime SRAssignedDate { get; set; }
    }
}

