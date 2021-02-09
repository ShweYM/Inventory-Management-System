using Inventory_Management_System.DB;
using Inventory_Management_System.Enums;
using Inventory_Management_System.Models;
using Inventory_Management_System.Utils;
using Inventory_Management_System.ViewModels;
using Microsoft.EntityFrameworkCore.Storage;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Inventory_Management_System.Services
{
    public class RequisitionService
    {
        private InventoryManagementSystemContext db;
        private ProductService pService;
        private EmployeeService empService;

        public RequisitionService(InventoryManagementSystemContext db, 
            ProductService pService,
            EmployeeService empService)
        {
            this.db = db;
            this.pService = pService;
            this.empService = empService;
        }

        public bool UpdateRequistionProduct(Employee emp, string comment, List<Product> pList)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())

            {
                try
                {
                    if (pList == null)
                    {
                        throw new Exception("No products to be stored as a Requisition Form");
                    }
                    bool isAllProductsQuantityLessThanZero = true;
                    foreach (Product p in pList)
                    {
                        if (p.ProductRequested >= 0)
                        {
                            isAllProductsQuantityLessThanZero = false;
                            break;
                        }
                    }

                    if(isAllProductsQuantityLessThanZero == true)
                    {
                        throw new Exception("Quantity of Product should be at least 0");
                    }

                    //find employee
                    Employee _emp = db.Employees.Find(emp.Id);

                    //Find the list of rfs on that day by the same employee
                    List<RequisitionForm> rfCheckList = FindRequisitionFormByDeptToday(_emp);

                    //Find the total no of rfs sent on the same day by the same department
                    int count = rfCheckList.Count + 1;

                    //rfCreateDate
                    DateTime createDate = DateTime.Now;

                    //Define RF Code
                    string rfCode = "RF" + "/" + _emp.Department.DeptCode + "/" + createDate.ToString("ddMMyy") + "/" + count.ToString();

                    //Create New RF
                    RequisitionForm _rf = new RequisitionForm() 
                    { 
                        RFCode = rfCode, 
                        Employee = _emp, 
                        RFStatus = RFStatus.Submitted, 
                        RFDate = createDate, 
                        RFComments = comment
                    };
                    db.RequisitionForms.Add(_rf);
                    db.SaveChanges();
                    

                    foreach (Product p in pList)
                    {
                        Product _p = db.Products.Find(p.Id);

                        RequisitionFormsProduct _rfp = new RequisitionFormsProduct()
                        {
                            RequisitionForm = _rf, 
                            Product = _p, 
                            ProductRequested = p.ProductRequested
                        };
                        db.RequisitionFormsProducts.Add(_rfp);
                        db.SaveChanges();
                    }

                    transcat.Commit();

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("IMS System", "silentxtwilight2@gmail.com"));
                    message.To.Add(new MailboxAddress(_emp.SupervisedBy.Firstname + " " + _emp.SupervisedBy.Lastname, _emp.SupervisedBy.Email));
                    message.Subject = "Requisition Form for Approval - " + _rf.RFCode;
                    message.Body = new TextPart("plain")
                    {
                        Text = "You have an outstanding Requisition Form  - " + _rf.RFCode + "\nthis is pending for approval"
                    };
                    EmailUtil eUtil = new EmailUtil();
                    eUtil.SendEmail(message);

                    return true;
                }
                catch
                {
                    transcat.Rollback();
                    return false;
                }

            }

        }

        public List<RequisitionForm> FindPendingRequisitionsByEmployee(Employee emp)
        {
            List<RequisitionForm> rfList = db.RequisitionForms
                .OrderBy(x => x.RFDate)
                .Where(x => x.Employee == emp)
                .Where(x=> x.RFStatus == Enums.RFStatus.Submitted)
                .ToList();
            return rfList;
        }

        public List<RequisitionForm> FindPendingRequisitionsByEmployeeAsHead(Department dept)
        {
            List<RequisitionForm> rfList = db.RequisitionForms
                .OrderBy(x => x.RFDate)
                .Where(x => x.Employee.Department == dept)
                .Where(x => x.RFStatus == RFStatus.Submitted)
                .ToList();
            return rfList;
        }

        public List<RequisitionForm> FindRequisitionsOtherThanSubmittedByEmployee(Employee emp)
        {
            List<RequisitionForm> rfList = db.RequisitionForms
                .OrderBy(x => x.RFDate)
                .Where(x => x.Employee == emp)
                .Where(x => x.RFStatus != RFStatus.Submitted)
                .ToList();
            return rfList;
        }

        public List<RequisitionForm> FindRequisitionsInDept(Department dept)
        {

            List<RequisitionForm> rfList = db.RequisitionForms
                .OrderBy(x => x.RFDate)
                .Where(x => x.Employee.Department == dept)
                .Where(x => x.RFStatus != Enums.RFStatus.Submitted)
                .ToList();
            return rfList;
        }

        public void ChangeRFStatusToCancelledById (int id)
        {
            RequisitionForm rf = db.RequisitionForms
                .Where(x => x.Id == id)
                .FirstOrDefault();
            rf.RFStatus = Enums.RFStatus.Cancelled;
            db.SaveChanges();
        }

        public bool ChangeRFStatusToApprovedById(RequisitionForm rf, List<RequisitionFormsProduct> rfpList, Employee deptHead, string comment)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    
                    


                    //Find Employee
                    Employee _emp = db.Employees.Find(deptHead.Id);

                    RequisitionForm _rf = db.RequisitionForms
                        .Where(x => x.Id == rf.Id)
                        .FirstOrDefault();

                    _rf.RFStatus = RFStatus.Approved;
                    _rf.RFHeadReply = comment;
                    _rf.RFApprovalDate = DateTime.Now;
                    _rf.RFApprovalBy = _emp;
                    db.RequisitionForms.Update(_rf);

                    

                    List<RequisitionFormsProduct> _rfpList = new List<RequisitionFormsProduct>();


                    foreach (RequisitionFormsProduct rfp in rfpList)
                    {
                        RequisitionFormsProduct _rfp = db.RequisitionFormsProducts.Where(x => x.Id == rfp.Id).FirstOrDefault();
                        _rfp.ProductApproved = rfp.ProductApproved;
                        db.RequisitionFormsProducts.Update(_rfp);
                        _rfpList.Add(_rfp);
                    }

                    //Check if all products are not null
                    bool isAllProductsQuantityLessThanZero = true;

                    foreach (RequisitionFormsProduct _rfp in _rfpList)
                    {
                        if (_rfp.ProductApproved >= 0)
                        {
                            isAllProductsQuantityLessThanZero = false;
                            break;
                        }
                    }

                    if (isAllProductsQuantityLessThanZero == true)
                    {
                        throw new Exception("All items cannot be 0");
                    }

                    db.SaveChanges();
                    transcat.Commit();

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("IMS System", "silentxtwilight2@gmail.com"));
                    message.To.Add(new MailboxAddress(_rf.Employee.Firstname + " " + _rf.Employee.Lastname, _rf.Employee.Email));
                    message.Subject = "[ " + _rf.RFCode + " ] Status: Approved";
                    message.Body = new TextPart("plain")
                    {
                        Text = "[ " + _rf.RFCode + " ]: Approved" +
                        "\nDepartment Head Comments: " + _rf.RFHeadReply
                    };
                    EmailUtil eUtil = new EmailUtil();
                    eUtil.SendEmail(message);

                    return true;
                }
                catch
                {
                    transcat.Rollback();
                    return false;
                }
            }

        }
        public bool ChangeRFStatusToRejectedById(RequisitionForm rf, Employee deptHead, string comment)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    //Find Employee
                    Employee _emp = db.Employees.Find(deptHead.Id);

                    //Check for comment if reject
                    if (comment == null || comment == "")
                    {
                        throw new Exception("There is no reply");
                    }

                    //Find the RF
                    RequisitionForm _rf = db.RequisitionForms.Find(rf.Id);

                    _rf.RFStatus = RFStatus.Rejected;
                    _rf.RFHeadReply = comment;
                    _rf.RFApprovalDate = DateTime.Now;
                    _rf.RFApprovalBy = _emp;
                    db.RequisitionForms.Update(_rf);
                    
                    db.SaveChanges();
                    transcat.Commit();

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("IMS System", "silentxtwilight2@gmail.com"));
                    message.To.Add(new MailboxAddress(_rf.Employee.Firstname + " " + _rf.Employee.Lastname, _rf.Employee.Email));
                    message.Subject = "[ " + _rf.RFCode + " ] Status: Rejected";
                    message.Body = new TextPart("plain")
                    {
                        Text = "[ " + _rf.RFCode + " ]: Approved" +
                        "\n Department Head Comments: " + _rf.RFHeadReply
                    };
                    EmailUtil eUtil = new EmailUtil();
                    eUtil.SendEmail(message);

                    return true;
                }
                catch
                {
                    transcat.Rollback();
                    return false;
                }
            }

        }

        public List<RequisitionForm> FindRequisitionFormByDeptToday(Employee empCheck)
        {
            List<RequisitionForm> rfCheckList = db.RequisitionForms
                    .Where(x => x.RFDate.Date == DateTime.Now.Date)
                    .Where(x => x.Employee.Department == empCheck.Department)
                    .ToList();
            return rfCheckList;
        }

        public RequisitionForm FindRequisitionFormById(int id)
        {
            RequisitionForm rf = db.RequisitionForms
                .Where(x => x.Id == id)
                .FirstOrDefault();
            return rf;
        }

        public List<RequisitionFormsProduct> FindRequisitionFormProductListById(int id)
        {
            List<RequisitionFormsProduct> rfpList = db.RequisitionFormsProducts
                .Where(x => x.RequisitionForm.Id == id)
                .ToList();
            return rfpList;
        }

        //Yamone
        public List<TrendDeptAndRequisitionByMonth> GetTotalReqForEachDeptForTrend(List<int> deptlist)
        {
            DateTime today = DateTime.Now;
            int thismonth = today.Month;
            int lastonemonth = today.AddMonths(-1).Month;
            int lasttwomonth = today.AddMonths(-2).Month;

            List<int> monthlist = new List<int>();
            monthlist.Add(thismonth);
            monthlist.Add(lastonemonth);
            monthlist.Add(lasttwomonth);

            List<TrendDeptAndRequisitionByMonth> tdrqlistmth = new List<TrendDeptAndRequisitionByMonth>();

            if(monthlist.Count != 0)
            {
                foreach (var month in monthlist)
                {
                    List<RequisitionForm> rflist_bymonth = db.RequisitionForms.Where(x => x.RFApprovalDate.Month == month).Where(x => x.RFStatus == Enums.RFStatus.Approved || x.RFStatus == Enums.RFStatus.Completed
               || x.RFStatus == Enums.RFStatus.NotCompleted || x.RFStatus == Enums.RFStatus.Ongoing).ToList();

                    if (rflist_bymonth != null)
                    {
                        var resultdept = rflist_bymonth.GroupBy(x => x.Employee.Department,
                        (baseDept, depts) => new
                        {
                            Key = baseDept,
                            Count = depts.Count(),

                        });

                        List<TrendDeptAndRequisition> tdrqlist = new List<TrendDeptAndRequisition>();

                        foreach (var dep in resultdept)
                        {
                            if (deptlist.Contains(dep.Key.Id))
                            {
                                TrendDeptAndRequisition tdept_req = new TrendDeptAndRequisition();
                                tdept_req.deptname = dep.Key.DepartmentName;
                                tdept_req.qtyrequisition = dep.Count;
                                tdrqlist.Add(tdept_req);
                            }
                        }

                        TrendDeptAndRequisitionByMonth tdept_reqmth = new TrendDeptAndRequisitionByMonth();
                        tdept_reqmth.month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month);
                        tdept_reqmth.deptreqlist = tdrqlist;
                        tdrqlistmth.Add(tdept_reqmth);
                    }
                    else
                    {

                    }
                }
            }

            return tdrqlistmth;

        }
        public bool SaveRFFromCSV(List<CSVRequisitionForm> rfList)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (CSVRequisitionForm rf in rfList)
                    {
                        RequisitionForm rfSave = new RequisitionForm();
                        rfSave.RFCode = rf.RFCode;
                        rfSave.Employee = empService.GetEmployee(rf.Username);
                        rfSave.RFDate = DateTime.Parse(rf.RFDate);
                        rfSave.RFStatus = (RFStatus)Enum.Parse(typeof(RFStatus), rf.RFStatus);
                        rfSave.RFComments = rf.RFComments;
                        rfSave.RFApprovalDate = DateTime.Parse(rf.RFApprovalDate);
                        rfSave.RFApprovalBy = empService.GetEmployee(rf.RFApprovalUsername);
                        rfSave.RFHeadReply = rf.RFHeadReply;
                        db.RequisitionForms.Add(rfSave);
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
