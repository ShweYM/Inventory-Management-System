using Inventory_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.ViewModels
{
    public class DashboardViewModel
    {
        public Employee emp { get; set; }
        public Department dept { get; set; }
        public Employee deptRep { get; set; }

        public List<CollectionPoint> cpList { get; set; }
        public CollectionPoint cpUpdated { get; set; }

        //By Dept Head - Total Req For this Month
        public int RFsThisMonth { get; set; }
        //By Dept Head - Total RF Yet to Approve
        public int PendingRFApproval { get; set; }
        public int PendingRFDelivery { get; set; }
        public int TotalReqSubmitted { get; set; }
        public int PendingApprovedRFByEmployee { get; set; }
        public int PendingRFToDeliver { get; set; }
        public int PendingIAApprovals { get; set; }
        public int PendingRFToAssignToSR { get; set; }
        public int PendingRFsForCollection { get; set; }

        public int TotalRFSubmitted { get; set; }
        public int TotalRFApproved { get; set; }
        public int TotalRFRejected { get; set; }
        public int TotalRFNotCompleted { get; set; }
        public int TotalRFCompleted { get; set; }
        public int TotalRFOngoing { get; set; }

        public int TotalRFSubmittedDept { get; set; }
        public int TotalRFApprovedDept { get; set; }
        public int TotalRFRejectedDept { get; set; }
        public int TotalRFNotCompletedDept { get; set; }
        public int TotalRFCompletedDept { get; set; }
        public int TotalRFOngoingDept { get; set; }




        public int TotalSROpen{ get; set; }
        public int TotalSRPendingAssignment{ get; set; }
        public int TotalSRAssigned { get; set; }

        public int TotalDFCreated { get; set; }
        public int TotalDFPendingDelivery { get; set; }
        public int TotalDFPendingAssignment { get; set; }
        public int TotalDFCompleted { get; set; }

        
        public int TotalPOIssued { get; set; }
        public int TotalPONotCompleted { get; set; }
        public int TotalPOCompleted { get; set; }

        public int TotalITPendingApproval { get; set; }
        public int TotalITApproved { get; set; }



    }
}
