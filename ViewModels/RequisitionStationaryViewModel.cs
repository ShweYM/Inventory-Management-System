using Inventory_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.ViewModels
{
    public class RequisitionStationaryViewModel
    {
        public List<RequisitionForm> requisition { get; set; }

        public IDictionary<string,List<int>> reqAndWareHouseproduct { get; set; }

        public List<SRProductViewModel> SRProduct { get; set; }
        public string comment { get; set; }

        public List<int> selectedrequisition { get; set; }

        public List<RequisitionForm> rfList { get; set; }

        public List<RequisitionFormsProduct> rfpConsolidatedList { get; set; }
        public List<Product> prodList { get; set; }
        public List<StationeryRetrievalProduct> srpList { get; set; }
        public Employee emp { get; set; }





    }
}
