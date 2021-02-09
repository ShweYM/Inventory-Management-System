using Inventory_Management_System.DB;
using Inventory_Management_System.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Services
{
    public class DeliveryOrderService
    {
        protected InventoryManagementSystemContext db;
        private InventoryTransactionService invtService;

        public DeliveryOrderService(InventoryManagementSystemContext db, InventoryTransactionService invtService)
        {
            this.db = db;
            this.invtService = invtService;
        }

        public List<DeliveryOrder> FindDeliveryOrders()
        {
            return db.DeliveryOrders.ToList();
        }

        public bool CreateDeliveryOrder(int poid, string doComments, int empid)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    
                    Employee _emp = db.Employees.Find(empid);
                    //check no of DOs today
                    int count = FindDOsByDateToday() +1;

                    //Assign a PO Code
                    string doCode = "DO" + "/" + DateTime.Now.ToString("ddMMyy") + "/" + count.ToString();

                    PurchaseOrder _poCheck = db.PurchaseOrders.Find(poid);

                    if (_poCheck.POStatus == Enums.POStatus.Completed)
                    {
                        throw new Exception("This is not the right PO");
                    }
                    //Create Delivery Order
                    DeliveryOrder _do = new DeliveryOrder()
                    {
                        PurchaseOrder = _poCheck,
                        DOCode = doCode,
                        DOComments = doComments,
                        DOReceivedDate = DateTime.Now,
                        ReceivedBy = _emp,
                        Supplier = _poCheck.supplier,
                        DOStatus = Enums.DOStatus.Created
                    };
                    db.DeliveryOrders.Add(_do);
                    db.SaveChanges();

                    //Find POSR based on corresponding PO
                    List<PurchaseOrderSupplierProduct> posrList = db.PurchaseOrderSupplierProducts
                        .Where(x => x.PurchaseOrder == _poCheck)
                        .ToList();

                    //Create Corresponding DOSR
                    foreach(PurchaseOrderSupplierProduct posr in posrList)
                    {
                        DeliveryOrderSupplierProduct _dosr = new DeliveryOrderSupplierProduct()
                        {
                            PurchaseOrderSupplierProduct = posr,
                            DeliveryOrder = _do,
                            //DOQuantityReceived = posr.POQuantityRequested,
                            DOQuantityReceived = 0,


                        };
                        db.DeliveryOrderSupplierProducts.Add(_dosr);
                    }

                    db.SaveChanges();

                    transcat.Commit();
                    return true;
                }
                catch
                {
                    transcat.Rollback();
                    return false;
                }
            }
        }
        public List<DeliveryOrder> FindAllDOsCreated()
        {
            List<DeliveryOrder> doList = db.DeliveryOrders
                .OrderBy(x => x.PurchaseOrder.POIssueDate)
                .Where(x => x.DOStatus == Enums.DOStatus.Created)
                .ToList();
            return doList;
        }
        public List<DeliveryOrder> FindAllCompletedDOs()
        {
            List<DeliveryOrder> poList = db.DeliveryOrders
                .OrderByDescending(x => x.PurchaseOrder.POIssueDate)
                .Where(x => x.PurchaseOrder.POStatus == Enums.POStatus.Completed)
                .ToList();
            return poList;
        }
        public int FindDOsByDateToday()
        {
            return db.DeliveryOrders
                .Where(x => x.DOReceivedDate.Date == DateTime.Today.Date)
                .Count();
        }

        public DeliveryOrder FindDOByPOIdWithDOStatusCreated(int poid)
        {
            return db.DeliveryOrders
                .Where(x => x.PurchaseOrder == db.PurchaseOrders.Find(poid))
                .Where(x => x.DOStatus == Enums.DOStatus.Created)
                .First();
        }

        public List<DeliveryOrder> FindDOListByPOID(int poid)
        {
            return db.DeliveryOrders
                .Where(x => x.PurchaseOrder == db.PurchaseOrders.Find(poid))
                .ToList();
        }

        public bool UpdateDeliveryOrder(List<DeliveryOrderSupplierProduct> dospList, int empid, DeliveryOrder DO)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    Dictionary<int, int> _prodCountCheckDict2 = new Dictionary<int, int>();

                    Employee _emp = db.Employees.Find(empid);

                    //FindDO
                    DeliveryOrder _do = db.DeliveryOrders.Find(DO.Id);

                    //FindPO
                    PurchaseOrder _po = db.PurchaseOrders.Find(_do.PurchaseOrder.Id);

                    //FindList of dosp
                    List<DeliveryOrderSupplierProduct> _dospList = db.DeliveryOrderSupplierProducts
                        .Where( x=>x.DeliveryOrder.Id == _do.Id)
                        .ToList();

                    

                    //Check InventoryTransaction Count
                    int count = invtService.FindInvTransByTodayCount();
                    //Define invtrans Code

                    foreach (DeliveryOrderSupplierProduct _dosp in _dospList)
                    {
                        foreach (DeliveryOrderSupplierProduct dosp in dospList)
                        {
                            if (dosp.Id == _dosp.Id)
                            {
                                _dosp.DOQuantityReceived = dosp.DOQuantityReceived;
                                db.Update(_dosp);
                                db.SaveChanges();

                                count++;
                                string invtransCode = "IT" + "/" + _do.DOReceivedDate.Date.ToString("ddMMyy") + "/" + count.ToString();

                                InventoryTransaction _it = new InventoryTransaction()
                                {
                                    InventoryTransComments = _do.DOCode,
                                    Product = _dosp.PurchaseOrderSupplierProduct.SupplierProduct.Product,
                                    ProductId = _dosp.PurchaseOrderSupplierProduct.SupplierProduct.Product.Id,
                                    Employee = _do.ReceivedBy,
                                    EmployeeId = _do.ReceivedBy.Id,
                                    InventoryChangeQuantity = _dosp.DOQuantityReceived,
                                    ITStatus = Enums.ITStatus.Auto,
                                    InventoryTransDate = _do.DOReceivedDate.Date,
                                    ITCode = invtransCode,
                                    ProductPrice = _dosp.PurchaseOrderSupplierProduct.POUnitPrice
                                    
                                };
                                db.Add(_it);
                                db.SaveChanges();
                                //Adjust Inventory Quantity
                                Product _p = db.Products.Find(_it.Product.Id);
                                _p.InventoryQuantity = _p.InventoryQuantity + _it.InventoryChangeQuantity;
                                
                                db.Products.Update(_p);
                            }
                        }
                    }
                    
                    _do.DOStatus = Enums.DOStatus.Completed;
                    db.Update(_do);

                    //Find All DO with respect to the po that needs to be updated
                    List <DeliveryOrder> doListCheck = db.DeliveryOrders
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
                        _prodCountCheckDict[p] = 0;
                    }

                    //Update Dict Value for Checking
                    for (int i = 0; i < prodListCheck.Count; i++)
                    {
                        List<DeliveryOrderSupplierProduct> _dosrList = FindDOSRByProductIdAndPO(_po, prodListCheck[i].Id);
                        foreach(DeliveryOrderSupplierProduct _dosr in _dosrList)
                        {
                            int p2 = _dosr.PurchaseOrderSupplierProduct.SupplierProduct.Product.Id;
                            _prodCountCheckDict[p2] = _prodCountCheckDict[p2] + _dosr.DOQuantityReceived;
                        }
                    }

                    bool isCompleted = true;
                    //Check Dict Value
                    foreach (KeyValuePair<int, int> entry in _prodCountCheckDict)
                    {
                        PurchaseOrderSupplierProduct _posr = db.PurchaseOrderSupplierProducts
                            .Where(x => x.SupplierProduct.Product.Id == entry.Key).First();
                        if(entry.Value != _posr.POQuantityRequested)
                        {
                            isCompleted = false;
                            break;
                        }
                    }
                    //If all items are completed, then check to save as PO Completed or Not Completed
                    if (isCompleted == false)
                    {
                        _po.POStatus = Enums.POStatus.NotCompleted;
                        db.Update(_po);
                        db.SaveChanges();
                    }
                    else
                    {
                        _po.POStatus = Enums.POStatus.Completed;
                        db.Update(_po);
                        db.SaveChanges();
                    }

                    transcat.Commit();
                    return true;
                }
                catch
                {
                    transcat.Rollback();
                    return false;
                }
            }
        }

        public DeliveryOrderSupplierProduct FindDOSRByProductId(DeliveryOrder _do, int poid)
        {
            DeliveryOrderSupplierProduct dosp = db.DeliveryOrderSupplierProducts
                .Where(x=>x.DeliveryOrder == _do)
                .Where(x=>x.PurchaseOrderSupplierProduct.SupplierProduct.Product.Id == poid)
                .FirstOrDefault();
            return dosp;
        }

        public List<DeliveryOrderSupplierProduct> FindDOSRByProductIdAndPO(PurchaseOrder po, int poid)
        {
            List<DeliveryOrderSupplierProduct> dospList = db.DeliveryOrderSupplierProducts
                .Where(x=>x.PurchaseOrderSupplierProduct.PurchaseOrder == po)
                .Where(x => x.PurchaseOrderSupplierProduct.SupplierProduct.Product.Id == poid)
                .ToList();
            return dospList;
        }


    }
}
