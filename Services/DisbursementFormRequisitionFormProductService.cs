using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory_Management_System.DB;
using Inventory_Management_System.Models;

namespace Inventory_Management_System.Services
{
    public class DisbursementFormRequisitionFormProductService
    {
        protected InventoryManagementSystemContext db;

        public DisbursementFormRequisitionFormProductService(InventoryManagementSystemContext db)
        {
            this.db = db;
        }

        public List<DisbursementFormRequisitionFormProduct> FindDisbursementFormRequisitionFormProducts()
        {
            return db.DisbursementFormRequisitionFormProducts.ToList();
        }
        public List<DisbursementFormRequisitionFormProduct> FindDisbursementFormRequisitionFormProductsByDFId(int id)
        {
            List<DisbursementFormRequisitionFormProduct> dfrfpList = db.DisbursementFormRequisitionFormProducts
                .Where(x => x.DisbursementForm.Id == id).ToList();
            return dfrfpList;
        }
        public List<DisbursementFormRequisitionFormProduct> FindDFRFPByDFId(int id, Employee emp)
        {
            return db.DisbursementFormRequisitionFormProducts
                .Where(x => x.DisbursementForm.Id == id)
                .Where(x => x.DisbursementForm.DeptRep.Department == emp.Department)
                .ToList();
        }
        public List<DisbursementFormRequisitionFormProduct> FindDFRFPByDFId(int id)
        {
            return db.DisbursementFormRequisitionFormProducts
                .Where(x => x.DisbursementForm.Id == id)
                .ToList();
        }
        public List<DisbursementFormRequisitionForm> FindDFRFList(int id)
        {
            return db.DisbursementFormRequisitionForms
                .Where(x => x.DisbursementForm.Id == id)
                .ToList();
        }
        public List<DisbursementFormRequisitionForm> FindDFRFList(int id, Employee emp)
        {
            return db.DisbursementFormRequisitionForms
                .Where(x => x.DisbursementForm.Id == id)
                .Where(x => x.DisbursementForm.DeptRep.Department == emp.Department)
                .ToList();
        }
        public List<DisbursementFormRequisitionFormProduct> FindDFRFPByDFOrderByProduct(int id)
        {
            List < DisbursementFormRequisitionFormProduct > dfrfpList = db.DisbursementFormRequisitionFormProducts
                .OrderBy(x => x.RequisitionFormsProduct.Product.ProductName)
                .Where(x => x.DisbursementForm.Id == id)
                .ToList();
            return dfrfpList;
        }
    }
}
