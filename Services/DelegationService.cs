using Inventory_Management_System.DB;
using Inventory_Management_System.Enums;
using Inventory_Management_System.Models;
using Inventory_Management_System.Utils;
using Microsoft.EntityFrameworkCore.Storage;
using MimeKit;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Services
{
    public class DelegationService
    {
        private InventoryManagementSystemContext db;
        private EmployeeService empService;
        private EmployeeTypeService etService;

        public DelegationService(InventoryManagementSystemContext db, EmployeeService empService, EmployeeTypeService etService)
        {
            this.db = db;
            this.empService = empService;
            this.etService = etService;
        }

        public DelegationForm FindDelegationForm(int id)
        {
            return db.DelegationForms.Find(id);
        }

        public void CancelDLBasedOnId(int id)
        {
            DelegationForm _df = db.DelegationForms.Find(id);
            _df.DLStatus = DLStatus.Cancelled;
            db.DelegationForms.Update(_df);
            db.SaveChanges();
        }

        public List<DelegationForm> FindAllDelegations(Department dept)
        {
            DelegationForm _dListOngoing = db.DelegationForms
                .Where(x => x.DLStatus == DLStatus.Ongoing)
                .Where(x =>x.DepartmentHead.Department == dept)
                .First();

            List<DelegationForm> _dListAssigned = db.DelegationForms
               .OrderBy(x => x.startDate)
               .Where(x=>x.DLStatus == DLStatus.Assigned)
               .Where(x => x.DepartmentHead.Department == dept)
               .ToList();

            List<DelegationForm> _dListCompleted = db.DelegationForms
               .OrderByDescending(x => x.startDate)
               .Where(x => x.DLStatus == DLStatus.Completed)
               .Where(x => x.DepartmentHead.Department == dept)
               .ToList();

            List<DelegationForm> _dListCancelled = db.DelegationForms
               .OrderBy(x => x.startDate)
               .Where(x => x.DLStatus == DLStatus.Cancelled)
               .Where(x => x.DepartmentHead.Department == dept)
               .ToList();

            List<DelegationForm> _delList = new List<DelegationForm>();
            _delList.Add(_dListOngoing);
            
            foreach(DelegationForm _dList in _dListAssigned)
            {
                _delList.Add(_dList);
            }
            foreach (DelegationForm _dList in _dListCompleted)
            {
                _delList.Add(_dList);
            }
            foreach (DelegationForm _dList in _dListCancelled)
            {
                _delList.Add(_dList);
            }

            return _delList;
        }

        public bool CheckEmpIsTempDeptHeadToday(Employee emp)
        {
            //Basic status
            bool status = false;

            //Find Employee
            Employee _emp = db.Employees.Find(emp.Id);

            //Find Temp Dept Head Status current
            if (emp.TempDeptHeadType != null)
            {
                status = true;
            }

            //Check if any delegation form exist that matches in the system to be assigned as dept head
            DelegationForm dFormCheck = db.DelegationForms
                .Where(x => x.Delegatee.Id == _emp.Id)
                .Where(x => x.startDate <= DateTime.Now)
                .Where(x => x.endDate >= DateTime.Now)
                .Where(x => x.DelegatedType.EmployeeTypeName == "Temporary Department Head")
                .Where(x=>x.DLStatus != DLStatus.Cancelled && x.DLStatus != DLStatus.Completed)
                .FirstOrDefault();

            //If he exists before being a dept rept
            if (dFormCheck != null && status == false)
            {
                status = true;
                _emp.TempDeptHeadType = db.EmployeeTypes
                    .Where(x => x.EmployeeTypeName == "Temporary Department Head").FirstOrDefault();

                dFormCheck.DLStatus = DLStatus.Ongoing;

                db.DelegationForms.Update(dFormCheck);
                db.Employees.Update(_emp);
                
                db.SaveChanges();
            }
            else if (dFormCheck == null && status == true)
            {
                status = false;
                _emp.TempDeptHeadType = null;
                dFormCheck.DLStatus = DLStatus.Completed;

                db.DelegationForms.Update(dFormCheck);
                db.Employees.Update(_emp);
                db.SaveChanges();
            }
            return status;
        }
        
        public bool AssignDeptRep2(string comment, Employee delegatee, Employee deptHead)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    Employee _deptHead = db.Employees.Find(deptHead.Id);
                    Employee deptRepAssigned = db.Employees.Find(delegatee.Id);
                    Employee oldDeptRep = empService.FindEmployeeWhoIsDeptRep(deptRepAssigned.Department);
                    if (deptRepAssigned.EmployeeType.EmployeeTypeName == "Department Representative")
                    {
                        throw new Exception("This person is already a department representative");
                    }

                    deptRepAssigned.EmployeeType = etService.FindEmployeeTypeByEmployeeTypeName("Department Representative");
                    db.Employees.Update(deptRepAssigned);
                    db.SaveChanges();

                    Department dept = db.Departments.Find(deptRepAssigned.Department.Id);

                    dept.DeptRepId = deptRepAssigned.Id;
                    oldDeptRep.EmployeeType = etService.FindEmployeeTypeByEmployeeTypeName("Employee");
                    db.Employees.Update(oldDeptRep);

                    db.Departments.Update(dept);
                    db.SaveChanges();



                    DelegationForm _dForm = new DelegationForm()
                    {
                        DLAssignedDate = DateTime.Now,
                        DLStatus = DLStatus.Assigned,
                        delegateComment = comment,
                        DelegatedType = deptRepAssigned.EmployeeType,
                        Delegatee = deptRepAssigned,
                        DepartmentHead = _deptHead
                    };

                    db.DelegationForms.Add(_dForm);

                    db.SaveChanges();

                    transcat.Commit();

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("IMS System", "silentxtwilight2@gmail.com"));
                    message.To.Add(new MailboxAddress(deptRepAssigned.Firstname + " " + deptRepAssigned.Lastname, deptRepAssigned.Email));
                    message.Subject = "Department Representative Assignment:" + deptRepAssigned.Firstname + " " + deptRepAssigned.Lastname;
                    message.Body = new TextPart("plain")
                    {
                        Text = "Congratulations! You have an been appointed as the new Department Repesentative as starting now! "
                        
                    };
                    EmailUtil eUtil = new EmailUtil();
                    eUtil.SendEmail(message);

                    var message2 = new MimeMessage();
                    message2.From.Add(new MailboxAddress("IMS System", "silentxtwilight2@gmail.com"));
                    message2.To.Add(new MailboxAddress(oldDeptRep.Firstname + " " + oldDeptRep.Lastname, oldDeptRep.Email));
                    message2.Subject = "Department Representative Assignment:" + deptRepAssigned.Firstname + " " + deptRepAssigned.Lastname;
                    message2.Body = new TextPart("plain")
                    {
                        Text = "The Department Head have reassigned the department representative to " + deptRepAssigned.Firstname + " " + deptRepAssigned.Lastname

                    };
                    EmailUtil eUtil2 = new EmailUtil();
                    eUtil2.SendEmail(message2);
                    return true;
                }
                catch
                {
                    transcat.Rollback();
                    return false;
                }
            }
        }

        public bool AssignTempDeptHead(string comment, Employee delegatee, Employee deptHead, DateTime startDate, DateTime endDate)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {

                    Employee _depthead = db.Employees.Find(deptHead.Id);
                    
                    if (_depthead.EmployeeType.EmployeeTypeName != "Department Head")
                    {
                        throw new Exception("This is not dept head");
                    }

                    Employee _tempDeptHead = db.Employees.Find(delegatee.Id);

                    if (_tempDeptHead.EmployeeType.EmployeeTypeName == "Department Representative")
                    {
                        throw new Exception("You cannot choose dept rep");
                    }

                    EmployeeType _tempDeptHeadType = db.EmployeeTypes
                        .Where(x => x.EmployeeTypeName == "Temporary Department Head")
                        .First();
                    

                    bool dateCheck = DateCheck(startDate, endDate);
                    if (!dateCheck)
                    {
                        throw new Exception("startDate is smaller than endDate");
                    }
                    List<DelegationForm> _dCheckList = db.DelegationForms
                        .Where(x => x.Delegatee == _tempDeptHead)
                        .Where(x => x.startDate <= startDate)
                        .Where(x => x.endDate >= startDate)
                        .Where(x => x.DLStatus != DLStatus.Cancelled)
                        .ToList();
                    if (_dCheckList.Count != 0)
                    {
                        throw new Exception("Cannot assign again if it is within this date range");
                    }

                    DelegationForm _dForm = new DelegationForm()
                    {
                        DLAssignedDate = DateTime.Now,
                        DLStatus = DLStatus.Assigned,
                        endDate = endDate,
                        startDate = startDate,
                        delegateComment = comment,
                        DelegatedType = _tempDeptHeadType,
                        Delegatee = _tempDeptHead,
                        DepartmentHead = _depthead
                    };

                    
                    db.DelegationForms.Add(_dForm);
                    db.SaveChanges();
                    
                    transcat.Commit();

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("IMS System", "silentxtwilight2@gmail.com"));
                    message.To.Add(new MailboxAddress(_tempDeptHead.Firstname + " " + _tempDeptHead.Lastname, _tempDeptHead.Email));
                    message.Subject = "[New] Temporary Department Head Assignment:" + _tempDeptHead.Firstname + " " + _tempDeptHead.Lastname;
                    message.Body = new TextPart("plain")
                    {
                        Text = "Congratulations! You have an been appointed as the new Department Repesentative as starting now! " +
                        "\nStart Date: " + _dForm.startDate.ToString("dd-MMM-yyyy") +
                        "\nEnd Date: " + _dForm.endDate.ToString("dd-MMM-yyyy")

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

        public bool UpdateDLFormStartandEndDate(DelegationForm dForm)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    //Find Delegation Form
                    DelegationForm _dF = db.DelegationForms.Find(dForm.Id);

                    if (dForm.startDate != DateTime.MinValue)
                    {
                        _dF.startDate = dForm.startDate;
                    }
                    _dF.endDate = dForm.endDate;
                    //Must have some validation
                    if (dForm.endDate == DateTime.MinValue)
                    {
                        throw new Exception("This cannot be empty");
                    }
                    if (dForm.endDate < DateTime.Now)
                    {
                        throw new Exception("This cannot be earlier than Today");
                    }

                    if (dForm.endDate <dForm.startDate)
                    {
                        throw new Exception("End date cannot be earlier than start date");

                    }

                    //Find if this delegation clash with other delegations
                    List<DelegationForm> dfCheckList = db.DelegationForms
                    .Where(x => x.Delegatee == _dF.Delegatee)
                    .Where(x=> x.Id != _dF.Id)
                    .ToList();

                    foreach (DelegationForm dfcheck in dfCheckList)
                    {
                        if(dfcheck.startDate < dForm.startDate && dfcheck.endDate >dForm.endDate)
                        {
                            throw new Exception("It clashes with another  delegation form for the same employee");
                        }
                    }
                    db.DelegationForms.Update(_dF);
                    db.SaveChanges();

                    transcat.Commit();
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("IMS System", "silentxtwilight2@gmail.com"));
                    message.To.Add(new MailboxAddress(dForm.Delegatee.Firstname + " " + dForm.Delegatee.Lastname, dForm.Delegatee.Email));
                    message.Subject = "[UPDATED] Temporary Department Head Assignment:" + dForm.Delegatee.Firstname + " " + dForm.Delegatee.Lastname;
                    message.Body = new TextPart("plain")
                    {
                        Text = "The Start/End Date has been modified: " +
                        "\nStart Date: " + dForm.startDate.ToString("dd-MMM-yyyy") +
                        "\nEnd Date: " + dForm.endDate.ToString("dd-MMM-yyyy")

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

        public bool DateCheck(DateTime startDate, DateTime endDate)
        {
            DateTime today = DateTime.Now;
            int todayVSstartDate = DateTime.Compare(today, startDate);
            int startDateVSendDate = DateTime.Compare(startDate, endDate);
            if (todayVSstartDate >= 0 || startDateVSendDate >= 0)
            {
                return false;
            }

            return true;
        }

    }
}
