using Inventory_Management_System.DB;
using Inventory_Management_System.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Services
{
    public class EmployeeTypeService
    {
        protected InventoryManagementSystemContext db;

        public EmployeeTypeService(InventoryManagementSystemContext db)
        {
            this.db = db;
        }

        public EmployeeType FindEmployeeTypeByEmployeeTypeName (string employeeTypeName)
        {
            EmployeeType empType = db.EmployeeTypes
                .Where(x => x.EmployeeTypeName == employeeTypeName).FirstOrDefault();
            return empType;
        }

        public bool SaveEmployeeTypesFromCSV(List<CSVEmployeeType> etList)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (CSVEmployeeType et in etList)
                    {
                        EmployeeType etSave = new EmployeeType ();
                        etSave.EmployeeTypeName = et.EmployeeType;

                        db.EmployeeTypes.Add(etSave);
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
