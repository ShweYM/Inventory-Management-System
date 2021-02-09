using Inventory_Management_System.Models;
using Inventory_Management_System.Services;
using Inventory_Management_System.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Inventory_Management_System.Controllers
{
    [Authorize]
    public class TrendAnalysisController : Controller
    {
        private ProductCategoryService productcatservice;
        private DepartmentService deptservice;
        private RequisitionService reqservice;
        private TrendAnalysisService taservice;
        //private readonly ILogger<StationeryRetrievalController> _logger;
        //private EmployeeService empservice;

        public TrendAnalysisController(ProductCategoryService productcatservice, DepartmentService deptservice, RequisitionService reqservice, TrendAnalysisService taservice)
        {
            //_logger = logger;
            this.productcatservice = productcatservice;
            this.deptservice = deptservice;
            this.reqservice = reqservice;
            this.taservice = taservice;
        }

        public IActionResult Index()
        {
            return View();
        }

        //List of Dept List for partial view
        public IActionResult GetDeptList()
        {
            List<Department> deptlist = deptservice.FindDepartmentList();
            return PartialView("_partialDeptList", deptlist);
        }

        //List of Category List for partial view
        public IActionResult GetCategoryList()
        {
            List<ProductCategory> clist = productcatservice.categorylist();
            return PartialView("_partialCategoryList", clist);
        }

        //View Trend Chart for each dept
        public IActionResult GetTotalReqForEachDept(List<int> deptlist)
        {
            List<TrendDeptAndRequisitionByMonth> trenddeptandrequ = reqservice.GetTotalReqForEachDeptForTrend(deptlist);
            if (trenddeptandrequ != null)
            {
                return new JsonResult(trenddeptandrequ);
            }
            else
            {
                return new JsonResult(new { success = "" });
            }

        }

        [Route("/tac/category")]
        public IActionResult TrendAnalysisChartforCategory()
        {


            //TODO: call TrendAnalysisService getTrend method to return latest TrendAnalysisViewModel
            TrendAnalysisChartViewModel taVM = new TrendAnalysisChartViewModel();
            taVM = taservice.getTrend();
            //TODO: return in the form of: return View("ViewRequisitionForm", rVModel);
            return View("TrendAnalysisChartforCategory", taVM);
        }

        [Route("/tac/department")]
        public IActionResult TrendAnalysisChartforDepartment()
        {
            return View();
        }


        public IActionResult GetDataForChartByDepartment()
        {
            return new JsonResult(new { success = "Success" });
        }


    }
}
