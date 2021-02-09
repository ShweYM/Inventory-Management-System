using Inventory_Management_System.DB;
using Inventory_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Services
{
    public class DisbursementFormRequisitionFormService
    {
        protected InventoryManagementSystemContext db;

        public DisbursementFormRequisitionFormService(InventoryManagementSystemContext db)
        {
            this.db = db;
        }

        public List<DisbursementFormRequisitionForm> FindDisbursementFormRequisitionForms()
        {
            return db.DisbursementFormRequisitionForms.ToList();
        }

        public List<DisbursementFormRequisitionForm> FindDisbursementFormRequisitionFormsByDFCode(String dfCode)
        {
            return db.DisbursementFormRequisitionForms.Where(x => x.DisbursementForm.DFCode == dfCode).ToList();
        }
    }
}
