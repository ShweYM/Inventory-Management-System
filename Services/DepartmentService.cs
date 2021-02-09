using Inventory_Management_System.DB;
using Inventory_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Services
{
    public class DepartmentService
    {
        protected InventoryManagementSystemContext db;

        public DepartmentService(InventoryManagementSystemContext db)
        {
            this.db = db;
        }

        public List<Department> FindDepartmentList()
        {
            List<Department> depts = db.Departments.ToList();
            foreach (Department dept in depts)
            {
                dept.deptHead = db.Employees
                    .Where(x => x.Department == dept)
                    .Where(x => x.EmployeeType.EmployeeTypeName == "Department Head")
                    .FirstOrDefault();
                dept.deptRep = db.Employees
                    .Where(x => x.Department == dept)
                    .Where(x => x.EmployeeType.EmployeeTypeName == "Department Representative")
                    .FirstOrDefault();
            }
            return depts;
        }

        public Department FindDepartmentByDepartmentName(string deptName)
        {
            Department dept = db.Departments.Where(x => x.DepartmentName == deptName).FirstOrDefault();
            return dept;
        }

        public Department GetDepartment(string deptname)
        {
            Department dept = db.Departments
                .Where(x => x.DepartmentName == deptname)
                .FirstOrDefault();

            return dept;
        }
    }
}
