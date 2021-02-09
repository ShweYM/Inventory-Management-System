using Inventory_Management_System.Models;
using Inventory_Management_System.Services;
using Inventory_Management_System.Utils;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Inventory_Management_System.DB
{
    public class DBSeeder
    {
        private IWebHostEnvironment _hostingEnvironment;

        public DBSeeder(InventoryManagementSystemContext db,
            IWebHostEnvironment environment,
            ProductService pService,
            DepartmentService dService,
            ProductCategoryService pcService,
            DisbursementFormService dfService,
            StationeryRetrievalFormService srfService,
            EmployeeTypeService etService,
            EmployeeService empService,
            RequisitionService rfService,
            DelegationService delService,
            SupplierService supService,
            SupplierProductService spService,
            InventoryTransactionService itService)
        {
            _hostingEnvironment = environment;

            

            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

            string filePath6 = Path.Combine(uploads, "ProductCategory.csv");
            string jsonString6 = ReadCSVFileUtil.ReadCSVFile(filePath6);
            List<CSVProductCategory> productCategories = (List<CSVProductCategory>)JsonConvert.DeserializeObject(jsonString6, (typeof(List<CSVProductCategory>)));
            pcService.SaveProductCategoryFromCSV(productCategories);

            string filePath7 = Path.Combine(uploads, "Supplier.csv");
            string jsonString7 = ReadCSVFileUtil.ReadCSVFile(filePath7);
            List<CSVSupplier> suppliers = (List<CSVSupplier>)JsonConvert.DeserializeObject(jsonString7, (typeof(List<CSVSupplier>)));
            supService.SaveSuppliersFromCSV(suppliers);

            string filePath3 = Path.Combine(uploads, "EmployeeType.csv");
            string jsonString3 = ReadCSVFileUtil.ReadCSVFile(filePath3);
            List<CSVEmployeeType> employeeTypes = (List<CSVEmployeeType>)JsonConvert.DeserializeObject(jsonString3, (typeof(List<CSVEmployeeType>)));
            etService.SaveEmployeeTypesFromCSV(employeeTypes);

            ProductCategory pc1 = pcService.findProductCategory("Clip");
            ProductCategory pc2 = pcService.findProductCategory("Envelope");
            ProductCategory pc3 = pcService.findProductCategory("Eraser");
            ProductCategory pc4 = pcService.findProductCategory("Exercise");
            ProductCategory pc5 = pcService.findProductCategory("File");
            ProductCategory pc6 = pcService.findProductCategory("Pen");
            ProductCategory pc7 = pcService.findProductCategory("Puncher");
            ProductCategory pc8 = pcService.findProductCategory("Pad");
            ProductCategory pc9 = pcService.findProductCategory("Paper");
            ProductCategory pc10 = pcService.findProductCategory("Ruler");
            ProductCategory pc11 = pcService.findProductCategory("Scissors");
            ProductCategory pc12 = pcService.findProductCategory("Tape");
            ProductCategory pc13 = pcService.findProductCategory("Sharpener");
            ProductCategory pc14 = pcService.findProductCategory("Shorthand");
            ProductCategory pc15 = pcService.findProductCategory("Stapler");
            ProductCategory pc16 = pcService.findProductCategory("Tacks");
            ProductCategory pc17 = pcService.findProductCategory("Tparency");
            ProductCategory pc18 = pcService.findProductCategory("Tray");

            string filePath1 = Path.Combine(uploads, "Product.csv");
            string jsonString = ReadCSVFileUtil.ReadCSVFile(filePath1);
            List<CSVProduct> products = (List<CSVProduct>)JsonConvert.DeserializeObject(jsonString, (typeof(List<CSVProduct>)));
            pService.SaveProductsFromCSV(products);

            

            string filePath9 = Path.Combine(uploads, "SupplierProduct.csv");
            string jsonString9 = ReadCSVFileUtil.ReadCSVFile(filePath9);
            List<CSVSupplierProduct> supplierProducts = (List<CSVSupplierProduct>)JsonConvert.DeserializeObject(jsonString9, (typeof(List<CSVSupplierProduct>)));
            spService.SaveSupplierProductsFromCSV(supplierProducts);

            //save the images under images folder to database
            var imagefolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
            DirectoryInfo Folder = new DirectoryInfo(imagefolder);
            FileInfo[] Images = Folder.GetFiles();
            List<String> imagesList = new List<String>();
            foreach (var image in Images)
            {
                imagesList.Add(String.Format(@"{0}", image.Name));
            }
            pService.SaveProductImages(imagesList);


            EmployeeType empType1 = etService.FindEmployeeTypeByEmployeeTypeName("Store Clerk");
            EmployeeType empType2 = etService.FindEmployeeTypeByEmployeeTypeName("Store Supervisor");
            EmployeeType empType3 = etService.FindEmployeeTypeByEmployeeTypeName("Store Manager");
            EmployeeType empType4 = etService.FindEmployeeTypeByEmployeeTypeName("Employee");
            EmployeeType empType5 = etService.FindEmployeeTypeByEmployeeTypeName("Department Head");
            EmployeeType empType6 = etService.FindEmployeeTypeByEmployeeTypeName("Department Representative");
            EmployeeType empType7 = etService.FindEmployeeTypeByEmployeeTypeName("Warehouse Packer");
            EmployeeType empType8 = etService.FindEmployeeTypeByEmployeeTypeName("Temporary Department Head");


            CollectionPoint cp1 = new CollectionPoint() { CollectionName = "Stationery Store - Administration Building" };
            CollectionPoint cp2 = new CollectionPoint() { CollectionName = "Management School" };
            CollectionPoint cp3 = new CollectionPoint() { CollectionName = "Medical School" };
            CollectionPoint cp4 = new CollectionPoint() { CollectionName = "Engineering School" };
            CollectionPoint cp5 = new CollectionPoint() { CollectionName = "Science School" };
            CollectionPoint cp6 = new CollectionPoint() { CollectionName = "University Hospital" };

            db.Add(cp1);
            db.Add(cp2);
            db.Add(cp3);
            db.Add(cp4);
            db.Add(cp5);
            db.Add(cp6);

            Department dept1 = new Department() { DepartmentName = "Store", DeptCode = "STOR", CollectionPoint = cp1, PhoneNumber = "12345678" };
            Department dept2 = new Department() { DepartmentName = "English Department", DeptCode = "ENDP", CollectionPoint = cp2, PhoneNumber = "98765432" };
            Department dept3 = new Department() { DepartmentName = "Chinese Department", DeptCode = "CHSL", CollectionPoint = cp2, PhoneNumber = "87654321" };
            Department dept4 = new Department() { DepartmentName = "Engineering Department", DeptCode = "ENGD", CollectionPoint = cp1, PhoneNumber = "76543219" };
            Department dept5 = new Department() { DepartmentName = "Arts Department", DeptCode = "ARTD", CollectionPoint = cp3, PhoneNumber = "53046012" };
            Department dept6 = new Department() { DepartmentName = "Business Department", DeptCode = "BIZD", CollectionPoint = cp4, PhoneNumber = "15397864" };
            Department dept7 = new Department() { DepartmentName = "Science Department", DeptCode = "SCID", CollectionPoint = cp5, PhoneNumber = "68425397" };
            Department dept8 = new Department() { DepartmentName = "Management Department", DeptCode = "MNGD", CollectionPoint = cp6, PhoneNumber = "86957412" };
            Department dept9 = new Department() { DepartmentName = "HQ Department", DeptCode = "HQDP", CollectionPoint = cp3, PhoneNumber = "32540325" };
            Department dept10 = new Department() { DepartmentName = "Music Department", DeptCode = "MSDP", CollectionPoint = cp4, PhoneNumber = "78954120" };

            db.Add(dept1);
            db.Add(dept2);
            db.Add(dept3);
            db.Add(dept4);
            db.Add(dept5);
            db.Add(dept6);
            db.Add(dept7);
            db.Add(dept8);
            db.Add(dept9);
            db.Add(dept10);
            db.SaveChanges();

            string filePath4 = Path.Combine(uploads, "Employee.csv");
            string jsonString4 = ReadCSVFileUtil.ReadCSVFile(filePath4);
            List<CSVEmployee> employees = (List<CSVEmployee>)JsonConvert.DeserializeObject(jsonString4, (typeof(List<CSVEmployee>)));
            empService.SaveEmployeesFromCSV(employees);
            empService.SaveEmployeesSupervisedByFromCSV(employees);

            Employee emp1 = empService.GetEmployee("thinn");
            Employee emp2 = empService.GetEmployee("arjun");
            Employee emp3 = empService.GetEmployee("rohan");
            Employee emp4 = empService.GetEmployee("darell");
            Employee emp5 = empService.GetEmployee("yamone");
            Employee emp6 = empService.GetEmployee("sheryl");
            Employee emp7 = empService.GetEmployee("xinqi");
            Employee emp8 = empService.GetEmployee("yanbin");
            Employee emp9 = empService.GetEmployee("darellmgr2");
            Employee emp10 = empService.GetEmployee("thinn2");

            Product pr1 = pService.FindProductById(1);
            Product pr2 = pService.FindProductById(2);
            Product pr3 = pService.FindProductById(3);
            Product pr4 = pService.FindProductById(4);
            Product pr5 = pService.FindProductById(5);
            Product pr6 = pService.FindProductById(6);


            DateTime dd1 = new DateTime(2020, 8, 13, 8, 30, 20);
            DateTime dd2 = new DateTime(2020, 8, 14, 8, 30, 20);
            DateTime dd3 = new DateTime(2020, 8, 12, 8, 40, 20);
            DateTime dd4 = new DateTime(2020, 8, 10, 8, 30, 20);
            DateTime dd13 = new DateTime(2020, 8, 15, 8, 30, 20);



            RequisitionForm rq1 = new RequisitionForm() { RFCode = "RF/" + emp1.Department.DeptCode + "/" + dd1.ToString("ddMMyy") + "/1", Employee = emp1, RFStatus = Enums.RFStatus.Approved, RFDate = dd1, RFApprovalDate = dd1, RFComments = "", RFApprovalBy = emp9 };
            RequisitionForm rq2 = new RequisitionForm() { RFCode = "RF/" + emp1.Department.DeptCode + "/" + dd2.ToString("ddMMyy") + "/1", Employee = emp1, RFStatus = Enums.RFStatus.Ongoing, RFDate = dd2, RFApprovalDate = dd1, RFComments = "", RFApprovalBy = emp9 };
            //RequisitionForm rq3 = new RequisitionForm() { RFCode = "RF/" + emp2.Department.DeptCode + "/" + dd3.ToString("ddMMyy") + "/1", Employee = emp2, RFStatus = Enums.RFStatus.Approved, RFDate = dd3, RFApprovalDate = dd1, RFComments = "", RFApprovalBy = emp9 };
            RequisitionForm rq4 = new RequisitionForm() { RFCode = "RF/" + emp1.Department.DeptCode + "/" + dd1.ToString("ddMMyy") + "/2", Employee = emp1, RFStatus = Enums.RFStatus.Submitted, RFDate = dd1 };
            RequisitionForm rq5 = new RequisitionForm() { RFCode = "RF/" + emp1.Department.DeptCode + "/" + dd2.ToString("ddMMyy") + "/2", Employee = emp1, RFStatus = Enums.RFStatus.Submitted, RFDate = dd2 };

            db.Add(rq1);
            db.Add(rq2);
            //db.Add(rq3);
            db.Add(rq4);
            db.Add(rq5);



            RequisitionFormsProduct rq1_p1 = new RequisitionFormsProduct() { RequisitionForm = rq1, Product = pr1, ProductRequested = 10, ProductApproved = 10 };
            RequisitionFormsProduct rq1_p2 = new RequisitionFormsProduct() { RequisitionForm = rq1, Product = pr2, ProductRequested = 10, ProductApproved = 10 };
            RequisitionFormsProduct rq2_p1 = new RequisitionFormsProduct() { RequisitionForm = rq2, Product = pr1, ProductRequested = 5, ProductApproved = 5 };
            RequisitionFormsProduct rq2_p2 = new RequisitionFormsProduct() { RequisitionForm = rq2, Product = pr2, ProductRequested = 3, ProductApproved = 2 };
            //RequisitionFormsProduct rq3_p1 = new RequisitionFormsProduct() { RequisitionForm = rq3, Product = pr1, ProductRequested = 10, ProductApproved = 5 };
            //RequisitionFormsProduct rq3_p2 = new RequisitionFormsProduct() { RequisitionForm = rq3, Product = pr2, ProductRequested = 10, ProductApproved = 7 };
            RequisitionFormsProduct rq4_p1 = new RequisitionFormsProduct() { RequisitionForm = rq1, Product = pr4, ProductRequested = 10, ProductApproved = 10 };
            RequisitionFormsProduct rq5_p1 = new RequisitionFormsProduct() { RequisitionForm = rq1, Product = pr5, ProductRequested = 10, ProductApproved = 1 };



            db.Add(rq1);
            db.Add(rq2);
            //db.Add(rq3);

            db.Add(rq1_p1);
            db.Add(rq1_p2);
            db.Add(rq2_p1);
            db.Add(rq2_p2);
            //db.Add(rq3_p1);
            //db.Add(rq3_p2);
            db.Add(rq4_p1);
            db.Add(rq5_p1);

            //db.SaveChanges();

            /*For Testing*/
            StationeryRetrieval sr1 = new StationeryRetrieval() { SRCode = "SR/" + dd1.ToString("ddMMyy") + "/1", SRStatus = Enums.SRStatus.PendingAssignment, SRComments = "Pending Assignment", SRDate = dd1, StoreClerk = emp1, WarehousePacker = emp7, SRRetrievalDate = dd2 };
            StationeryRetrieval sr2 = new StationeryRetrieval() { SRCode = "SR/" + dd1.ToString("ddMMyy") + "/2", SRStatus = Enums.SRStatus.Assigned, SRComments = "Assigned", SRDate = dd1, StoreClerk = emp1, WarehousePacker = emp7, SRRetrievalDate = dd2 , SRAssignedDate = dd13  };
            //StationeryRetrieval sr3 = new StationeryRetrieval() { SRCode = "SR/" + dd1.ToString("ddMMyy") + "/3", SRStatus = Enums.SRStatus.Open, SRComments = "Open", SRDate = dd1, StoreClerk = emp1 };
            db.Add(sr1);
            db.Add(sr2);
            //db.Add(sr3);
            db.SaveChanges();

            StationeryRetrievalProduct srpr1 = new StationeryRetrievalProduct() { Product = pr1, ProductRequestedTotal = 5, StationeryRetrieval = sr1, ProductReceivedTotal = 4 };
            StationeryRetrievalProduct srpr2 = new StationeryRetrievalProduct() { Product = pr2, ProductRequestedTotal = 10, StationeryRetrieval = sr1, ProductReceivedTotal = 10 };
            StationeryRetrievalProduct srpr3 = new StationeryRetrievalProduct() { Product = pr1, ProductRequestedTotal = 5, StationeryRetrieval = sr2 };
            StationeryRetrievalProduct srpr4 = new StationeryRetrievalProduct() { Product = pr2, ProductRequestedTotal = 10, StationeryRetrieval = sr2 };

            db.Add(srpr1);
            db.Add(srpr2);
            db.Add(srpr3);
            db.Add(srpr4);
            //db.Add(srpr5);
            //db.Add(srpr6);
            db.SaveChanges();

            StationeryRetrievalRequisitionForm sr1_rf_1 = new StationeryRetrievalRequisitionForm() { RequisitionForm = rq1, SRRFStatus = Enums.SRRFStatus.PendingAssignment, StationeryRetrieval = sr1 };
            StationeryRetrievalRequisitionForm sr1_rf_2 = new StationeryRetrievalRequisitionForm() { RequisitionForm = rq2, SRRFStatus = Enums.SRRFStatus.PendingAssignment, StationeryRetrieval = sr1 };
            //StationeryRetrievalRequisitionForm sr2_rf_3 = new StationeryRetrievalRequisitionForm() { RequisitionForm = rq3, SRRFStatus = Enums.SRRFStatus.PendingAssignment, StationeryRetrieval = sr2 };
            db.Add(sr1_rf_1);
            db.Add(sr1_rf_2);
            //db.Add(sr2_rf_3);
            db.SaveChanges();

            StationeryRetrievalRequisitionFormProduct sr1_rf1_pr_1 = new StationeryRetrievalRequisitionFormProduct() { SR = sr1, RFP = rq1_p1 };
            StationeryRetrievalRequisitionFormProduct sr1_rf1_pr_2 = new StationeryRetrievalRequisitionFormProduct() { SR = sr1, RFP = rq1_p2 };
            StationeryRetrievalRequisitionFormProduct sr1_rf2_pr_1 = new StationeryRetrievalRequisitionFormProduct() { SR = sr1, RFP = rq2_p1 };
            StationeryRetrievalRequisitionFormProduct sr1_rf2_pr_2 = new StationeryRetrievalRequisitionFormProduct() { SR = sr1, RFP = rq2_p2 };
            //StationeryRetrievalRequisitionFormProduct sr1_rf3_pr_1 = new StationeryRetrievalRequisitionFormProduct() { SR = sr1, RFP = rq3_p2 };

            db.Add(sr1_rf1_pr_1);
            db.Add(sr1_rf1_pr_2);
            db.Add(sr1_rf2_pr_1);
            db.Add(sr1_rf2_pr_2);

            db.SaveChanges();
            /*End For Testing*/



            //Testing for RequisitionFormDisplay

            DateTime dd5 = new DateTime(2020, 8, 10, 8, 30, 20);
            DateTime dd6 = new DateTime(2020, 8, 11, 8, 30, 20);
            DateTime dd7 = new DateTime(2020, 8, 10, 8, 30, 20);

            string filePath5 = Path.Combine(uploads, "RequisitionForm.csv");
            string jsonString5 = ReadCSVFileUtil.ReadCSVFile(filePath5);
            List<CSVRequisitionForm> rfs = (List<CSVRequisitionForm>)JsonConvert.DeserializeObject(jsonString5, (typeof(List<CSVRequisitionForm>)));
            rfService.SaveRFFromCSV(rfs);

            RequisitionForm rq6 = new RequisitionForm() { RFCode = "RF/" + emp1.Department.DeptCode + "/" + dd5.ToString("ddMMyy") + "/1", Employee = emp1, RFStatus = Enums.RFStatus.NotCompleted, RFDate = dd5, RFApprovalDate = dd6, RFApprovalBy = emp9 };
            RequisitionFormsProduct rfp1 = new RequisitionFormsProduct() { Product = pr1, ProductRequested = 10, RequisitionForm = rq6, ProductCollectedTotal = 9, ProductApproved = 10 };
            RequisitionFormsProduct rfp2 = new RequisitionFormsProduct() { Product = pr2, ProductRequested = 20, RequisitionForm = rq6, ProductCollectedTotal = 7, ProductApproved = 10 };

            db.Add(rq6);
            db.Add(rfp1);
            db.Add(rfp2);

            DisbursementForm df1 = new DisbursementForm() { CollectionPoint = cp1, DFCode = "DF/" + emp2.Department.DeptCode + "/" + dd1.ToString("ddMMyy") + "/1", DeptRep = emp2, StoreClerk = emp4, DFDeliveryDate = dd2, DFStatus = Enums.DFStatus.Created, DFDate = dd1 };
            DisbursementForm df2 = new DisbursementForm() { CollectionPoint = cp2, DFCode = "DF/" + emp2.Department.DeptCode + "/" + dd1.ToString("ddMMyy") + "/2", DeptRep = emp2, StoreClerk = emp4, DFDeliveryDate = dd2, DFStatus = Enums.DFStatus.Completed, DFDate = dd1 };
            DisbursementFormRequisitionForm dfrf1 = new DisbursementFormRequisitionForm() { DisbursementForm = df1, DFRFStatus = Enums.DFRFStatus.Assigned, RequisitionForm = rq1 };
            DisbursementFormRequisitionForm dfrf2 = new DisbursementFormRequisitionForm() { DisbursementForm = df1, DFRFStatus = Enums.DFRFStatus.Assigned, RequisitionForm = rq2 };
            DisbursementFormRequisitionFormProduct dfrfp1 = new DisbursementFormRequisitionFormProduct() { DisbursementForm = df1, RequisitionFormsProduct = rfp1, ProductCollected = 9 };
            DisbursementFormRequisitionFormProduct dfrfp2 = new DisbursementFormRequisitionFormProduct() { DisbursementForm = df1, RequisitionFormsProduct = rfp2, ProductCollected = 7 };

            db.Add(df1);
            db.Add(df2);
            db.Add(dfrf1);
            db.Add(dfrf2);
            db.Add(dfrfp1);
            db.Add(dfrfp2);
            db.SaveChanges();

            //DisbursementFormRequisitionForm dfrf3 = new DisbursementFormRequisitionForm() { DFRFStatus = Enums.DFRFStatus.PendingDelivery, DisbursementForm = df1, RequisitionForm = rq1 };
            //DisbursementFormRequisitionForm dfrf4 = new DisbursementFormRequisitionForm() { DFRFStatus = Enums.DFRFStatus.PendingDelivery, DisbursementForm = df1, RequisitionForm = rq2 };
            //db.Add(dfrf3);
            //db.Add(dfrf4);
            db.SaveChanges();
            DisbursementFormProduct dfp1 = new DisbursementFormProduct() { DisbursementForm = df1, Product = pr1, ProductToDeliverTotal = 10 };
            DisbursementFormProduct dfp2 = new DisbursementFormProduct() { DisbursementForm = df1, Product = pr2, ProductToDeliverTotal = 40 };
            db.Add(dfp1);
            db.Add(dfp2);
            db.SaveChanges();

            
            Supplier sup1 = db.Suppliers.Find(1);
            Supplier sup2 = db.Suppliers.Find(2);
            Supplier sup3 = db.Suppliers.Find(3);
            Supplier sup4 = db.Suppliers.Find(4);
            Supplier sup5 = db.Suppliers.Find(5);
            Supplier sup6 = db.Suppliers.Find(6);
            Supplier sup7 = db.Suppliers.Find(7);
            Supplier sup8 = db.Suppliers.Find(8);
            Supplier sup9 = db.Suppliers.Find(9);
            Supplier sup10 = db.Suppliers.Find(10);
            Supplier sup11 = db.Suppliers.Find(11);
            Supplier sup12 = db.Suppliers.Find(12);
            Supplier sup13 = db.Suppliers.Find(13);
            Supplier sup14 = db.Suppliers.Find(14);
            Supplier sup15 = db.Suppliers.Find(15);
            Supplier sup16 = db.Suppliers.Find(16);
            Supplier sup17 = db.Suppliers.Find(17);
            Supplier sup18 = db.Suppliers.Find(18);
            Supplier sup19 = db.Suppliers.Find(19);
            Supplier sup20 = db.Suppliers.Find(20);

            //db.Add(sup1);
            //db.Add(sup2);
            //db.Add(sup3);
            //db.Add(sup4);
            db.SaveChanges();

            DateTime dd8 = new DateTime(2020, 8, 10, 8, 30, 20);
            DateTime dd9 = new DateTime(2020, 8, 22, 8, 30, 20);
            DateTime dd10 = new DateTime(2020, 7, 10, 8, 40, 20);
            DateTime dd11 = new DateTime(2020, 9, 12, 8, 30, 20);
            DateTime dd12 = new DateTime(2020, 9, 13, 9, 30, 20);

            SupplierProduct sp1 = db.SupplierProducts.Find(1);
            SupplierProduct sp2 = db.SupplierProducts.Find(6);
            SupplierProduct sp3 = db.SupplierProducts.Find(3);
            SupplierProduct sp4 = db.SupplierProducts.Find(4);
            //db.Add(sp1);
            //db.Add(sp2);
            //db.Add(sp3);
            //db.Add(sp4);
            db.SaveChanges();

            PurchaseOrder po1 = new PurchaseOrder() {supplier = sup1, DeliverTo="Logic University - Store", expectedDeliveryDate=dd9, IssuedBy = emp7, POStatus=Enums.POStatus.Issued, POComments= "Order affected by COVID", POCode = "PO/" + dd8.ToString("ddMMyy") + "/1", POIssueDate=dd8 };
            PurchaseOrder po2 = new PurchaseOrder() { supplier = sup2, DeliverTo = "Logic University - Store", expectedDeliveryDate = dd11, IssuedBy = emp7, POStatus = Enums.POStatus.Completed, POComments = "Some Items may be out of stock", POCode = "PO/" + dd10.ToString("ddMMyy") + "/1", POIssueDate = dd10 };
            PurchaseOrder po3 = new PurchaseOrder() { supplier = sup2, DeliverTo = "Logic University - Store", expectedDeliveryDate = dd12, IssuedBy = emp7, POStatus = Enums.POStatus.NotCompleted, POComments = "Urgent", POCode = "PO/" + dd10.ToString("ddMMyy") + "/2", POIssueDate = dd10};

            db.Add(po1);
            db.Add(po2);
            db.Add(po3);


            PurchaseOrderSupplierProduct posr1 = new PurchaseOrderSupplierProduct()
            {SupplierProduct = sp1, POQuantityRequested = 10, POUnitPrice = sp1.ProductPrice, PurchaseOrder = po1 };
            PurchaseOrderSupplierProduct posr2 = new PurchaseOrderSupplierProduct()
            { SupplierProduct = sp2, POQuantityRequested = 15, POUnitPrice = sp2.ProductPrice, PurchaseOrder = po1 };
            PurchaseOrderSupplierProduct posr3 = new PurchaseOrderSupplierProduct()
            { SupplierProduct = sp3, POQuantityRequested = 5, POUnitPrice = sp3.ProductPrice, PurchaseOrder = po1 };

            PurchaseOrderSupplierProduct posr4 = new PurchaseOrderSupplierProduct()
            { SupplierProduct = sp1, POQuantityRequested = 3, POUnitPrice = sp1.ProductPrice, PurchaseOrder = po2 };
            PurchaseOrderSupplierProduct posr5 = new PurchaseOrderSupplierProduct()
            { SupplierProduct = sp2, POQuantityRequested = 5, POUnitPrice = sp2.ProductPrice, PurchaseOrder = po2 };

            PurchaseOrderSupplierProduct posr6 = new PurchaseOrderSupplierProduct()
            { SupplierProduct = sp3, POQuantityRequested = 3, POUnitPrice = sp3.ProductPrice, PurchaseOrder = po3 };
            PurchaseOrderSupplierProduct posr7 = new PurchaseOrderSupplierProduct()
            { SupplierProduct = sp4, POQuantityRequested = 5, POUnitPrice = sp4.ProductPrice, PurchaseOrder = po3 };

            db.Add(posr1);
            db.Add(posr2);
            db.Add(posr3);
            db.Add(posr4);
            db.SaveChanges();
            //--------------------------

            DeliveryOrder do2 = new DeliveryOrder() { DeliveryOrderNo = "D0121567", DOReceivedDate = dd11, PurchaseOrder = po2, Supplier = sup1, DOCode = "DO/" + dd11.ToString("ddMMyy") + "/1", ReceivedBy = emp7 };
            DeliveryOrder do3 = new DeliveryOrder() { DeliveryOrderNo = "D011267", DOReceivedDate = dd12, PurchaseOrder = po3, Supplier = sup2, DOCode = "DO/" + dd12.ToString("ddMMyy") + "/1", ReceivedBy = emp7 };
            db.Add(do2);
            db.Add(do3);
            db.SaveChanges();

            DeliveryOrderSupplierProduct dosr4 = new DeliveryOrderSupplierProduct()
            { PurchaseOrderSupplierProduct = posr4, DOQuantityReceived = 3, DeliveryOrder = do2 };
            DeliveryOrderSupplierProduct dosr5 = new DeliveryOrderSupplierProduct()
            { PurchaseOrderSupplierProduct = posr5, DOQuantityReceived = 5, DeliveryOrder = do2 };

            DeliveryOrderSupplierProduct dosr6 = new DeliveryOrderSupplierProduct()
            { PurchaseOrderSupplierProduct = posr6, DOQuantityReceived = 3, DeliveryOrder = do3 };
            DeliveryOrderSupplierProduct dosr7 = new DeliveryOrderSupplierProduct()
            { PurchaseOrderSupplierProduct = posr7, DOQuantityReceived = 3, DeliveryOrder = do3 };

            db.Add(dosr4);
            db.Add(dosr5);
            db.Add(dosr6);
            db.Add(dosr7);

            DateTime dd19 = new DateTime(2020, 8, 15, 0, 0, 0);
            DateTime dd20 = new DateTime(2020, 9, 15,0, 0, 0);
            DateTime dd21 = new DateTime(2020, 7, 15, 10, 10, 0);

            DateTime dd22 = new DateTime(2020, 10, 15, 0, 0, 0);
            DateTime dd23 = new DateTime(2020, 11, 15, 0, 0, 0);


            DelegationForm delForm1 = new DelegationForm()
            {
                startDate = dd19,
                endDate = dd20,
                delegateComment = "Hello There",
                Delegatee = emp10,
                DelegatedType = empType8,
                DepartmentHead = emp9,
                DLAssignedDate = dd21,
                DLStatus = Enums.DLStatus.Ongoing
            };

            DelegationForm delForm2 = new DelegationForm()
            {
                startDate = dd22,
                endDate = dd23,
                delegateComment = "Hello There",
                Delegatee = emp10,
                DelegatedType = empType8,
                DepartmentHead = emp9,
                DLAssignedDate = dd21,
                DLStatus = Enums.DLStatus.Assigned
            };

            //DelegationForm delForm3 = new DelegationForm()
            //{
            //    startDate = dd22,
            //    endDate = dd23,
            //    delegateComment = "Hello There",
            //    Delegatee = emp10,
            //    DelegatedType = empType6,
            //    DepartmentHead = emp9,
            //    DLAssignedDate = dd21,
            //    DLStatus = Enums.DLStatus.Assigned
            //};

            db.Add(delForm1);
            db.Add(delForm2);

            db.SaveChanges();
        }
    }
}
