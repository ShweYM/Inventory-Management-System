using Inventory_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.ViewModels
{
    public class RequisitionSummaryViewModel
    {
        public List<RequisitionForm> rfListPending { get; set; }
        public List<RequisitionForm> rfListProcessed { get; set; }
        public Employee employee { get; set; }
    }
}
