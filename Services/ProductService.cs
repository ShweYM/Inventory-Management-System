using Inventory_Management_System.DB;
using Inventory_Management_System.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Services
{
    public class ProductService
    {
        private InventoryManagementSystemContext db;
        private ProductCategoryService pcService;

        public ProductService(InventoryManagementSystemContext db, ProductCategoryService pcService)
        {
            this.db = db;
            this.pcService = pcService;
        }

        public List<Product> FindProducts()
        {
            return db.Products.ToList();
        }

        public List<String> FindProductCategory()
        {
            return db.Products.Select(p => p.ProductCategory.ProductCategoryName).Distinct().ToList();
        }

        public bool SaveProductsFromCSV(List<CSVProduct> pList)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (CSVProduct p in pList)
                    {
                        Product pSave = new Product();
                        pSave.ProductCode = p.ProductCode;
                        pSave.ProductCategory = pcService.findProductCategory(p.ProductCategory);
                        pSave.ProductName = p.ProductName;
                        pSave.Units = p.Units;
                        pSave.ReorderLevel = p.ReorderLevel;
                        pSave.ReorderQuantity = p.ReorderQuantity;
                        pSave.InventoryLocation = p.InventoryLocation;
                        pSave.InventoryQuantity = p.InventoryQuantity;
                        pSave.MLReorderQuantity = p.MLReorderQuantity;
                        pSave.MLReorderLevel = p.MLReorderLevel;
                        db.Products.Add(pSave);
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

        public int FindProductQuantityByProductName(string productName)
        {
            return db.Products.Where(x => x.ProductName == productName).FirstOrDefault().InventoryQuantity;
        }

        

        public Product FindProductById(int id)
        {
            return db.Products.Find(id);
        }

        public Product FindProductByProductCode(string code)
        {
            return db.Products.Where(x=>x.ProductCode == code).FirstOrDefault();
        }

        public Product FindProductByProductName(string code)
        {
            return db.Products
                .Where(x => x.ProductName == code)
                .FirstOrDefault();
        }

        //save imageurl-name into database //htp
        public bool SaveProductImages(List<String> imgList)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    int imgid = 0;
                    foreach (String img in imgList)
                    {
                        //to save img as per product id
                        string output = new string(img.ToCharArray().Where(c => char.IsDigit(c)).ToArray());
                        imgid = int.Parse(output);
                        Product prod = db.Products.Where(x => x.Id == imgid).FirstOrDefault();

                        prod.ProductImage = img;
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
    }
}
