using Inventory_Management_System.DB;
using Inventory_Management_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Services
{
    public class EmployeeService
    {
        protected InventoryManagementSystemContext db;
        private EmployeeTypeService etService;
        private DepartmentService dService;

        public EmployeeService(InventoryManagementSystemContext db, EmployeeTypeService etService, DepartmentService dService)
        {
            this.db = db;
            this.etService = etService;
            this.dService = dService;
        }

        public Employee GetEmployee(string username, string password)
        {
            Employee emp = db.Employees
                .Where(x => x.Username == username.ToLower() && x.Password == password)
                .FirstOrDefault();

            return emp;
        }

        public Employee GetEmployeeById(int id)
        {
            Employee emp = db.Employees
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return emp;
        }
        public Employee GetEmployee(string username)
        {
            Employee emp = db.Employees
                .Where(x => x.Username == username)
                .FirstOrDefault();

            return emp;
        }

        //API Test
        public List<Employee> GetEmployeeList()
        {
            List<Employee> emp = db.Employees.ToList();
               
            return emp;
        }

        public List<Employee> GetEmpListExcludingDeptHeadAndTempDeptHeadandDeptRep(Department dept)
        {
            List<Employee> empList = db.Employees
                .Where(x => x.Department == dept)
                .Where(x => x.EmployeeType.EmployeeTypeName != "Department Representative" && x.EmployeeType.EmployeeTypeName != "Department Head")
                .Where(x => x.TempDeptHeadType.EmployeeTypeName != "Temporary Department Head")
                .ToList();

            return empList;
        }
        public List<Employee> GetEmployeeListByDepartmentExcludingDeptHeadandDeptRep(Department dept)
        {
            List<Employee> empList = db.Employees
                .Where(x => x.Department == dept)
                .Where(x => x.EmployeeType.EmployeeTypeName != "Department Representative" && x.EmployeeType.EmployeeTypeName != "Department Head")
                .ToList();

            return empList;
        }

        public Employee FindDeptHeadOrStoreManager(Department dept, string empType)
        {
            Employee deptHead = db.Employees
                .Where(x => x.Department == dept)
                .Where(x => x.EmployeeType.EmployeeTypeName == empType)
                .FirstOrDefault();
            return deptHead;
        }

        public Employee FindDeptHead(Employee emp)
        {
            Employee deptHead = db.Employees
                .Where(x => x.Department == emp.Department)
                .Where(x => x.EmployeeType == emp.EmployeeType)
                .FirstOrDefault();
            return deptHead;
        }


        

        

        public Employee FindEmployeeWhoIsDeptRep(Department dept)
        {
            return db.Employees
                .Where(x => x.Department == dept)
                .Where(x=> x.EmployeeType.EmployeeTypeName == "Department Representative")
                .FirstOrDefault();
        }

        public bool SaveEmployeesFromCSV(List<CSVEmployee> empList)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (CSVEmployee e in empList)
                    {
                        Employee eSave = new Employee();
                        eSave.Username = e.Username;
                        eSave.Password = e.Password;
                        eSave.Email = e.Email;
                        eSave.Firstname = e.Firstname;
                        eSave.Lastname = e.Lastname;
                        eSave.PhoneNumber = e.PhoneNumber;
                        eSave.EmployeeType = etService.FindEmployeeTypeByEmployeeTypeName(e.EmployeeType);
                        //eSave.Firstname = db.Employees.Where(x=>x.Id)(e.SupervisedById);
                        eSave.Department = dService.FindDepartmentByDepartmentName(e.Department);
                        //eSave.OriginalEmployeeType = etService.FindEmployeeTypeByEmployeeTypeName(e.OriginalEmployeeType);
                        eSave.TempDeptHeadType = etService.FindEmployeeTypeByEmployeeTypeName(e.TemporaryHead);
                        db.Employees.Add(eSave);
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

        public bool SaveEmployeesSupervisedByFromCSV(List<CSVEmployee> empList)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (CSVEmployee e in empList)
                    {
                        Employee eSave = GetEmployee(e.Username);
                        if (e.EmployeeType != "Department Head" && e.EmployeeType != "Store Manager" && e.EmployeeType != "Store Supervisor" && e.EmployeeType != "Store Clerk" && e.EmployeeType != "Warehouse Packer")
                        {
                            Department deptCheck = dService.FindDepartmentByDepartmentName(e.Department);
                            Employee deptHeadCheck = FindDeptHeadOrStoreManager(deptCheck, "Department Head");
                                eSave.SupervisedBy = deptHeadCheck;

                        }
                        else if(e.EmployeeType != "Store Manager")
                        {
                            Department deptCheck = dService.FindDepartmentByDepartmentName(e.Department);
                            Employee deptHeadCheck = FindDeptHeadOrStoreManager(deptCheck, "Store Manager");
                            eSave.SupervisedBy = deptHeadCheck;
                        }
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

        public Employee FindByUsernameAndPassword(string username , string password)
        {
            Employee emp = db.Employees
                .Where(x => x.Username == username)
                .Where(x => x.Password == password)
                .First();
            return emp;
        }

        public bool UpdateEmployeeDetails(Employee emp, CollectionPoint cp)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    Employee _emp = db.Employees.Find(emp.Id);
                    _emp.Password = emp.Password;
                    _emp.PhoneNumber = emp.PhoneNumber;
                    _emp.Email = emp.Email;
                    db.Employees.Update(_emp);
                    db.SaveChanges();

                    if (_emp.EmployeeType.EmployeeTypeName == "Department Representative")
                    {
                        Department _dept = db.Departments.Find(emp.Department.Id);
                        CollectionPoint _cp = db.CollectionPoints.Find(cp.Id);
                        _dept.CollectionPoint = _cp;
                        db.Departments.Update(_dept);
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
