using Inventory_Management_System.DB;
using Inventory_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Services
{
    public class PurchaseDeliveryProductService
    {
        protected InventoryManagementSystemContext db;

        public PurchaseDeliveryProductService(InventoryManagementSystemContext db)
        {
            this.db = db;
        }

        public List<PurchaseOrderSupplierProduct> FindPurchaseOrderSupplierProductList()
        {
            return db.PurchaseOrderSupplierProducts.ToList();
        }
    }
}
