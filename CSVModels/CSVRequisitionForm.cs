using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class CSVRequisitionForm
    {
        public string Username { get; set; }
        public string RFCode { get; set; }
        public string RFDate { get; set; }
        public string RFStatus { get; set; }
        public string RFComments { get; set; }
        public string RFApprovalDate { get; set; }
        public string RFApprovalUsername { get; set; }
        public string RFHeadReply { get; set; }


    }
}
