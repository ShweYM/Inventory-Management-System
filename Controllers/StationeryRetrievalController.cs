using Inventory_Management_System.Models;
using Inventory_Management_System.Services;
using Inventory_Management_System.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Inventory_Management_System.Controllers
{
    [Authorize]
    public class StationeryRetrievalController : Controller
    {
        private StationeryRetrievalFormService srformservice;
        private readonly ILogger<StationeryRetrievalController> _logger;
        private EmployeeService empservice;
        private ProductService pService;
        private Employee emp;

        public StationeryRetrievalController(ILogger<StationeryRetrievalController> logger, StationeryRetrievalFormService srformservice, EmployeeService empservice, ProductService pService)
        {
            _logger = logger;
            this.srformservice = srformservice;
            this.empservice = empservice;
            this.pService = pService;
        }
        public IActionResult Index()
        {
            return View();
        }


        [Route("srf/{id}")]
        public IActionResult StationeryRetrievalForm(int Id)
        {
            emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            StationeryRetrievalViewModel SRviewModel = new StationeryRetrievalViewModel();
            SRviewModel.stationeryRetrieval = srformservice.GetSRDetails(Id);
            SRviewModel.retrievalRequisitions = srformservice.GetRequisitionDetailBySR_RF(Id);
            SRviewModel.retrievalProducts = srformservice.GetSRPListForAssign(Id); 
            SRviewModel.employee = emp;
            SRviewModel.sRRFPList = srformservice.FindSRRFPList(Id);
            SRviewModel.srrfList = srformservice.FindSRRFList(Id);

            return View("StationeryRetrievalFormView", SRviewModel);
        }

        //go to stationery retrieval summary form 
        //by htp
        [Route("srf/summary")]
        public IActionResult StationeryRetrievalSummary()
        {
            StationeryRetrievalSummaryViewModel srsVModel = new StationeryRetrievalSummaryViewModel();
            emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            srsVModel.Employee = emp;

            List<StationeryRetrieval> srOpenList = srformservice.GetListSRWithSRStatus(Enums.SRStatus.Open);
            srsVModel.pendingSROpens = srOpenList;

            List<StationeryRetrieval> srPendingList = srformservice.GetListSRWithSRStatus(Enums.SRStatus.PendingAssignment);
            srsVModel.pendingSRAssignments = srPendingList;

            List<StationeryRetrieval> srCompletedList = srformservice.GetListSRWithSRStatus(Enums.SRStatus.Assigned);
            srsVModel.completedSRs = srCompletedList;

            return View("stationeryRetrievalSummary",srsVModel);
        }


        //Yamone
        [Route("srf/createsrf")]
        public IActionResult CreateStationaryRetrievalForm()
        {
            emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            List<RequisitionForm> reqformlist = srformservice.GetRequisitionFormList();
            RequisitionStationaryViewModel rsvm = new RequisitionStationaryViewModel();
            rsvm.requisition = reqformlist;
            rsvm.emp = emp;
            return View(rsvm);
        }


        public IActionResult GetTotalQtyFromRequisitions(List<int> requistionlist)
        {
            List<StationeryRetrievalProduct> srpList = srformservice.CreateSRPListFromRFIdList(requistionlist);

            RequisitionStationaryViewModel rsvm = new RequisitionStationaryViewModel();
            rsvm.srpList = srpList;
            return PartialView("partial_reqAndwarehouse", rsvm);

        }
        [Route("srf/savesrproducts")]
        [HttpPost]
        public IActionResult SaveToSRProduct([FromBody] RequisitionStationaryViewModel srrfvm)
        {
            bool status = srformservice.SaveStationaryRetrievalProducts(srrfvm.SRProduct, srrfvm.emp, srrfvm.comment, srrfvm.selectedrequisition, srrfvm.srpList);

            if (status == true)
            {
                return new JsonResult(new { success = "Success" });
            }
            else
            {
                return new JsonResult(new { success = "Failure" });
            }

        }
        //save the received products from warehouse and check the login credentials of store clerk+warehouse rep
        [Route("srf/saveReceivedqty")]
        [HttpPost]
        public IActionResult SaveReceivedQty([FromBody] StationeryRetrievalViewModel SRviewModel)
        {            
            bool status = srformservice.saveReceivedProds3(SRviewModel.retrievalProducts, SRviewModel.stationeryRetrieval.Id, SRviewModel.warehousepacker, SRviewModel.storeclerk, SRviewModel.srrfList);

            if (status == true)
            {
                return new JsonResult(new { success = "Success" });
            }
            else
            {
                return new JsonResult(new { success = "Failure" });
            }

        }

        [Route("srf/assign/{id}")]
        public IActionResult ViewAssignedProductsInPendingSR([FromRoute] int id)
        {
            StationeryRetrievalViewModel srVModel = new StationeryRetrievalViewModel();
            emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;
            srVModel.employee = emp;
            srVModel.retrievalProducts = srformservice.GetSRPListForAssign(id);
            StationeryRetrieval sr = srformservice.FindBySRId(id);
            srVModel.sRRFPList = srformservice.FindSRRFPList(id);
            srVModel.stationeryRetrieval = sr;
            srVModel.srrfList = srformservice.GetSRRFListBySRID(id);
            return View("StationeryRetrievalFormAssignment", srVModel);
        }


        [HttpPost("srf/assign/save/{id}")]
        public IActionResult SaveAssignedProductsInPendingSR([FromRoute] int id, [FromBody] StationeryRetrievalViewModel srVModel)
        {
            bool status = srformservice.SetProductsAssignedInSR(id, srVModel.sRRFPList, srVModel.retrievalProducts, srVModel.srrfList);
            if (status == true)
            {
                return new JsonResult(new { success = "Success" });
            }
            else
            {
                return new JsonResult(new { success = "Failure" });
            }
        }

        [Route("srf/open/{id}")]
        public IActionResult StationeryRetrievalForm2([FromRoute] int id)
        {
            StationeryRetrievalViewModel SRviewModel = new StationeryRetrievalViewModel();
            emp = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("employee")) as Employee;

            //SR details_clerkid,date,...to show in SR details page //by htp
            StationeryRetrieval sr = srformservice.GetSRDetails(id);
            SRviewModel.stationeryRetrieval = sr;
            //data for requisition form summary in SR details //by htp
            List<RequisitionForm> rfList = srformservice.GetRequisitionDetailBySR_RF(id);
            SRviewModel.retrievalRequisitions = rfList;


            //data for warehouse stock preview table(all products) in SR details //by htp
            List<StationeryRetrievalProduct> srpList = srformservice.GetSRDetailProducts(id);
            SRviewModel.retrievalProducts = srpList;
            SRviewModel.employee = emp;
            SRviewModel.srrfList = srformservice.GetSRRFListBySRID(id);

            return View("StationeryRetrievalFormTransaction", SRviewModel);
        }
    }
}
