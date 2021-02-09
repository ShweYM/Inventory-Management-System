using Inventory_Management_System.DB;
using Inventory_Management_System.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Services
{
    public class ProductCategoryService
    {
        protected InventoryManagementSystemContext db;

        public ProductCategoryService(InventoryManagementSystemContext db)
        {
            this.db = db;
        }

        public ProductCategory findProductCategory(string productCategoryName)
        {

            return db.ProductCategories
                .Where(x => x.ProductCategoryName == productCategoryName)
                .FirstOrDefault();
        }

        //Yamone
        public List<ProductCategory> categorylist()
        {
            return db.ProductCategories.ToList();
        }

        public bool SaveProductCategoryFromCSV(List<CSVProductCategory> pcList)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (CSVProductCategory pc in pcList)
                    {
                        ProductCategory pcSave = new ProductCategory();
                        pcSave.ProductCategoryName = pc.ProductCategoryName;
                        db.ProductCategories.Add(pcSave);
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
