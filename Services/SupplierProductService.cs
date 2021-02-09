using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory_Management_System.DB;
using Inventory_Management_System.Enums;
using Inventory_Management_System.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace Inventory_Management_System.Services
{
    public class SupplierProductService
    {
        private InventoryManagementSystemContext db;

        public SupplierProductService (InventoryManagementSystemContext db)
        {
            this.db = db;
        }

        public List<SupplierProduct> FindSupplierProducts()
        {
            return db.SupplierProducts.ToList();
        }

        public bool SaveSupplierProductsFromCSV(List<CSVSupplierProduct> csvList)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (CSVSupplierProduct sp in csvList)
                    {
                        SupplierProduct spSave = new SupplierProduct();
                        spSave.ProductPrice = sp.ProductPrice;
                        spSave.PriorityLevel = sp.PriorityLevel;
                        spSave.SPAvailStatus = (SPAvailStatus)Enum.Parse(typeof(SPAvailStatus), sp.SPAvailStatus);
                        spSave.Product = db.Products.Where(x => x.ProductCode == sp.ProductCode).First();
                        spSave.Supplier = db.Suppliers.Where(x => x.SupplierName == sp.SupplierName).First();

                        db.SupplierProducts.Add(spSave);
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

        
    }
}
