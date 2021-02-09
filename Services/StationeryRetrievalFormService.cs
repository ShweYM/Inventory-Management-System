using Inventory_Management_System.DB;
using Inventory_Management_System.Enums;
using Inventory_Management_System.Models;
using Inventory_Management_System.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Services
{
    public class StationeryRetrievalFormService
    {
        private InventoryManagementSystemContext db;
        private InventoryTransactionService invtService;
        private ProductService pService;
        private EmployeeService empService;

        public StationeryRetrievalFormService(InventoryManagementSystemContext db, InventoryTransactionService invtService, ProductService pService, EmployeeService empService)
        {
            this.db = db;
            this.invtService = invtService;
            this.pService = pService;
            this.empService = empService;
        }

        

        public StationeryRetrieval FindBySRId(int id)
        {
            return db.StationeryRetrievals.Find(id);
        }

        public List<StationeryRetrievalRequisitionForm> GetSRRFListBySRID(int id)
        {
            StationeryRetrieval sr = db.StationeryRetrievals.Find(id);
            List<StationeryRetrievalRequisitionForm> srrfList = db.StationeryRetrievalRequisitionForms
                .Where(x => x.StationeryRetrieval.Id == sr.Id)
                .ToList();
            return srrfList;
        }
        public Employee GetEmployee(string username, string password)
        {
            Employee emp = db.Employees
                .Where(x => x.Username == username && x.Password == password)
                .FirstOrDefault();

            return emp;
        }

        public List<RequisitionForm> GetRequisitionFormList()
        {
            List<RequisitionForm> reqlist = db.RequisitionForms.Where(x => x.RFStatus == RFStatus.Approved
            || x.RFStatus == RFStatus.NotCompleted).ToList();

            return reqlist;
        }
        
        
        public List<StationeryRetrieval> GetStationeryRetrievals()
        {
            List<StationeryRetrieval> retrievals = db.StationeryRetrievals.ToList();

            return retrievals;
        }

        //get the open or pending stationery retrievals //by htp
        public List<StationeryRetrieval> GetPendingStationeryRetrieval()
        {
            List<StationeryRetrieval> pendingRetrievals = db.StationeryRetrievals
                                                            .Where(x => x.SRStatus == SRStatus.PendingAssignment || x.SRStatus == Enums.SRStatus.Open)
                                                            .ToList();
            return pendingRetrievals;
        }

        public List<StationeryRetrieval> GetListSRWithSRStatus(SRStatus sRStatus)
        {
            List<StationeryRetrieval> srList = db.StationeryRetrievals.OrderBy(x=>x.SRDate)
                                                            .Where(x => x.SRStatus == sRStatus)
                                                            .ToList();
            return srList;
        }
        

        public List<RequisitionForm> FindRFListByRF(List<int> rfIntList)
        {
            List<RequisitionForm> _rfList = new List<RequisitionForm>();
            foreach (int rfId in rfIntList)
            {
                RequisitionForm _rf = db.RequisitionForms.Find(rfId);
                _rfList.Add(_rf);
            }
            return _rfList;
        }

        //Get selected Stationery retrieval details //by htp
        public StationeryRetrieval GetSRDetails(int SRid)
        {
            StationeryRetrieval retrievalDetails = db.StationeryRetrievals
                                                    .Where(x => x.Id == SRid)
                                                    .FirstOrDefault();
            return retrievalDetails;
        }


        //Get requisitions and their info included in one SR //by htp
        public List<RequisitionForm> GetRequisitionDetailBySR_RF(int SRid)
        {
            List<RequisitionForm> SRrequistionsInfo = (from rf in db.RequisitionForms
                                                       join sr_rf in db.StationeryRetrievalRequisitionForms
                                                       on rf.Id equals sr_rf.RequisitionForm.Id
                                                       where sr_rf.StationeryRetrieval.Id == SRid
                                                       select rf
                                                       ).ToList();

            return SRrequistionsInfo;
        }


        //Get the products included in one SR //by htp
        public List<StationeryRetrievalProduct> GetSRDetailProducts(int SRid)
        {
            List<StationeryRetrievalProduct> srpList = db.StationeryRetrievalProduct
                        .OrderBy(x => x.Product.ProductName)
                        .Where(x => x.StationeryRetrieval.Id == SRid)
                        .ToList();
            foreach (StationeryRetrievalProduct srp in srpList)
            {
                srp.ProductReceivedTotal = srp.ProductRequestedTotal;
            }

            return srpList;
        }
        public List<StationeryRetrievalProduct> GetSRPListForAssign(int SRid)
        {
            List<StationeryRetrievalProduct> srpList = db.StationeryRetrievalProduct
                        .OrderBy(x => x.Product.ProductName)
                        .Where(x => x.StationeryRetrieval.Id == SRid)
                        .ToList();
            
            return srpList;
        }
        //Yamone

        public List<RequisitionFormsProduct> GetReqFormProducts(int req_id)
        {
            List<RequisitionFormsProduct> reqformproducts = db.RequisitionFormsProducts.Where(x => x.RequisitionForm.Id == req_id).ToList();
            return reqformproducts;
        }

        //public int GetInventoryCount(string productname)
        //{
        //    Inventory product = db.Inventories.Where(x => x.Product.ProductName == productname).FirstOrDefault();
        //    return product.InventoryQuantity;
        //}

        public Employee getEmployee(String username)
        {
            Employee emp = db.Employees.Where(x => x.Username == username).FirstOrDefault();
            return emp;
        }

        public List<StationeryRetrieval> FindStationaryRetrievalFormByDeptToday(Employee emp)
        {
            List<StationeryRetrieval> srfCheckList = db.StationeryRetrievals
                    .Where(x => x.SRDate.Date == DateTime.Now.Date)
                    .Where(x => x.StoreClerk.Department == emp.Department)
                    .ToList();
            return srfCheckList;
        }
        public int FindStationaryRetrievalFormByDeptTodayCount(Employee emp)
        {
            int srfCheckListCount = db.StationeryRetrievals
                    .Where(x => x.SRDate.Date == DateTime.Now.Date)
                    .Where(x => x.StoreClerk.Department == emp.Department)
                    .Count();
            return srfCheckListCount;
;
        }
        public bool SaveStationaryRetrievalProducts(List<SRProductViewModel> productlist, Employee emp, string comment, List<int> selectedrequisiton, List<StationeryRetrievalProduct> srpList)
        {

            using (IDbContextTransaction transcat = db.Database.BeginTransaction())

            {
                try
                {
                    Employee _emp = db.Employees.Find(emp.Id);
                    List<RequisitionForm> rfList = FindRFListByRF(selectedrequisiton);
                    
                    //Create empty srrfList
                    List<StationeryRetrievalRequisitionForm> _srrfList = new List<StationeryRetrievalRequisitionForm>();

                    //Create empty rfpList
                    List<RequisitionFormsProduct> _rfpList = new List<RequisitionFormsProduct>();

                    //Create empty srrfpList
                    List<StationeryRetrievalRequisitionFormProduct> _srrfpList = new List<StationeryRetrievalRequisitionFormProduct>();

                    //Create empty srpList
                    List<StationeryRetrievalProduct> _srpList = new List<StationeryRetrievalProduct>();

                    //Find the list of rfs on that day by the same employee
                    int count = FindStationaryRetrievalFormByDeptTodayCount(_emp) + 1;
                    //Define RF Code
                    string srfCode = "SR" + "/" + DateTime.Now.ToString("ddMMyy") + "/" + count.ToString();

                    //Create New SRF and SRFP

                    //DateTime ddToday = DateTime.Now;
                    StationeryRetrieval srfForm = new StationeryRetrieval() 
                    { StoreClerk = _emp, 
                        SRCode = srfCode, 
                        SRStatus = SRStatus.Open, 
                        SRComments = comment, 
                        SRDate = DateTime.Now 
                    };

                    db.StationeryRetrievals.Add(srfForm);
                    db.SaveChanges();

                    foreach(RequisitionForm rf in rfList)
                    {
                        StationeryRetrievalRequisitionForm _srrf = new StationeryRetrievalRequisitionForm()
                        {
                            StationeryRetrieval = srfForm,
                            RequisitionForm = rf,
                            SRRFStatus = SRRFStatus.Open
                        };
                        db.Add(_srrf);
                        _srrfList.Add(_srrf);
                        db.SaveChanges();

                        List<RequisitionFormsProduct> individualrfpList = db.RequisitionFormsProducts
                            .Where(x=>x.RequisitionForm == rf)
                            .ToList();
                        foreach (RequisitionFormsProduct individualrfp in individualrfpList)
                        {
                            _rfpList.Add(individualrfp);
                        }
                    }

                    foreach (RequisitionFormsProduct _rfp in _rfpList)
                    {
                        StationeryRetrievalRequisitionFormProduct _srrfp = new StationeryRetrievalRequisitionFormProduct()
                        {
                            SR = srfForm,
                            RFP = _rfp,
                            ProductAssigned = 0
                        };
                        _srrfpList.Add(_srrfp);
                        db.Add(_srrfp);
                        db.SaveChanges();
                    }

                    if (productlist == null)
                    {
                        throw new Exception("No products to be stored as a Stationary Retrieval Form");
                    }
                    else
                    {
                        foreach (SRProductViewModel p in productlist)
                        {

                            Product product = db.Products.Where(x => x.ProductName == p.productname).FirstOrDefault();

                            StationeryRetrievalProduct _srfProduct = new StationeryRetrievalProduct();
                            _srfProduct.StationeryRetrieval = srfForm;
                            _srfProduct.Product = product;
                            _srfProduct.ProductRequestedTotal = p.productqty;
                            db.StationeryRetrievalProduct.Add(_srfProduct);
                            db.SaveChanges();

                        }
                        foreach (var requ_id in selectedrequisiton)
                        {
                            RequisitionForm requform = db.RequisitionForms.Where(x => x.Id == requ_id).FirstOrDefault();
                            requform.RFStatus = RFStatus.Ongoing;
                            db.SaveChanges();
                        }
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

        

        public bool saveReceivedProds3(List<StationeryRetrievalProduct> srpList, 
            int SRid, 
            Employee warehousepacker, 
            Employee storeclerk, 
            List<StationeryRetrievalRequisitionForm> srrfList)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    //Check for store clerk and warehouse packer
                    Employee _whpacker = empService.FindByUsernameAndPassword(warehousepacker.Username.ToLower(), warehousepacker.Password);
                    Employee _stclerk = empService.FindByUsernameAndPassword(storeclerk.Username.ToLower(), storeclerk.Password);
                    
                    if (_whpacker.EmployeeType.EmployeeTypeName != "Warehouse Packer")
                    {
                        throw new Exception("The user type is wrong");
                    }

                    if (_stclerk.EmployeeType.EmployeeTypeName != "Store Clerk" && _stclerk.EmployeeType.EmployeeTypeName != "Store Supervisor" && _stclerk.EmployeeType.EmployeeTypeName != "Store Manager")
                    {
                        throw new Exception("The user type is wrong");
                    }

                    if (_whpacker == null || _stclerk == null)
                    {
                        throw new Exception("The username or password is wrong, please login again");
                    }

                    DateTime transactDate = DateTime.Now;

                    //Create RF List
                    List<RequisitionForm> _rfList = new List<RequisitionForm>();

                    //Create new empty list to save to db
                    List<StationeryRetrievalProduct> _srpList = new List<StationeryRetrievalProduct>();

                    //Create new empty list to save to db
                    List<StationeryRetrievalRequisitionForm> _srrfList = new List<StationeryRetrievalRequisitionForm>();

                    //Find sr
                    StationeryRetrieval _sr = db.StationeryRetrievals.Find(SRid);
                    _sr.SRStatus = SRStatus.PendingAssignment;
                    _sr.WarehousePacker = _whpacker;
                    _sr.SRRetrievalDate = transactDate;
                    _sr.StoreClerk = _stclerk;
                    db.StationeryRetrievals.Update(_sr);
                    db.SaveChanges();

                    

                    //Define invtrans Code
                    int count = invtService.FindInvTransByTodayCount();

                    foreach (StationeryRetrievalProduct srp in srpList)
                    {
                        if(srp.ProductReceivedTotal <0)
                        {
                            throw new Exception("This value should not be negative");
                        }

                        count++;

                        StationeryRetrievalProduct _srp = db.StationeryRetrievalProduct.Find(srp.Id);
                        _srp.ProductReceivedTotal = srp.ProductReceivedTotal;
                        _srpList.Add(_srp);
                        db.StationeryRetrievalProduct.Update(_srp);
                        db.SaveChanges();

                        //Check for logic in terms of each RFP should not have more than what was specified


                        //create itCode
                        string invtransCode = "IT" + "/" + DateTime.Now.ToString("ddMMyy") + "/" + count.ToString();

                        //Create It
                        InventoryTransaction _it = new InventoryTransaction()
                        {
                            EmployeeId = _sr.StoreClerk.Id,
                            ProductId = _srp.Product.Id,
                            Employee = _sr.StoreClerk,
                            InventoryChangeQuantity = -_srp.ProductReceivedTotal,
                            InventoryTransComments = _sr.SRCode,
                            InventoryTransDate = _sr.SRRetrievalDate,
                            ITStatus = ITStatus.Auto,
                            ITCode = invtransCode,
                            Product = _srp.Product
                        };

                        db.InventoryTransactions.Add(_it);
                        db.SaveChanges();



                        //Adjust Inventory Quantity
                        Product _p = db.Products.Find(_srp.Product.Id);
                        _p.InventoryQuantity = _p.InventoryQuantity + _it.InventoryChangeQuantity;
                        if (_p.InventoryQuantity < 0)
                        {
                            throw new Exception(_p.InventoryQuantity + " is not enough");
                        }
                        db.Products.Update(_p);
                        db.SaveChanges();
                    }

                    foreach (StationeryRetrievalRequisitionForm srrf in srrfList)
                    {
                        StationeryRetrievalRequisitionForm _srrf = db.StationeryRetrievalRequisitionForms.Find(srrf.Id);
                        _srrf.SRRFStatus = SRRFStatus.PendingAssignment;
                        db.StationeryRetrievalRequisitionForms.Update(_srrf);
                        db.SaveChanges();

                        RequisitionForm _rf = db.RequisitionForms.Find(_srrf.RequisitionForm.Id);
                        _rf.RFStatus = RFStatus.Ongoing;
                        
                        db.RequisitionForms.Update(_rf);
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


        public List<StationeryRetrievalRequisitionFormProduct> FindSRRFPList(int SRId)
        {
            List<StationeryRetrievalRequisitionFormProduct> srrfpList = db.StationeryRetrievalRequisitionFormProducts
                .OrderBy(x => x.RFP.Product.ProductName)
                .Where(x => x.SR.Id == SRId)
                .ToList();
            return srrfpList;
        }

        public List<StationeryRetrievalRequisitionForm> FindSRRFList(int SRId)
        {
            List<StationeryRetrievalRequisitionForm> srrfList = db.StationeryRetrievalRequisitionForms
                .OrderBy(x => x.RequisitionForm.RFDate)
                .Where(x => x.StationeryRetrieval.Id == SRId)
                .ToList();
            return srrfList;
        }

        

        public bool SetProductsAssignedInSR(int SRId,
            List<StationeryRetrievalRequisitionFormProduct> srrfpList,
            List<StationeryRetrievalProduct> srpList,
            List<StationeryRetrievalRequisitionForm> srrfList)
        {
            using (IDbContextTransaction transcat = db.Database.BeginTransaction())
            {
                try
                {
                    
                    DateTime assignDate = DateTime.Now;
                    Dictionary<int, int> _prodCountCheckDict = new Dictionary<int, int>();

                    //Find SR
                    StationeryRetrieval _sr = db.StationeryRetrievals.Find(SRId);

                    //Find SRPList through db
                    List<StationeryRetrievalProduct> _srpList = new List<StationeryRetrievalProduct>();

                    //Find SRRFList through db
                    List<StationeryRetrievalRequisitionForm> _srrfList = new List<StationeryRetrievalRequisitionForm>();

                    //Find pList through db
                    List<Product> _pList = new List<Product>();

                    //Find pList through db
                    List<RequisitionForm> _rfList = new List<RequisitionForm>();

                    //Find rfpList through db
                    List<RequisitionFormsProduct> _rfpList = new List<RequisitionFormsProduct>();

                    //Find SRRFPList THROUGH db
                    List<StationeryRetrievalRequisitionFormProduct> _srrfpList = new List<StationeryRetrievalRequisitionFormProduct>();
                    
                    foreach (StationeryRetrievalProduct srp in srpList)
                    {
                        StationeryRetrievalProduct _srp = db.StationeryRetrievalProduct.Find(srp.Id);
                        _srp.ProductReceivedTotal = _srp.ProductReceivedTotal;
                        _srpList.Add(_srp);
                        _pList.Add(_srp.Product);
                    }

                    foreach (StationeryRetrievalRequisitionForm srrf in srrfList)
                    {
                        StationeryRetrievalRequisitionForm _srrf = db.StationeryRetrievalRequisitionForms.Find(srrf.Id);
                        _srrfList.Add(_srrf);
                        _rfList.Add(_srrf.RequisitionForm);
                    }

                    foreach (StationeryRetrievalRequisitionFormProduct srrfp in srrfpList)
                    {
                        if (srrfp.ProductAssigned <0)
                        {
                            throw new Exception("You cannot assign less than 0");
                        }
                        StationeryRetrievalRequisitionFormProduct _srrfp = db.StationeryRetrievalRequisitionFormProducts.Find(srrfp.Id);
                        if(_srrfp.RFP.ProductBalanceForSR < _srrfp.ProductAssigned)
                        {
                            throw new Exception("You cannot have a value less than the productApproved");
                        };
                        _srrfpList.Add(_srrfp);
                    }

                    foreach (RequisitionForm _rf in _rfList)
                    {
                        List<RequisitionFormsProduct> _rfpIndividualList = db.RequisitionFormsProducts
                            .Where(x => x.RequisitionForm == _rf)
                            .ToList();
                        foreach (RequisitionFormsProduct _rfpIndividual in _rfpIndividualList)
                        {
                            _rfpList.Add(_rfpIndividual);
                        }
                    }

                    

                    //Assign Dictionary
                    for (int i = 0; i < _srpList.Count; i++)
                    {
                        int p = _srpList[i].Product.Id;
                        _prodCountCheckDict[p] = 0;
                    }

                    //Update Dict Value for Checking
                    for (int i = 0; i < _srrfpList.Count; i++)
                    {
                        int p2 = _srrfpList[i].RFP.Product.Id;
                        //Check total quantity collected is the same as quantity assigned
                        _prodCountCheckDict[p2] = _prodCountCheckDict[p2] + srrfpList[i].ProductAssigned;
                        Debug.WriteLine(_srrfpList[i].RFP.Product.Id + ": " + _prodCountCheckDict[p2]);

                    }

                    //This is to check if the value received is the sme as the value that is assigned.
                    foreach (KeyValuePair<int, int> entry in _prodCountCheckDict)
                    {
                        for (int i = 0; i < _srpList.Count; i++)
                        {
                            if (entry.Key == _srpList[i].Product.Id)
                            {
                                if (entry.Value != _srpList[i].ProductReceivedTotal)
                                {
                                    throw new Exception("The quantity doesnt match");
                                }
                            }
                        }
                    }

                    //update SRP Quantity
                    foreach (StationeryRetrievalProduct _srp in _srpList)
                    {
                        db.StationeryRetrievalProduct.Update(_srp);
                        db.SaveChanges();
                    }

                    //Update SRRFP quantity
                    for (int i = 0; i < srrfpList.Count; i++)
                    {
                        StationeryRetrievalRequisitionFormProduct _srrfp = FindSRRFPById(srrfpList[i].Id);
                        _srrfp.ProductAssigned = srrfpList[i].ProductAssigned;
                        db.StationeryRetrievalRequisitionFormProducts.Update(_srrfp);
                        db.SaveChanges();
                    }

                    //Update SRRF Status
                    foreach (StationeryRetrievalRequisitionForm _srrf in _srrfList)
                    {
                        _srrf.SRRFStatus = SRRFStatus.Assigned;
                        db.Update(_srrf);
                        db.SaveChanges();
                    }

                    //foreach (RequisitionForm _rf in _rfList)
                    //{
                    //    _rf.RFStatus = RFStatus.NotCompleted;
                    //    db.RequisitionForms.Update(_rf);
                    //    db.SaveChanges();
                    //}

                    //Validation
                    foreach (RequisitionFormsProduct _rfp in _rfpList)
                    {
                        List<StationeryRetrievalRequisitionFormProduct> _srrfpCheckList = _srrfpList.Where(x => x.RFP == _rfp).ToList();
                        int sumofProd = _srrfpCheckList.Select(x => x.ProductAssigned).Sum();
                        if (_rfp.ProductApproved < sumofProd)
                        {
                            throw new Exception("Sum of Assigned Products exceeded ProductsApproved");
                        }
                        foreach (StationeryRetrievalRequisitionFormProduct _srrfp in _srrfpCheckList)
                        {
                            if (_rfp.Product.Id == _srrfp.RFP.Product.Id)
                            {
                                if (_rfp.ProductApproved < _rfp.ProductCollectedTotal + _srrfp.ProductAssigned)
                                {
                                    throw new Exception("Sum of Collected Products with Product Assigned is more than the Sum of the Assigned Products");
                                }
                            }
                        }
                    }

                    //Update SR Status
                    _sr.SRStatus = SRStatus.Assigned;
                    _sr.SRAssignedDate = assignDate;
                    db.StationeryRetrievals.Update(_sr);
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

        public StationeryRetrievalRequisitionFormProduct FindSRRFPById(int id)
        {
            return db.StationeryRetrievalRequisitionFormProducts.Find(id);
        }

        public List<RequisitionFormsProduct> GetListOfProductsRequested(List<int> rfIdList)
        {
            List<RequisitionForm> rfList = new List<RequisitionForm>();

            List<RequisitionFormsProduct> rfpConsolidatedList = new List<RequisitionFormsProduct>();

            foreach (int rfId in rfIdList)
            {
                RequisitionForm rf = db.RequisitionForms.Find(rfId);
                rfList.Add(rf);
            }    
            foreach (RequisitionForm rf in rfList)
            {
                List<RequisitionFormsProduct> rfpList = db.RequisitionFormsProducts
                    .Where(x=>x.RequisitionForm == rf)
                    .ToList();
                foreach (RequisitionFormsProduct rfp in rfpList)
                {
                    rfp.ProductBalanceForSR = rfp.ProductApproved - rfp.ProductCollectedTotal;
                    rfpConsolidatedList.Add(rfp);
                }
            }

            return rfpConsolidatedList;
        }

        public List<Product> GetListOfProductsFromRFIdList(List<int> rfIdList)
        {
            List<Product> pList = new List<Product>();
            List<RequisitionForm> rfList = new List<RequisitionForm>();
            List<RequisitionFormsProduct> rfpConsolidatedList = new List<RequisitionFormsProduct>();

            foreach (int rfId in rfIdList)
            {
                RequisitionForm rf = db.RequisitionForms.Find(rfId);
                rfList.Add(rf);
            }
            foreach (RequisitionForm rf in rfList)
            {
                List<RequisitionFormsProduct> rfpList = db.RequisitionFormsProducts
                    .Where(x => x.RequisitionForm == rf)
                    .ToList();
                foreach (RequisitionFormsProduct rfp in rfpList)
                {
                    rfp.ProductBalanceForSR = rfp.ProductApproved - rfp.ProductCollectedTotal;
                    rfpConsolidatedList.Add(rfp);
                }
            }

            IEnumerable<Product> pListEnumerable = rfpConsolidatedList.Select(x => x.Product).Distinct();
            foreach (Product p in pListEnumerable)
            {
                pList.Add(db.Products.Find(p.Id));
            }

            return pList;
        }
        public List<StationeryRetrievalProduct> CreateSRPListFromRFIdList(List<int> rfIdList)
        {
            List<RequisitionForm> rfList = new List<RequisitionForm>();
            List<RequisitionFormsProduct> rfpConsolidatedList = new List<RequisitionFormsProduct>();
            List<StationeryRetrievalProduct> _srpList = new List<StationeryRetrievalProduct>();

            foreach (int rfId in rfIdList)
            {
                RequisitionForm rf = db.RequisitionForms.Find(rfId);
                rfList.Add(rf);
            }
            foreach (RequisitionForm rf in rfList)
            {
                List<RequisitionFormsProduct> rfpList = db.RequisitionFormsProducts
                    .Where(x => x.RequisitionForm == rf)
                    .ToList();
                foreach (RequisitionFormsProduct rfp in rfpList)
                {
                    rfp.ProductBalanceForSR = rfp.ProductApproved - rfp.ProductCollectedTotal;
                    rfpConsolidatedList.Add(rfp);
                }
            }

            IEnumerable<Product> pListEnumerable = rfpConsolidatedList.Select(x => x.Product).Distinct();
            foreach (Product p in pListEnumerable)
            {
                StationeryRetrievalProduct srp = new StationeryRetrievalProduct()
                {
                    Product = p,
                    ProductCount = 0,
                    ProductApprovedCount = 0
                };
                _srpList.Add(srp);
            }

            foreach (StationeryRetrievalProduct srp in _srpList)
            {
                foreach (RequisitionFormsProduct rfpConsolidated in rfpConsolidatedList)
                {
                    if (srp.Product.Id == rfpConsolidated.Product.Id)
                    {
                        srp.ProductCount = rfpConsolidated.ProductBalanceForSR + srp.ProductCount;
                        srp.ProductApprovedCount = rfpConsolidated.ProductApproved + srp.ProductApprovedCount;
                    }
                }
            }
            return _srpList;
        }


    }
}
