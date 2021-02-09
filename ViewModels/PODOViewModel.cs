using Inventory_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.ViewModels
{
    public class PODOViewModel
    {
        public Employee Employee{ get; set; }
        public PurchaseOrder PO { get; set; }
        public DeliveryOrder DO { get; set; }

        public PurchaseOrderSupplierProduct purchaseOrderSupplierProduct{ get; set; }

        public List<PurchaseOrder> poListIssued { get; set; }
        public List<PurchaseOrder> poListNotCompleted { get; set; }
        public List<PurchaseOrder> poListCompleted { get; set; }

        public List<DeliveryOrder> doListNotCompleted { get; set; }
        public List<DeliveryOrder> doListCompleted { get; set; }

        public List<Supplier> supplierList { get; set; }
        public List<SupplierProduct> supprodList { get; set; }
        public List<Product> productList { get; set; }
        public string comment { get; set; }
        public List<PurchaseOrderSupplierProduct> posrList { get; set; }
        public List<DeliveryOrder> doList { get; set; }
        public List<DeliveryOrderSupplierProduct> dosrList { get; set; }

    }
}
