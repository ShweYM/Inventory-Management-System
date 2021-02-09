using Inventory_Management_System.DB;
using Inventory_Management_System.Models;
using Inventory_Management_System.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventory_Management_System.Services
{
    public class InventoryTransactionService
    {
        protected InventoryManagementSystemContext db;
        private EmployeeService empService;
        private ProductService pService;

        public InventoryTransactionService(InventoryManagementSystemContext db, EmployeeService employeeService, ProductService pService)
        {
            this.db = db;
            this.empService = employeeService;
            this.pService = pService;
        }

        public InventoryTransaction FindInventoryTransaction(int id)
        {
            InventoryTransaction _it = db.InventoryTransactions.Find(id);
            _it.TotalValue = _it.ProductPrice * _it.InventoryChangeQuantity;

            return _it;
        }
        public List<InventoryTransaction> FindInvTransByToday()
        {
            List<InventoryTransaction> InvTransCheckList = db.InventoryTransactions
                    .Where(x => x.InventoryTransDate.Date == DateTime.Now.Date)
                    .ToList();
            return InvTransCheckList;
        }

        public int FindInvTransByTodayCount()
        {
            return db.InventoryTransactions
                    .Where(x => x.InventoryTransDate.Date == DateTime.Now.Date).Count();
        }

        public List<InventoryTransaction> ListInventoryTransaction()
        {
            return db.InventoryTransactions.ToList();
        }

        
        public bool CreateTransactionsManual2(List<Product> pList, List<int> itQuantity, Employee emp, string comments)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    List<Product> _productList = new List<Product>();

                    foreach (Product p in pList)
                    {
                        Product _p = db.Products.Find(p.Id);
                        _productList.Add(_p);
                    }
                    SupplierProduct sp = db.SupplierProducts
                        .Where(x => x.PriorityLevel == 1)
                        .First();

                    List<int> _itQuantity = itQuantity;

                    Employee _emp = empService.GetEmployeeById(emp.Id);
                    for (int i = 0; i < _productList.Count; i++)
                    {
                        int InvTransCheckList = FindInvTransByTodayCount();
                        int count = InvTransCheckList + 1;
                        //Define invtrans Code
                        string invtransCode = "IT" + "/" + DateTime.Now.ToString("ddMMyy") + "/" + count.ToString();
                        InventoryTransaction itSave = new InventoryTransaction()
                        {
                            EmployeeId = _emp.Id,
                            ProductId = _productList[i].Id,
                            Employee = _emp,
                            InventoryChangeQuantity = _itQuantity[i],
                            InventoryTransComments = comments,
                            InventoryTransDate = DateTime.Now,
                            ITStatus = Enums.ITStatus.PendingApproval,
                            ITCode = invtransCode,
                            Product = _productList[i],
                            ProductPrice = sp.ProductPrice
                        };
                        
                        db.InventoryTransactions.Add(itSave);

                        db.SaveChanges();
                    };

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
        public List<Product> ListITransProducts(List<InventoryTransaction> itList)
        {
            List<Product> productList = new List<Product>();
            foreach (InventoryTransaction it in itList)
            {
                Product _itProduct = db.Products
                    .Where(x => x.Id == it.ProductId)
                    .FirstOrDefault();
                productList.Add(_itProduct);
            }
            return productList;
        }

        public List<Employee> ListITransEmployee (List<InventoryTransaction> itList)
        {
            List<Employee> empList = new List<Employee>();
            foreach (InventoryTransaction it in itList)
            {
                Employee _itEmp = db.Employees
                    .Where(x => x.Id == it.EmployeeId)
                    .FirstOrDefault();
                empList.Add(_itEmp);
            }
            return empList;
        }

        public bool ApproveInventoryApproval(InventoryTransaction it, Employee emp)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    Employee approvalBy = db.Employees.Find(emp.Id);
                    DateTime approveDate = DateTime.Now;
                    InventoryTransaction _it = db.InventoryTransactions.Find(it.Id);
                    _it.ITStatus = Enums.ITStatus.Approved;
                    _it.ITApprovalDate = approveDate;
                    _it.ITApprovalBy = approvalBy;

                    db.InventoryTransactions.Update(_it);
                    db.SaveChanges();

                    Product pUpdate = pService.FindProductById(_it.Product.Id);
                    pUpdate.InventoryQuantity = pUpdate.InventoryQuantity + _it.InventoryChangeQuantity;
                    if (pUpdate.InventoryQuantity < 0)
                    {
                        throw new Exception(pUpdate.InventoryQuantity + " is not enough");
                    }
                    db.Products.Update(pUpdate);
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
        public bool RejectInventoryApproval(InventoryTransaction it, Employee emp)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    Employee approvalBy = db.Employees.Find(emp.Id);
                    DateTime approveDate = DateTime.Now;
                    InventoryTransaction _it = db.InventoryTransactions.Find(it.Id);
                    _it.ITStatus = Enums.ITStatus.Rejected;
                    _it.ITApprovalDate = approveDate;
                    _it.ITApprovalBy = approvalBy;
                    db.InventoryTransactions.Update(_it);
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

        public List<InventoryTransaction> ListITWithPendingApprovalMoreThanOrEqual250()
        {
            List<InventoryTransaction> _itList = db.InventoryTransactions
                .Where(x => x.ITStatus == Enums.ITStatus.PendingApproval)
                .ToList();
            foreach (InventoryTransaction _it in _itList)
            {
                _it.TotalValue = _it.InventoryChangeQuantity * _it.ProductPrice;
            }
            _itList = _itList
                .Where(x => x.TotalValue >= 250.00 || x.TotalValue <= -250.00)
                .ToList();
            return _itList;
        }
        public List<InventoryTransaction> ListITWithPendingApprovalLessThan250()
        {
            List<InventoryTransaction> _itList = db.InventoryTransactions
                .Where(x => x.ITStatus == Enums.ITStatus.PendingApproval)
                .ToList();
            foreach (InventoryTransaction _it in _itList)
            {
                _it.TotalValue = _it.InventoryChangeQuantity * _it.ProductPrice;
            }
            _itList = _itList
                .Where(x => x.TotalValue < 250.00 && x.TotalValue >-250.00)
                .ToList();
            return _itList;
        }

    }
}
