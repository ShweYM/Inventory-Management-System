using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory_Management_System.DB;
using Inventory_Management_System.Models;

namespace Inventory_Management_System.Services
{
    public class StationeryRetrievalRequisitionFormService
    {
        private InventoryManagementSystemContext db;

        public StationeryRetrievalRequisitionFormService(InventoryManagementSystemContext db)
        {
            this.db = db;
        }

        public List<StationeryRetrievalRequisitionForm> FindStationeryRetrievalRequisitionFormByStatusOrderByDept()
        {
            return db.StationeryRetrievalRequisitionForms
                .OrderBy(x=>x.RequisitionForm.Employee.Department)
                .OrderBy(x=>x.RequisitionForm.RFDate)
                .Where(x=>x.SRRFStatus == Enums.SRRFStatus.Assigned)
                .ToList();
        }

        public List<StationeryRetrievalRequisitionForm> FindStationeryRetrievalRequisitionFormByCompletedStatusOrderByDept()
        {
            return db.StationeryRetrievalRequisitionForms
                .OrderBy(x => x.RequisitionForm.Employee.Department)
                .OrderBy(x => x.RequisitionForm.RFDate)
                .Where(x => x.SRRFStatus == Enums.SRRFStatus.Completed)
                .ToList();
        }

    }
}
