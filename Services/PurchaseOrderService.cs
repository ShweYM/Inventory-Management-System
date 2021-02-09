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
    public class PurchaseOrderService
    {
        protected InventoryManagementSystemContext db;

        public PurchaseOrderService(InventoryManagementSystemContext db)
        {
            this.db = db;
        }

        public List<PurchaseOrder> FindPurchaseOrders()
        {
            return db.PurchaseOrders.ToList();
        }



        public List<PurchaseOrder> FindPOListThatIsNotCompleted()
        {
            List<PurchaseOrder> poList = db.PurchaseOrders
                .OrderBy(x=>x.POIssueDate)
                .Where(x => x.POStatus != Enums.POStatus.Completed)
                .ToList();
            return poList;
        }

        public List<PurchaseOrder> FindAllIssuedPOs()
        {
            List<PurchaseOrder> poList = db.PurchaseOrders
                .OrderBy(x => x.POIssueDate)
                .Where(x => x.POStatus == Enums.POStatus.Issued)
                .ToList();
            return poList;
        }

        public List<PurchaseOrder> FindAllPOsNotCompleted()
        {
            List<PurchaseOrder> poList = db.PurchaseOrders
                .OrderBy(x => x.POIssueDate)
                .Where(x => x.POStatus == Enums.POStatus.NotCompleted)
                .ToList();
            return poList;
        }

        public List<PurchaseOrder> FindAllPOsCompleted()
        {
            List<PurchaseOrder> poList = db.PurchaseOrders
                .OrderBy(x => x.POIssueDate)
                .Where(x => x.POStatus == Enums.POStatus.Completed)
                .ToList();
            return poList;
        }

        public bool CreatePurchaseOrder(Employee emp, string comment, List<Product> productList)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    DateTime createDate = DateTime.Now;

                    string commentSave = comment;
                    // Assign the employee
                    Employee empCheck = db.Employees
                    .Where(x => x.Id == emp.Id)
                    .FirstOrDefault();

                    //Create empty list of supplier products
                    List<SupplierProduct> _spList = new List<SupplierProduct>();



                    for (int i = 0; i<productList.Count;i++)
                    {
                        SupplierProduct sp = db.SupplierProducts
                            .OrderBy(x=>x.PriorityLevel)
                            .Where(x => x.Product == productList[i])
                            .Where (x=> x.SPAvailStatus == Enums.SPAvailStatus.Available)
                            .First();
                        if (sp == null)
                        {
                            sp = db.SupplierProducts
                                .OrderBy(x => x.PriorityLevel)
                                .Where(x => x.Product == productList[i])
                                .First();
                        }
                        _spList.Add(sp);
                        Debug.WriteLine(sp.Supplier.SupplierName);

                    }

                    //Find the total of PO before adding PO
                    int count = FindPOsByDateToday();


                    IEnumerable<Supplier> suppliers = _spList.Select(x => x.Supplier).Distinct();
                    foreach (Supplier sup in suppliers)
                    {
                        count++;

                        Debug.WriteLine(sup.SupplierName);


                        //Assign a PO Code
                        string poCode = "PO" +  "/" + createDate.ToString("ddMMyy") + "/" + count.ToString();

                        //Create a PO
                        PurchaseOrder _po = new PurchaseOrder()
                        {
                            supplier = sup,
                            DeliverTo = "Logic University",
                            expectedDeliveryDate = DateTime.Now.AddDays(10),
                            IssuedBy = empCheck, 
                            POCode = poCode, 
                            POComments = comment, 
                            POIssueDate = DateTime.Now, 
                            POStatus = Enums.POStatus.Issued
                        };
                        db.Add(_po);
                        db.SaveChanges();

                        //Find the Products of this particular Supplier based on the filtered list previously generated
                        List<SupplierProduct> spListIndividualSupplier = _spList
                            .Where(x => x.Supplier == sup)
                            .ToList();

                        foreach (SupplierProduct _sp in spListIndividualSupplier)
                        {
                            PurchaseOrderSupplierProduct _posr = new PurchaseOrderSupplierProduct()
                            {
                                POUnitPrice = _sp.ProductPrice,
                                PurchaseOrder = _po,
                                SupplierProduct = _sp,
                                //POQuantityRequested = productList.Where(x => x.Id == _sp.Product.Id).First().ProductRequested,
                                POQuantityRequested = db.Products.Find(productList.Where(x => x.Id == _sp.Product.Id).First().Id).ReorderQuantity,
                            };
                            db.PurchaseOrderSupplierProducts.Add(_posr);
                            db.SaveChanges();
                        }
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
        
        public int FindPOsByDateToday()
        {
            return db.PurchaseOrders
                .Where(x => x.POIssueDate.Date == DateTime.Today.Date)
                .Count();
        }

        public PurchaseOrder FindPurchaseOrderById(int id)
        {
            return db.PurchaseOrders.Find(id);
        }
        public List<PurchaseOrderSupplierProduct> FindPOSRByPOId(int id)
        {
            return db.PurchaseOrderSupplierProducts
                .Where(x => x.PurchaseOrder.Id == id)
                .ToList();
        }

        public PurchaseOrder FindPO(int id)
        {
            return db.PurchaseOrders.Find(id);
        }
    }
}
