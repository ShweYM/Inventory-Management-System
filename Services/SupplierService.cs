using Inventory_Management_System.DB;
using Inventory_Management_System.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Inventory_Management_System.Services
{
    public class SupplierService
    {
        protected InventoryManagementSystemContext db;

        public SupplierService(InventoryManagementSystemContext db)
        {
            this.db = db;
        }

        public List<Supplier> FindSuppliers()
        {
            List<Supplier> sups = db.Suppliers.ToList();
            return sups;
        }

        public List<Supplier> FindSupplierList()
        {
            List<Supplier> sups = db.Suppliers
                .OrderBy(x => x.SupplierCode)
                .ToList();
            return sups;
        }
        public bool SaveSuppliersFromCSV(List<CSVSupplier> csvList)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (CSVSupplier s in csvList)
                    {
                        Supplier sSave = new Supplier();
                        sSave.SupplierCode = s.SupplierCode;
                        sSave.SupplierName = s.SupplierName;
                        sSave.Address = s.Address;
                        sSave.ContactName = s.ContactName;
                        sSave.FaxNumber = s.FaxNumber;
                        sSave.PhoneNumber = s.PhoneNumber;
                        sSave.GSTregistrationNumber = s.GSTregistrationNumber;
                        sSave.SupplierEmail = s.SupplierEmail;
                        db.Suppliers.Add(sSave);
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
