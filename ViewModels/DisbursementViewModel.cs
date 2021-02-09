using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory_Management_System.Models;

namespace Inventory_Management_System.ViewModels
{
    public class DisbursementViewModel
    {
        public Employee employee { get; set; }
        public List<Product> productList { get; set; }
        public DisbursementForm disbursementForm { get; set; }
        public List<DisbursementFormRequisitionForm> DisbursementFormRequisitionForms { get; set; }
        public List<DisbursementFormProduct> DisbursementFormProducts { get; set; }
        public List<DisbursementFormRequisitionFormProduct> DisbursementFormRequisitionFormProducts { get; set; }

        public List<DisbursementForm> dFCreatedList { get; set; }
        public List<DisbursementForm> dfPendingDeliveryList { get; set; }
        public List<DisbursementForm> dfPendingAssignList { get; set; }

        public List<DisbursementForm> dfCompletedList { get; set; }
        public List<StationeryRetrievalRequisitionForm> srrfAssignedList { get; set; }
        public string comment { get; set; }
        public Employee deptrep { get; set; }
        public Employee storeclerk { get; set; }

    }
}
