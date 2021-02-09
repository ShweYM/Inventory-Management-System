using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inventory_Management_System.Services;
using Inventory_Management_System.Models;
using Inventory_Management_System.ViewModels;
using Newtonsoft.Json;
using Inventory_Management_System.Controllers;

namespace Inventory_Management_System.APIControllers
{
    [Route("api/Stationery")]
    [ApiController]
    public class StationeryAPIController : ControllerBase
    {
        StationeryRetrievalFormService srfservice;
        StationeryRetrievalRequisitionFormService srrfservice;
        ProductService pservice;

        public StationeryAPIController(StationeryRetrievalFormService srfservice, StationeryRetrievalRequisitionFormService srrfservice, ProductService pservice)
        {
            this.srfservice = srfservice;
            this.srrfservice = srrfservice;
            this.pservice = pservice;
          
        }

        //Yamone
        [Route("GetStationeryRetrievals")]
        [HttpGet]
        public IActionResult GetStationeryRetrievals()
        {
            List<StationeryRetrieval> srlist = srfservice.GetStationeryRetrievals();
            return Ok(srlist);
        }

        [Route("GetOpenSRSummary")]
        [HttpGet]
        public IActionResult GetOpenSRSummary()
        {
            StationeryRetrievalSummaryViewModel srsVModel = new StationeryRetrievalSummaryViewModel();
            //emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            //srsVModel.Employee = emp;

            List<StationeryRetrieval> srOpenList = srfservice.GetListSRWithSRStatus(Enums.SRStatus.Open);
            srsVModel.pendingSROpens = srOpenList;

            return Ok(srsVModel);
        }

        [Route("GetPendingSRSummary")]
        [HttpGet]
        public IActionResult GetPendingSRSummary()
        {
            StationeryRetrievalSummaryViewModel srsVModel = new StationeryRetrievalSummaryViewModel();
            //emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            //srsVModel.Employee = emp;

            List<StationeryRetrieval> srPendingList = srfservice.GetListSRWithSRStatus(Enums.SRStatus.PendingAssignment);
            srsVModel.pendingSRAssignments = srPendingList;

            return Ok(srsVModel);
        }

        [Route("GetCompletedSRSummary")]
        [HttpGet]
        public IActionResult GetCompletedSRSummary()
        {
            StationeryRetrievalSummaryViewModel srsVModel = new StationeryRetrievalSummaryViewModel();
            //emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            //srsVModel.Employee = emp;

            List<StationeryRetrieval> srCompletedList = srfservice.GetListSRWithSRStatus(Enums.SRStatus.Assigned);
            srsVModel.completedSRs = srCompletedList;

            return Ok(srsVModel);
        }


        //Yamone
        //To create SRF, Get Requistion List of Pending and Approved 
        [Route("GetPARequistionForm")]
        [HttpGet]
        public IActionResult GetPARequisitionForm()
        {
            List<RequisitionForm> reqformlist = srfservice.GetRequisitionFormList();
            //RequisitionStationaryViewModel rsvm = new RequisitionStationaryViewModel();
            //rsvm.requisition = reqformlist;
            return Ok(reqformlist);
        }

        //Yamone
        //To get unique products and total qty by selected requisition
        [Route("PostProductsBySelectedRequisition")]
        [HttpPost]
        /*public IActionResult GetTotalQtyFromRequisitions(List<int> selectedRequisition)
        {
            IDictionary<string, List<int>> dict_reqwarehouseitem = new Dictionary<string, List<int>>();
            //List<int> qtycount = new List<int>();
            int inventorycount;
            foreach (var item in selectedRequisition)
            {
                List<RequisitionFormsProduct> reqformproduct = srfservice.GetReqFormProducts(item);

                foreach (var productitem in reqformproduct)
                {
                    inventorycount = pservice.FindProductQuantityByProductName(productitem.Product.ProductName);
                    if (dict_reqwarehouseitem.ContainsKey(productitem.Product.ProductName))
                    {
                        dict_reqwarehouseitem[productitem.Product.ProductName][0] = dict_reqwarehouseitem[productitem.Product.ProductName][0] + productitem.ProductApproved - productitem.ProductCollectedTotal;
                    }
                    else
                    {
                        List<int> qtycount = new List<int>();
                        qtycount.Add(productitem.ProductApproved);
                        qtycount.Add(inventorycount);
                        dict_reqwarehouseitem.Add(productitem.Product.ProductName, qtycount);
                    }
                }
            }

            List<StationeryProductViewModel> spvmlist = new List<StationeryProductViewModel>();

            foreach (KeyValuePair<string, List<int>> product in dict_reqwarehouseitem)
            {
                // do something with entry.Value or entry.Key
                StationeryProductViewModel spvm = new StationeryProductViewModel();
                spvm.productname = product.Key;
                spvm.productcount = product.Value[0];
                spvm.warehousecount = product.Value[1];
                spvmlist.Add(spvm);
            }

            //dict_reqwarehouseitem.Keys.Count();
            return Ok(spvmlist);
        }*/

        public IActionResult GetTotalQtyFromRequisitions(List<int> selectedRequisition)
        {
            List<StationeryRetrievalProduct> srpList = srfservice.CreateSRPListFromRFIdList(selectedRequisition);

            RequisitionStationaryViewModel rsvm = new RequisitionStationaryViewModel();
            rsvm.srpList = srpList;
            if(srpList != null)
            {
                return Ok(srpList);
            }
            else
            {
                return null;
            }

        }

        //Yamone
        //createsrf by requistion and products
        [Route("CreateSRForm")]
        [HttpPost]
        public IActionResult SaveToSRProduct([FromBody] StationeryRequisitionProductViewModel srpvm)
        {
            if (srpvm.spvm != null)
            {
                Employee emp = srfservice.getEmployee(srpvm.username);
                //int emp_id = emp.Id;

                //srfservice.SaveStationaryRetrievalProducts(srrfvm.SRProduct, emp, srrfvm.comment, srrfvm.selectedrequisition, srrfvm.srpList);

                List<SRProductViewModel> srpvmlist = new List<SRProductViewModel>();

                foreach(var product in srpvm.spvm)
                {
                    SRProductViewModel spvm = new SRProductViewModel();
                    spvm.productname = product.Product.ProductName;
                    spvm.productqty = product.ProductCount;
                    srpvmlist.Add(spvm);
                }

                srfservice.SaveStationaryRetrievalProducts(srpvmlist, emp, srpvm.comment, srpvm.requisitionIdList, null);

                return Ok(srpvm);
            }
            else
            {
                return null;
            }

        }

        //Yamone
        //Go to created SR form by each summary link
        [Route("GetOpenSRFormById")]
        [HttpGet]
        public IActionResult StationeryRetrievalForm2(int SFId)
        {
            StationeryRetrievalViewModel SRviewModel = new StationeryRetrievalViewModel();
            //emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;

            //SR details_clerkid,date,...to show in SR details page //by htp
            StationeryRetrieval sr = srfservice.GetSRDetails(SFId);
            SRviewModel.stationeryRetrieval = sr;
            //data for requisition form summary in SR details //by htp
            List<RequisitionForm> rfList = srfservice.GetRequisitionDetailBySR_RF(SFId);
            SRviewModel.retrievalRequisitions = rfList;


            //data for warehouse stock preview table(all products) in SR details //by htp
            List<StationeryRetrievalProduct> srpList = srfservice.GetSRDetailProducts(SFId);
            SRviewModel.retrievalProducts = srpList;
            //SRviewModel.employee = emp;
            SRviewModel.srrfList = srfservice.GetSRRFListBySRID(SFId);

            return Ok(SRviewModel);
        }

        //Yamone
        //To save Qty for Open SRF
        [Route("SaveReceivedQtyForOpenSRF")]
        [HttpPost]
        public IActionResult SaveReceivedQty([FromBody] StationeryRetrievalViewModel SRviewModel)
        {
            bool status = srfservice.saveReceivedProds3(SRviewModel.retrievalProducts, SRviewModel.stationeryRetrieval.Id, SRviewModel.warehousepacker, SRviewModel.storeclerk, SRviewModel.srrfList);
            if (status == true)
            {
                return Ok(SRviewModel);
            }
            else
            {
                return null;
            }

        }


        [Route("GetPendingSRFormById")]
        [HttpGet]
        public IActionResult ViewAssignedProductsInPendingSR(int SFId)
        {
            StationeryRetrievalViewModel srVModel = new StationeryRetrievalViewModel();
            //emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            //srVModel.employee = emp;
            srVModel.retrievalProducts = srfservice.GetSRPListForAssign(SFId);
            StationeryRetrieval sr = srfservice.FindBySRId(SFId);
            srVModel.sRRFPList = srfservice.FindSRRFPList(SFId);
            srVModel.stationeryRetrieval = sr;
            srVModel.srrfList = srfservice.GetSRRFListBySRID(SFId);
            return Ok(srVModel);
        }

        [Route("SaveAssignedProductsInPendingSR")]
        [HttpPost]
        public IActionResult SaveAssignedProductsInPendingSR(int srId, [FromBody] StationeryRetrievalViewModel srVModel)
        {
            bool status = srfservice.SetProductsAssignedInSR(srId, srVModel.sRRFPList, srVModel.retrievalProducts, srVModel.srrfList);
            if (status == true)
            {
                return Ok(srVModel);
            }
            else
            {
                return null;
            }
        }

        [Route("GetCompleteSRById")]
        [HttpGet]
        public IActionResult StationeryRetrievalForm(int SFId)
        {
            //emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            StationeryRetrievalViewModel SRviewModel = new StationeryRetrievalViewModel();
            SRviewModel.stationeryRetrieval = srfservice.GetSRDetails(SFId);
            SRviewModel.retrievalRequisitions = srfservice.GetRequisitionDetailBySR_RF(SFId);
            SRviewModel.retrievalProducts = srfservice.GetSRPListForAssign(SFId);
            //SRviewModel.employee = emp;
            SRviewModel.sRRFPList = srfservice.FindSRRFPList(SFId);
            SRviewModel.srrfList = srfservice.FindSRRFList(SFId);

            return Ok(SRviewModel);
        }
    }
}