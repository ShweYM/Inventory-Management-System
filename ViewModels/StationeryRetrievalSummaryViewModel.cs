using Inventory_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.ViewModels
{
    public class StationeryRetrievalSummaryViewModel
    {
        public Employee Employee { get; set; }

        public List<StationeryRetrieval> pendingSROpens { get; set; }
        public List<StationeryRetrieval> pendingSRAssignments { get; set; }
        public List<StationeryRetrieval> completedSRs { get; set; }

    }
}
