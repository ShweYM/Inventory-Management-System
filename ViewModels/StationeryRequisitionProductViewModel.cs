using Inventory_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.ViewModels
{
    public class StationeryRequisitionProductViewModel
    {
        public string username { get; set; }
        public string comment { get; set; }
        public List<StationeryRetrievalProduct> spvm { get; set; }
        public List<int> requisitionIdList { get; set; }
    }
}
