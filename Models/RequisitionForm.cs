using Inventory_Management_System.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class RequisitionForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual Employee Employee { get; set; }
        public string RFCode { get; set; }
        public RFStatus RFStatus { get; set; }
        public string RFComments { get; set; }
        public DateTime RFDate { get; set; }
        [AllowNull]
        public DateTime RFApprovalDate { get; set; }
        [AllowNull]
        public virtual Employee RFApprovalBy { get; set; }
        public string RFHeadReply { get; set; }
    }
}
