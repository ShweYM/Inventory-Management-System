using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory_Management_System.Models;
using Inventory_Management_System.ViewModels;

namespace Inventory_Management_System.ViewModels
{
    public class StationeryRetrievalViewModel
    {
        public Employee employee { get; set; }
        public StationeryRetrieval stationeryRetrieval { get; set; }
        public List<RequisitionForm> retrievalRequisitions { get; set; }
        public List<StationeryRetrievalProduct> retrievalProducts { get; set; }
        public List<LoginViewModel> users { get; set; }
        public List<StationeryRetrievalRequisitionFormProduct> sRRFPList { get; set; }
        public Employee warehousepacker { get; set; }
        public Employee storeclerk { get; set; }
        public List<StationeryRetrievalRequisitionForm> srrfList { get; set; }
        


    }
}
