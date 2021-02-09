using Inventory_Management_System.DB;
using Inventory_Management_System.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Services
{
    public class CollectionPointService
    {
        private InventoryManagementSystemContext db;

        public CollectionPointService(InventoryManagementSystemContext db)
        {
            this.db = db;
        }

        public List<CollectionPoint> GetCPList()
        {
            return db.CollectionPoints.ToList();
        }

        public bool UpdateCollectionPoint(Employee emp, CollectionPoint cp)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    CollectionPoint _cp = db.CollectionPoints.Find(cp.Id);
                    Employee _emp = db.Employees.Find(emp.Id);

                    if (_emp.EmployeeType.EmployeeTypeName != "Department Representative")
                    {
                        throw new Exception("This is not a dept rep");
                    }

                    Department _dept = db.Departments.Find(emp.Department.Id);

                    _dept.CollectionPoint = _cp;
                    _dept.DeptRepId = _emp.Id;
                    _dept.deptRep = _emp;
                    db.Update(_dept);
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
