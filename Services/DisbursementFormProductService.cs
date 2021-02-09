using Inventory_Management_System.DB;
using Inventory_Management_System.Models;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Services
{
    public class DisbursementFormProductService
    {
        protected InventoryManagementSystemContext db;

        public DisbursementFormProductService(InventoryManagementSystemContext db)
        {
            this.db = db;
        }

        public List<DisbursementFormProduct> FindDisbursementFormProducts()
        {
            return db.DisbursementFormProducts.ToList();
        }
        
        public List<DisbursementFormProduct> FindDisbursementFormProductsByDFId(int id)
        {
            //Find DF
            DisbursementForm df = db.DisbursementForms.Find(id);
            //Find DFP
            List<DisbursementFormProduct> dfpList = db.DisbursementFormProducts
                .OrderBy(x=>x.Product.ProductName)
                .Where(x => x.DisbursementForm == df)
                .ToList();
            //Find DFRF
            List<DisbursementFormRequisitionForm> dfrfList = db.DisbursementFormRequisitionForms
                .Where(x => x.DisbursementForm == df).ToList();
            IEnumerable<RequisitionForm> rfList = dfrfList.Select(x => x.RequisitionForm).Distinct();

            //Call consolidated RFP
            List<RequisitionFormsProduct> _rfpConsolidatedList = new List<RequisitionFormsProduct>();

            //Find consolidated list of rfps
            foreach (RequisitionForm rf in rfList)
            {
                List<RequisitionFormsProduct> rfpList = db.RequisitionFormsProducts
                    .Where(x => x.RequisitionForm == rf)
                    .ToList();
                foreach (RequisitionFormsProduct rfp in rfpList)
                {
                    _rfpConsolidatedList.Add(rfp);
                }
            }

            foreach (DisbursementFormProduct dfp in dfpList)
            {
                dfp.ProductApprovedCount = 0;
                foreach (RequisitionFormsProduct _rfpConsolidated in _rfpConsolidatedList)
                {
                    if (dfp.Product.Id == _rfpConsolidated.Product.Id)
                    {
                        dfp.ProductApprovedCount = dfp.ProductApprovedCount + _rfpConsolidated.ProductApproved;
                    }
                }
            }
            return dfpList;
        }
        public List<DisbursementFormProduct> FindDisbursementFormProductsByDFId(int id, Employee emp)
        {
            return db.DisbursementFormProducts
                .Where(x => x.DisbursementForm.Id == id)
                .Where(x=>x.DisbursementForm.DeptRep.Department == emp.Department)
                .ToList();
        }

        public List<DisbursementFormProduct> FindDisbursementFormProductsByDFCode(String dfCode)
        {
            return db.DisbursementFormProducts
                .Where(x => x.DisbursementForm.DFCode == dfCode)
                .ToList();
        }
    }
}
