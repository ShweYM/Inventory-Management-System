using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory_Management_System.DB;
using Inventory_Management_System.Models;

namespace Inventory_Management_System.Services
{
    public class PurchaseOrderSupplierProductService
    {
        protected InventoryManagementSystemContext db;

        public PurchaseOrderSupplierProductService(InventoryManagementSystemContext db)
        {
            this.db = db;
        }

        public List<PurchaseOrderSupplierProduct> FindPurchaseOrderProductListById(int id)
        {
            List<PurchaseOrderSupplierProduct> purchaseOrderSupplierProducts = db.PurchaseOrderSupplierProducts
                .Where(x => x.PurchaseOrder.Id == id)
                .ToList();
            return purchaseOrderSupplierProducts;
        }
    }
}
