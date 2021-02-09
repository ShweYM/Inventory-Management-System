using Inventory_Management_System.Enums;
using Inventory_Management_System.Models;
using System.Collections.Generic;

namespace Inventory_Management_System.ViewModels
{
    public class RequisitionViewModel
    {
        public Employee employee { get; set; }
        public List<Product> productList { get; set; }
        public RequisitionForm requisitionForm { get; set; }
        public List<DisbursementFormRequisitionFormProduct> dfrfpList { get; set; }
        public List<RequisitionFormsProduct> rfpList { get; set; }
        public string Comment { get; set; }
        public string RFHeadReply { get; set; }
        public List<DisbursementFormRequisitionForm> dfrfList { get; set; }
    }
}
