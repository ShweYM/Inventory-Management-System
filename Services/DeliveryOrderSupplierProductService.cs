using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Inventory_Management_System.DB;
using Inventory_Management_System.Models;

namespace Inventory_Management_System.Services
{
    public class DeliveryOrderSupplierProductService
    {
        protected InventoryManagementSystemContext db;
        private DeliveryOrderService doService;

        public DeliveryOrderSupplierProductService(InventoryManagementSystemContext db,DeliveryOrderService doService)
        {
            this.db = db;
            this.doService = doService;
        }
        public List<DeliveryOrderSupplierProduct> FindDeliveryOrderProductListById(int id)
        {
            List<DeliveryOrderSupplierProduct> deliveryOrderSupplierProductsList = db.DeliveryOrderSupplierProducts
                .Where(x => x.DeliveryOrder.Id == id)
                .ToList();
            return deliveryOrderSupplierProductsList;
        }

        public List<DeliveryOrderSupplierProduct> FindDOSRsByDO(DeliveryOrder DO)
        {
            return db.DeliveryOrderSupplierProducts
                .Where(x => x.DeliveryOrder == DO)
                .ToList();
        }

        public List<DeliveryOrderSupplierProduct> FindBalanceValuestoInput(int doid)
        {
            DeliveryOrder _do = db.DeliveryOrders.Find(doid);

            PurchaseOrder _po = db.PurchaseOrders.Find(_do.PurchaseOrder.Id);


            //Find All DO with respect to the po that needs to be updated
            List<DeliveryOrder> doListCheck = db.DeliveryOrders
                .Where(x => x.PurchaseOrder == _po)
                .ToList();

            //Find List of Products in the PO
            List<Product> prodListCheck = db.PurchaseOrderSupplierProducts.Where(x => x.PurchaseOrder == _po)
                .Select(x => x.SupplierProduct.Product)
                .Distinct()
                .ToList();


            Dictionary<int, int> _prodCountCheckDict = new Dictionary<int, int>();

            //Assign Dictionary
            for (int i = 0; i < prodListCheck.Count; i++)
            {
                int p = prodListCheck[i].Id;
                _prodCountCheckDict[p] = db.PurchaseOrderSupplierProducts
                    .Where(x=>x.PurchaseOrder == _po)
                    .Where(x=>x.SupplierProduct.Product.Id == p)
                    .First()
                    .POQuantityRequested;
            }

            //Update Dict Value for Checking
            for (int i = 0; i < prodListCheck.Count; i++)
            {
                List<DeliveryOrderSupplierProduct> _dosrList = doService.FindDOSRByProductIdAndPO(_po, prodListCheck[i].Id);
                foreach (DeliveryOrderSupplierProduct _dosr in _dosrList)
                {
                    int p2 = _dosr.PurchaseOrderSupplierProduct.SupplierProduct.Product.Id;
                    _prodCountCheckDict[p2] = _prodCountCheckDict[p2] - _dosr.DOQuantityReceived;
                    Debug.WriteLine(p2 + ": " + _prodCountCheckDict[p2]);
                }
            }


            List<DeliveryOrderSupplierProduct> dosrList = new List<DeliveryOrderSupplierProduct>();

            foreach (KeyValuePair<int, int> entry in _prodCountCheckDict)
            {
                PurchaseOrderSupplierProduct _posr = db.PurchaseOrderSupplierProducts
                    .Where(x => x.PurchaseOrder == _do.PurchaseOrder)
                    .Where(x => x.SupplierProduct.Product.Id == entry.Key)
                    .First();
                

                DeliveryOrderSupplierProduct _dosr = db.DeliveryOrderSupplierProducts
                    .Where(x => x.DeliveryOrder == _do)
                    .Where(x => x.PurchaseOrderSupplierProduct == _posr)
                    .First();

                _dosr.DOQuantityReceived = entry.Value;

                dosrList.Add(_dosr);
            }

            return dosrList;

            
        }
        public List<DeliveryOrderSupplierProduct> FindDOSRByPOId(int POid)
        {
            PurchaseOrder _po = db.PurchaseOrders.Find(POid);
            List<DeliveryOrder> _doList = db.DeliveryOrders
                .Where(x => x.PurchaseOrder == _po)
                .ToList();

            List<DeliveryOrderSupplierProduct> _dospList = new List<DeliveryOrderSupplierProduct>();
            foreach (DeliveryOrder _do in _doList)
            {
                List<DeliveryOrderSupplierProduct> _dospIndividualList = db.DeliveryOrderSupplierProducts
                    .Where(x => x.DeliveryOrder == _do)
                    .ToList();
                foreach (DeliveryOrderSupplierProduct _dosp in _dospIndividualList)
                {
                    _dospList.Add(_dosp);
                }
            }

            return _dospList;
        }
    }
}
