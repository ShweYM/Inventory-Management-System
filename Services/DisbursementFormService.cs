using Inventory_Management_System.DB;
using Inventory_Management_System.Enums;
using Inventory_Management_System.Models;
using Inventory_Management_System.Utils;
using Microsoft.EntityFrameworkCore.Storage;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Services
{
    public class DisbursementFormService
    {
        protected InventoryManagementSystemContext db;

        public DisbursementFormService(InventoryManagementSystemContext db)
        {
            this.db = db;
        }

        public List<DisbursementForm> FindDisbursementForms()
        {
            return db.DisbursementForms.ToList();
        }

        public List<DisbursementForm> FindCreatedDisbursementForms()
        {
            return db.DisbursementForms.Where(x => x.DFStatus == DFStatus.Created).ToList();
        }

        public List<DisbursementForm> FindCreatedDisbursementFormsByDept(Employee emp)
        {
            return db.DisbursementForms.Where(x => x.DFStatus == DFStatus.Created)
                .Where(x => x.DeptRep.Department == emp.Department)

                .ToList();
        }

        public List<DisbursementForm> FindPendingAssignDisbursementForms()
        {
            return db.DisbursementForms.Where(x => x.DFStatus == DFStatus.PendingAssignment).ToList();
        }
        public List<DisbursementForm> FindPendingAssignDisbursementFormsByDept(Employee emp)
        {
            return db.DisbursementForms
                .Where(x => x.DFStatus == DFStatus.PendingAssignment)
                .Where(x => x.DeptRep.Department == emp.Department)
                .ToList();
        }
        public List<DisbursementForm> FindPendingDeliveryDisbursementForms()
        {
            return db.DisbursementForms
                .Where(x => x.DFStatus == DFStatus.PendingDelivery)
                .ToList();
        }
        public List<DisbursementForm> FindPendingDeliveryDisbursementFormsByDept(Employee emp)
        {
            return db.DisbursementForms
                .Where(x => x.DFStatus == DFStatus.PendingDelivery)
                .Where(x => x.DeptRep.Department == emp.Department)
                .ToList();
        }

        public List<DisbursementForm> FindCompletedDisbursementForms()
        {
            return db.DisbursementForms
                .Where(x => x.DFStatus == DFStatus.Completed)
                .ToList();
        }
        public List<DisbursementForm> FindCompletedDisbursementFormsByDept(Employee emp)
        {
            return db.DisbursementForms
                .Where(x => x.DFStatus == DFStatus.Completed)
                .Where(x => x.DeptRep.Department == emp.Department)
                .ToList();
        }

        public List<DisbursementFormRequisitionFormProduct> FindDFRFPListByRFId (int id)
        {
            List<DisbursementFormRequisitionFormProduct> dfrfpList = db.DisbursementFormRequisitionFormProducts
                .OrderBy(x => x.RequisitionFormsProduct.Product)
                .OrderByDescending(x=>x.DisbursementForm.DFHandoverDate)
                .Where(x => x.RequisitionFormsProduct.RequisitionForm.Id == id)
                .Where(x=>x.DisbursementForm.DFStatus == DFStatus.Completed)
                .ToList();
            return dfrfpList;
        }

        public List<DisbursementFormRequisitionForm> FindDFRFListByRFId(int id)
        {
            List<DisbursementFormRequisitionForm> dfrfList = db.DisbursementFormRequisitionForms
                .Where(x => x.RequisitionForm.Id == id)
                .Where(x=>x.DisbursementForm.DFStatus == DFStatus.Completed)
                .ToList();
            return dfrfList;
        }

        public DisbursementForm FindDisbursementFormById(int id)
        {
            return db.DisbursementForms.Where(model => model.Id == id).FirstOrDefault();
        }

        public DisbursementForm FindDisbursementFormByDFCode(String dfCode)
        {
            return db.DisbursementForms.Where(model => model.DFCode == dfCode).FirstOrDefault();
        }

        public DisbursementForm FindDisbursementFormById(int id, Employee emp)
        {
            return db.DisbursementForms
                .Where(model => model.Id == id)
                .Where(model => model.DeptRep.Department == emp.Department)
                .FirstOrDefault();
        }

        public bool CreateDisbursementForm(List<StationeryRetrievalRequisitionForm> srrfList, Employee emp, string comment)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    DateTime createDate = DateTime.Now;

                    //find employee
                    Employee _emp = db.Employees.Find(emp.Id);

                    //Create empty df List to save
                    List<DisbursementForm> _dfList = new List<DisbursementForm>();

                    //Create empty list of srrf
                    List<StationeryRetrievalRequisitionForm> _srrfList = new List<StationeryRetrievalRequisitionForm>();

                    //Find all SRRF and provide a full mapping properly
                    foreach (StationeryRetrievalRequisitionForm srrf in srrfList)
                    {
                        StationeryRetrievalRequisitionForm _srrf = db.StationeryRetrievalRequisitionForms.Find(srrf.Id);
                        _srrfList.Add(_srrf);
                    }

                    //Find all the distinct Departments
                    IEnumerable<Department> deptList = _srrfList.Select(x => x.RequisitionForm.Employee.Department).Distinct();

                    foreach (Department dept in deptList)
                    {
                        //Dict to check for Total Product Approved Based on a particular product
                        Dictionary<int, int> _prodCountCheckDict1 = new Dictionary<int, int>();
                        //Dict to check for Total Product Collected Based on a particular product
                        Dictionary<int, int> _prodCountCheckDict2 = new Dictionary<int, int>();

                        //Find the total of DOs before adding NEW DO
                        int count = FindDFsByDateTodayAndDept(dept);
                        count++;
                        
                        //Create empty dfp List to save
                        List<DisbursementFormProduct> _dfpList = new List<DisbursementFormProduct>();

                        //Create an empty dfrflist to save
                        List<DisbursementFormRequisitionForm> _dfrfList = new List<DisbursementFormRequisitionForm>();

                        //Create an empty dfrflist to save
                        List<DisbursementFormRequisitionFormProduct> _dfrfpList = new List<DisbursementFormRequisitionFormProduct>();

                        //Find Filtered list of srrf based on dept
                        List<StationeryRetrievalRequisitionForm> _srrfListFilteredByDept = _srrfList
                            .Where(x => x.RequisitionForm.Employee.Department == dept)
                            .ToList();

                        DateTime deliveryDate = createDate.Date.AddDays(1);
                        if(dept.CollectionPoint.CollectionName == "Stationery Store - Administration Building")
                        {
                            deliveryDate = deliveryDate.AddHours(9).AddMinutes(30);
                        }
                        else if (dept.CollectionPoint.CollectionName == "Management School")
                        {
                            deliveryDate = deliveryDate.AddHours(11).AddMinutes(0);

                        }
                        else if (dept.CollectionPoint.CollectionName == "Medical School")
                        {
                            deliveryDate = deliveryDate.AddHours(9).AddMinutes(30);

                        }
                        else if (dept.CollectionPoint.CollectionName == "Engineering School")
                        {
                            deliveryDate = deliveryDate.AddHours(11).AddMinutes(0);

                        }
                        else if (dept.CollectionPoint.CollectionName == "Science School")
                        {
                            deliveryDate = deliveryDate.AddHours(9).AddMinutes(30);

                        }
                        else if (dept.CollectionPoint.CollectionName == "University Hospital")
                        {
                            deliveryDate = deliveryDate.AddHours(11).AddMinutes(0);
                        }

                        //Assign a DF Code
                        string dfCode = "DF" + "/" + dept.DeptCode + "/" + createDate.ToString("ddMMyy") + "/" + count.ToString();

                        //Create a DF
                        DisbursementForm _df = new DisbursementForm()
                        {
                            DFDate = createDate,
                            DFStatus = DFStatus.Created,
                            DFCode = dfCode,
                            DFComments = comment,
                            DFDeliveryDate = deliveryDate,
                            DeptRep = db.Employees
                            .Where(x => x.Department == dept)
                            .Where(x => x.EmployeeType.EmployeeTypeName == "Department Representative")
                            .First(),
                            //DeptRep = db.Employees.Find(dept.DeptRepId),
                            CollectionPoint = dept.CollectionPoint,
                            StoreClerk = _emp
                        };

                        db.DisbursementForms.Add(_df);
                        _dfList.Add(_df);
                        db.SaveChanges();

                        //Find All distinct RFs in filtered srrfList
                        IEnumerable<RequisitionForm> rfListFilteredByDept = _srrfListFilteredByDept.Select(x => x.RequisitionForm).Distinct();

                        //Create a consolidated RFPList based on all rf in a dept
                        List<RequisitionFormsProduct> rfpConsolidatedListFilteredByDept = new List<RequisitionFormsProduct>();

                        foreach (RequisitionForm rfFilteredByDept in rfListFilteredByDept)
                        {
                            DisbursementFormRequisitionForm _dfrf = new DisbursementFormRequisitionForm()
                            {
                                DisbursementForm = _df,
                                RequisitionForm = rfFilteredByDept,
                                DFRFStatus = DFRFStatus.PendingDelivery
                            };
                            _dfrfList.Add(_dfrf);
                            db.DisbursementFormRequisitionForms.Add(_dfrf);
                            db.SaveChanges();

                            //Find RFPList based on RF
                            List<RequisitionFormsProduct> rfpListFilteredByDept = db.RequisitionFormsProducts
                                .Where(x => x.RequisitionForm == rfFilteredByDept)
                                .ToList();

                            //Find All RFP in a dept and add it to the consolidated list
                            foreach(RequisitionFormsProduct rfpFilteredByDept in rfpListFilteredByDept)
                            {
                                rfpConsolidatedListFilteredByDept.Add(rfpFilteredByDept);
                            }
                        }

                        //Find All independent Products in consolidated RFP in a dept
                        IEnumerable<Product> pListFilteredByDept = rfpConsolidatedListFilteredByDept
                            .Select(x => x.Product).Distinct();

                        

                        //Create DFRFP based on RFP
                        foreach (RequisitionFormsProduct rfpConsolidatedFilteredByDept in rfpConsolidatedListFilteredByDept)
                        {
                            DisbursementFormRequisitionFormProduct _dfrfp = new DisbursementFormRequisitionFormProduct()
                            {
                                DisbursementForm = _df,
                                RequisitionFormsProduct = rfpConsolidatedFilteredByDept,
                                ProductCollected = 0
                            };
                            _dfrfpList.Add(_dfrfp);
                            db.DisbursementFormRequisitionFormProducts.Add(_dfrfp);
                            db.SaveChanges();
                        }

                        //Assign product as key to dictionary and set default values to 0
                        foreach (Product p in pListFilteredByDept)
                        {
                            _prodCountCheckDict1[p.Id] = 0;
                            _prodCountCheckDict2[p.Id] = 0;
                        }

                        //Find All SRRFP pertaining to this DF
                        List<StationeryRetrievalRequisitionFormProduct> _srrfpList = new List<StationeryRetrievalRequisitionFormProduct>();

                        List<StationeryRetrieval> _srList = new List<StationeryRetrieval>();

                        IEnumerable<RequisitionForm> _rfListFilteredByDept = _srrfListFilteredByDept.Select(x => x.RequisitionForm).Distinct();
                        IEnumerable<StationeryRetrieval> _srListFilteredByDept = _srrfListFilteredByDept.Select(x => x.StationeryRetrieval).Distinct();
                        
                        foreach (StationeryRetrieval _srFilteredByDept in _srListFilteredByDept)
                        {
                            List<StationeryRetrievalRequisitionFormProduct> _srrfpListIndividual = db.StationeryRetrievalRequisitionFormProducts
                                .Where(x => x.SR == _srFilteredByDept)
                                .ToList();
                            foreach (RequisitionForm _rfFilteredByDept in _rfListFilteredByDept)
                                _srrfpListIndividual = _srrfpListIndividual
                                    .Where(x => x.RFP.RequisitionForm == _rfFilteredByDept)
                                    .ToList();
                            foreach (StationeryRetrievalRequisitionFormProduct _srrfpIndividual in _srrfpListIndividual)
                            {
                                _srrfpList.Add(_srrfpIndividual);
                            }    

                        }

                        //Create DFP from Ienumerable products
                        foreach (Product p in pListFilteredByDept)
                        {
                            int prodToDelivery = 0;

                            foreach (RequisitionFormsProduct rfpConsolidatedFilteredByDept in rfpConsolidatedListFilteredByDept)
                            {
                                if (p.Id == rfpConsolidatedFilteredByDept.Product.Id)
                                {
                                    _prodCountCheckDict1[p.Id] = _prodCountCheckDict1[p.Id] + rfpConsolidatedFilteredByDept.ProductApproved;
                                    _prodCountCheckDict2[p.Id] = _prodCountCheckDict2[p.Id] + rfpConsolidatedFilteredByDept.ProductCollectedTotal;
                                }
                            }

                            foreach (StationeryRetrievalRequisitionFormProduct _srrfp in _srrfpList)
                            {
                                if (p.Id == _srrfp.RFP.Product.Id)
                                {
                                    prodToDelivery = prodToDelivery + _srrfp.ProductAssigned;
                                }
                            }

                            //CheckProductToReceive

                            DisbursementFormProduct _dfp = new DisbursementFormProduct()
                            {
                                DisbursementForm = _df,
                                Product = p,
                                ProductToDeliverTotal = prodToDelivery
                            };

                            _dfpList.Add(_dfp);
                            db.DisbursementFormProducts.Add(_dfp);
                            db.SaveChanges();
                        }

                        ////Create DFP from Ienumerable products
                        //foreach (Product p in pListFilteredByDept)
                        //{
                        //    foreach (RequisitionFormsProduct rfpConsolidatedFilteredByDept in rfpConsolidatedListFilteredByDept)
                        //    {
                        //        if (p.Id == rfpConsolidatedFilteredByDept.Product.Id)
                        //        {
                        //            _prodCountCheckDict1[p.Id] = _prodCountCheckDict1[p.Id] + rfpConsolidatedFilteredByDept.ProductApproved;
                        //            _prodCountCheckDict2[p.Id] = _prodCountCheckDict2[p.Id] + rfpConsolidatedFilteredByDept.ProductCollectedTotal;
                        //        }
                        //    }

                        //    DisbursementFormProduct _dfp = new DisbursementFormProduct()
                        //    {
                        //        DisbursementForm = _df,
                        //        Product = p,
                        //        ProductToDeliverTotal = _prodCountCheckDict1[p.Id] - _prodCountCheckDict2[p.Id]
                        //    };

                        //    _dfpList.Add(_dfp);
                        //    db.DisbursementFormProducts.Add(_dfp);
                        //    db.SaveChanges();
                        //}
                    }

                    foreach (StationeryRetrievalRequisitionForm _srrf in _srrfList)
                    {
                        _srrf.SRRFStatus = SRRFStatus.Completed;
                        db.StationeryRetrievalRequisitionForms.Update(_srrf);
                        db.SaveChanges();
                    }

                    transcat.Commit();
                    foreach (DisbursementForm _df in _dfList)
                    {
                        var message = new MimeMessage();
                        message.From.Add(new MailboxAddress("IMS System", "silentxtwilight2@gmail.com"));
                        message.To.Add(new MailboxAddress(_df.DeptRep.Firstname + " " + _df.DeptRep.Lastname, _df.DeptRep.Email));
                        message.Subject = "[ " + _df.DFCode + " ] Status: Created";
                        message.Body = new TextPart("plain")
                        {
                            Text = "[ " + _df.DFCode + " ]: Created" +
                            "\nDisbursement From Comments: " + _df.DFComments
                        };
                        EmailUtil eUtil = new EmailUtil();
                        eUtil.SendEmail(message);
                    }
                    return true;
                }
                catch
                {
                    transcat.Rollback();
                    return false;
                }
            }
        }


        public int FindDFsByDateToday()
        {
            return db.DisbursementForms
                .Where(x => x.DFDate.Date == DateTime.Today.Date)
                .Count();
        }

        public int FindDFsByDateTodayAndDept(Department dept)
        {
            return db.DisbursementForms
                .Where(x => x.DFDate.Date == DateTime.Today.Date)
                .Where(x => x.DeptRep.Department == dept)
                .Count();
        }

        public List<DisbursementFormProduct> SaveDfP(DisbursementForm df)
        {
            Dictionary<int, int> _prodCountCheckDict = new Dictionary<int, int>();

            List< DisbursementFormProduct > _dfpList = new List<DisbursementFormProduct>();

            List<DisbursementFormRequisitionFormProduct> _dfrfpList = db.DisbursementFormRequisitionFormProducts
                .Where(x => x.DisbursementForm == df)
                .ToList();
            IEnumerable<Product> prodList = _dfrfpList.Select(x => x.RequisitionFormsProduct.Product).Distinct();

            return _dfpList;
        }

        public bool ConfirmDFDelivery(Employee deptrep, DisbursementForm df)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    Employee _deptrep = db.Employees.Find(deptrep.Id);
                    DisbursementForm _df = db.DisbursementForms.Find(df.Id);
                    //_df.DFDeliveryDate = ;
                    _df.DFStatus = Enums.DFStatus.PendingDelivery;
                    _df.DeptRep = _deptrep;
                    db.DisbursementForms.Update(_df);
                    db.SaveChanges();

                    transcat.Commit();

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("IMS System", "silentxtwilight2@gmail.com"));
                    message.To.Add(new MailboxAddress(_df.StoreClerk.Firstname + " " + _df.StoreClerk.Lastname, _df.StoreClerk.Email));
                    message.Subject = "[ " + _df.DFCode + " ] Status: Confirmed";
                    message.Body = new TextPart("plain")
                    {
                        Text = "[ " + _df.DFCode + " ]: Confirmed" +
                        "\nCollection Point: " + _df.CollectionPoint.CollectionName
                        +
                        "\nDelivery Date: " + _df.DFDeliveryDate
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
            };
        }
        public bool ConfirmDFTransactionWithDeptRep(DisbursementForm df, List<DisbursementFormProduct> dfpList, Employee storeclerk, Employee deptrep, string comment)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    //Assign date time now
                    DateTime handoverDate = DateTime.Now;

                    //find storeclerk
                    Employee sc = db.Employees
                    .Where(x => x.Username == storeclerk.Username.ToLower())
                    .Where(x => x.Password == storeclerk.Password)
                    .FirstOrDefault();

                    //find deptrep
                    Employee dp = db.Employees
                    .Where(x => x.Username == deptrep.Username.ToLower())
                    .Where(x => x.Password == deptrep.Password)
                    .FirstOrDefault();

                    //Check if username and password is valid
                    if (sc == null || dp == null)
                    {
                        throw new Exception("Username is invalid");
                    }

                    if (dp.EmployeeType.EmployeeTypeName != "Department Representative")
                    {
                        throw new Exception("The user type is wrong");
                    }

                    if (sc.EmployeeType.EmployeeTypeName != "Store Clerk" && sc.EmployeeType.EmployeeTypeName != "Store Supervisor" && sc.EmployeeType.EmployeeTypeName != "Store Manager")
                    {
                        throw new Exception("The user type is wrong");
                    }
                    //Find df
                    DisbursementForm _df = db.DisbursementForms.Find(df.Id);


                    //Create an empty dfpList to save
                    List<DisbursementFormProduct> _dfpList = new List<DisbursementFormProduct>();


                    foreach (DisbursementFormProduct dfp in dfpList)
                    {
                        DisbursementFormProduct _dfp = db.DisbursementFormProducts.Find(dfp.Id);
                        _dfp.ProductDeliveredTotal = dfp.ProductDeliveredTotal;
                        db.DisbursementFormProducts.Update(_dfp);
                        _dfpList.Add(_dfp);
                        db.SaveChanges();

                    }

                    foreach (DisbursementFormProduct _dfp in _dfpList)
                    {
                        if (_dfp.ProductToDeliverTotal < _dfp.ProductDeliveredTotal)
                        {
                            throw new Exception("You cannot input a value more than the number that is to deliver");
                        }
                    }
                    //Update DF Status
                    _df.DeptRep = dp;
                    _df.StoreClerk = sc;
                    _df.DFStatus = Enums.DFStatus.PendingAssignment;
                    _df.DFHandoverDate = handoverDate;
                    _df.DFComments = comment;
                    db.SaveChanges();

                    //Change all DFRF Status to pending assignment
                    List<DisbursementFormRequisitionForm> _dfrfList = db.DisbursementFormRequisitionForms
                        .Where(x => x.DisbursementForm == _df)
                        .ToList();

                    //Change all DFRF StatuS to pending assignment
                    foreach (DisbursementFormRequisitionForm _dfrf in _dfrfList)
                    {
                        _dfrf.DFRFStatus = DFRFStatus.PendingAssignment;
                        db.DisbursementFormRequisitionForms.Update(_dfrf);
                        db.SaveChanges();
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

        public bool SaveDFRFPAssignment(
            List<DisbursementFormRequisitionFormProduct> dfrfpList, 
            Employee emp, 
            DisbursementForm df,
            List<DisbursementFormProduct> dfpList,
            List<DisbursementFormRequisitionForm> dfrfList)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    //Assign employee to be used for DFAssignedby
                    Employee _emp = db.Employees.Find(emp.Id);

                    //Create RF List
                    List<RequisitionForm> _rfList = new List<RequisitionForm>();
                    List<RequisitionFormsProduct> _rfpList = new List<RequisitionFormsProduct>();

                    Dictionary<int, int> _prodCountCheckDict = new Dictionary<int, int>();

                    DateTime dfTransactAssignDate = DateTime.Now;

                    //Find DF
                    DisbursementForm _df = db.DisbursementForms.Find(df.Id);

                    //Create empty lists
                    List<DisbursementFormProduct> _dfpList = new List<DisbursementFormProduct>();
                    List<DisbursementFormRequisitionForm> _dfrfList = new List<DisbursementFormRequisitionForm>();
                    List<DisbursementFormRequisitionFormProduct> _dfrfpList = new List<DisbursementFormRequisitionFormProduct>();

                    //Add dfp to dfpList
                    foreach (DisbursementFormProduct dfp in dfpList)
                    {
                        DisbursementFormProduct _dfp = db.DisbursementFormProducts.Find(dfp.Id);
                        _dfpList.Add(_dfp);
                    }
                    
                    //Add dfrf to dfrfList
                    foreach (DisbursementFormRequisitionForm dfrf in dfrfList)
                    {
                        DisbursementFormRequisitionForm _dfrf = db.DisbursementFormRequisitionForms.Find(dfrf.Id);
                        _dfrfList.Add(_dfrf);
                        _rfList.Add(_dfrf.RequisitionForm);

                        //Find all individual rfpList
                        List<RequisitionFormsProduct> _rfpIndividualList = db.RequisitionFormsProducts
                            .Where(x => x.RequisitionForm == _dfrf.RequisitionForm)
                            .ToList();

                        //add all rfp to a rfplist
                        foreach (RequisitionFormsProduct _rfpIndividual in _rfpIndividualList)
                        {
                            _rfpList.Add(_rfpIndividual);
                        }
                    }

                    //add dfrfp to dfrfpList
                    foreach (DisbursementFormRequisitionFormProduct dfrfp in dfrfpList)
                    {
                        DisbursementFormRequisitionFormProduct _dfrfp = db.DisbursementFormRequisitionFormProducts.Find(dfrfp.Id);
                        _dfrfpList.Add(_dfrfp);
                    }

                    //Check total quantity of each product
                    foreach (DisbursementFormProduct _dfp in _dfpList)
                    {
                        _prodCountCheckDict[_dfp.Product.Id] = 0;
                        foreach (DisbursementFormRequisitionFormProduct dfrfp in dfrfpList)
                        {
                            if (_dfp.Product.Id == dfrfp.RequisitionFormsProduct.Product.Id)
                            {
                                _prodCountCheckDict[_dfp.Product.Id] = _prodCountCheckDict[_dfp.Product.Id] + dfrfp.ProductCollected;
                            }
                        }
                        
                    }
                    //Check each key
                    foreach (KeyValuePair<int, int> entry in _prodCountCheckDict)
                    {
                        for (int i = 0; i < _dfpList.Count; i++)
                        {
                            if (entry.Key == _dfpList[i].Product.Id)
                            {
                                if (entry.Value != _dfpList[i].ProductDeliveredTotal)
                                {
                                    throw new Exception("The quantity doesnt match");
                                }
                            }
                        }
                    }

                    //AssignProductCollected to be save to db for dfrfp
                    for (int i = 0; i<_dfrfpList.Count;i++)
                    {
                        foreach (DisbursementFormRequisitionFormProduct dfrfp in dfrfpList)
                        {

                           if (_dfrfpList[i].Id == dfrfp.Id)
                            {
                                _dfrfpList[i].ProductCollected = dfrfp.ProductCollected;
                                db.DisbursementFormRequisitionFormProducts.Update(_dfrfpList[i]);
                                db.SaveChanges();
                                foreach (RequisitionFormsProduct _rfp in _rfpList)
                                {
                                    if (_dfrfpList[i].RequisitionFormsProduct.Id == _rfp.Id)
                                    {
                                        _rfp.ProductCollectedTotal = _rfp.ProductCollectedTotal + dfrfp.ProductCollected;
                                        db.RequisitionFormsProducts.Update(_rfp);
                                        db.SaveChanges();
                                    }
                                }
                                if (_dfrfpList[i].RequisitionFormsProduct.ProductApproved < _dfrfpList[i].RequisitionFormsProduct.ProductCollectedTotal)
                                {
                                    throw new Exception("The value have exceeded");
                                }

                            }
                        }
                        db.SaveChanges();
                    }


                    foreach (DisbursementFormRequisitionForm _dfrf in _dfrfList)
                    {
                        _dfrf.DFRFStatus = DFRFStatus.Delivered;
                        db.DisbursementFormRequisitionForms.Update(_dfrf);
                        db.SaveChanges();
                    }

                    _df.DFStatus = DFStatus.Completed;
                    _df.DFTransAssignDate = dfTransactAssignDate;
                    _df.DFAssignedBy = _emp;
                    db.DisbursementForms.Update(_df);
                    db.SaveChanges();

                    //Check for RF
                    foreach (RequisitionForm _rf in _rfList)
                    {
                        bool isCompleted = true;
                        foreach (RequisitionFormsProduct _rfp in _rfpList)
                        {
                            if (_rfp.RequisitionForm.Id == _rf.Id)
                            {
                                if (_rfp.ProductCollectedTotal != _rfp.ProductApproved)
                                {
                                    isCompleted = false;
                                    break;
                                }
                            }
                            
                        }

                        if (isCompleted == true)
                        {
                            _rf.RFStatus = RFStatus.Completed;
                        }
                        else
                        {
                            _rf.RFStatus = RFStatus.NotCompleted;
                        }
                        db.RequisitionForms.Update(_rf);
                        db.SaveChanges();
                    }

                    transcat.Commit();
                    foreach(RequisitionForm _rf in _rfList)
                    {
                        var message = new MimeMessage();
                        message.From.Add(new MailboxAddress("IMS System", "silentxtwilight2@gmail.com"));
                        message.To.Add(new MailboxAddress(_rf.Employee.Firstname + " " + _rf.Employee.Lastname, _rf.Employee.Email));
                        message.Subject = "[ " + _rf.RFCode + " ] Status: Delivered";
                        message.Body = new TextPart("plain")
                        {
                            Text = "[ " + _rf.RFCode + " ]: Delivered"  
                        };
                        EmailUtil eUtil = new EmailUtil();
                        eUtil.SendEmail(message);
                    }
                    
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
