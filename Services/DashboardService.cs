using Inventory_Management_System.DB;
using Inventory_Management_System.Enums;
using Inventory_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Services
{
    public class DashboardService
    {
        private InventoryManagementSystemContext db;

        public DashboardService(InventoryManagementSystemContext db)
        {
            this.db = db;
        }

        public int FindCountForRFThisMonth(Employee emp)
        {
            return db.RequisitionForms
                .Where(x => x.RFDate.Month == DateTime.Now.Month)
                .Where(x => x.Employee.Department == emp.Department)
                .Count();
        }

        

        public int FindCountRFToApproveByDepartment(Employee emp)
        {
            return db.RequisitionForms
                .Where(x => x.Employee.Department == emp.Department)
                .Where(x => x.RFStatus == Enums.RFStatus.Submitted)
                .Count();
        }
        public int FindCountRFToDeliverByDepartmentForDeptHead(Employee emp)
        {
            return db.DisbursementFormRequisitionForms
                .Where(x => x.RequisitionForm.Employee.Department == emp.Department)
                .Where(x => x.DFRFStatus == Enums.DFRFStatus.PendingDelivery)
                .Count();
        }


        public int TotalRFSubmittedByEmployee(Employee emp)
        {
            return db.RequisitionForms
                .Where(x => x.Employee == emp)
                .Where(x => x.RFStatus == RFStatus.Submitted)
                .Count();
        }
        public int TotalRFNosByEmployee(Employee emp, RFStatus rFStatus)
        {
            return db.RequisitionForms
                .Where(x => x.Employee == emp)
                .Where(x => x.RFStatus == rFStatus)
                .Count();
        }
        public int TotalRFNosByDept(Employee emp, RFStatus rFStatus)
        {
            return db.RequisitionForms
                .Where(x => x.Employee.Department == emp.Department)
                .Where(x => x.RFStatus == rFStatus)
                .Count();
        }
        public int TotalDFNosByDept(Employee emp, DFStatus dFStatus)
        {
            return db.DisbursementForms
                .Where(x => x.DeptRep.Department == emp.Department)
                .Where(x => x.DFStatus == dFStatus)
                .Count();
        }
        public int TotalDFNos(Employee emp, DFStatus dFStatus)
        {
            return db.DisbursementForms
                .Where(x => x.DFStatus == dFStatus)
                .Count();
        }
        public int TotalSRNos(Employee emp, SRStatus sRStatus)
        {
            return db.StationeryRetrievals
                .Where(x => x.SRStatus == sRStatus)
                .Count();
        }
        public int TotalRFNos(Employee emp, RFStatus rFStatus)
        {
            return db.RequisitionForms
                .Where(x => x.RFStatus == rFStatus)
                .Count();
        }
        public int TotalITNos(Employee emp, ITStatus iTStatus)
        {
            return db.InventoryTransactions
                .Where(x => x.ITStatus == iTStatus)
                .Count();
        }

        public int FindCountRFApprovalByEmployee(Employee emp)
        {
            return db.RequisitionForms
                .Where(x => x.Employee == emp)
                .Where(x => x.RFStatus == RFStatus.Submitted)
                .Count();
        }

        public int FindCountRFApprovedToBeAssignedToSR()
        {
            return db.RequisitionForms
                .Where(x => x.RFStatus == Enums.RFStatus.Approved)
                .Count();
        }

        public int FindCountRFThatWasntCompleted()
        {
            return db.RequisitionForms
                .Where(x => x.RFStatus == Enums.RFStatus.NotCompleted)
                .Count();
        }

        public int FindCountRFDeliveryByEmployee(Employee emp)
        {
            return db.DisbursementFormRequisitionForms
                .Where(x => x.RequisitionForm.Employee == emp)
                .Where(x => x.DFRFStatus == Enums.DFRFStatus.PendingDelivery)
                .Count();
        }
        
        public int FindCountSRToRetrieveFromWareHouseToSR()
        {
            return db.StationeryRetrievalRequisitionForms
                .Where(x => x.SRRFStatus == Enums.SRRFStatus.Open)
                .Count();
        }

        public int FindCountDFDelivery()
        {
            return db.DisbursementForms
                .Where(x => x.DFStatus == Enums.DFStatus.Created)
                .Count();
        }

    }
}
