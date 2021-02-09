using Inventory_Management_System.Models;
using Inventory_Management_System.Services;
using Inventory_Management_System.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using Inventory_Management_System.Enums;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Inventory_Management_System.APIControllers
{
    [Route("api/Dashboard")]
    [ApiController]
    public class DashboardAPIController : ControllerBase
    {
        DashboardService dashservice;
        EmployeeService empService;
        CollectionPointService cpService;
        public DashboardAPIController(DashboardService dashservice, EmployeeService empService, CollectionPointService cpService)
        {
            this.dashservice = dashservice;
            this.empService = empService;
            this.cpService = cpService;
        }

        [Route("GetCountDashboard")]
        [HttpGet]
        public IActionResult GetCountById(int id)
        {
            DashboardViewModel dVModel = new DashboardViewModel();

            Employee empCheck = empService.GetEmployeeById(id);
            dVModel.emp = empCheck;
            if (empCheck.Department.DepartmentName != "Store")
            {
                dVModel.deptRep = empService.FindEmployeeWhoIsDeptRep(empCheck.Department);
            }
            dVModel.cpList = cpService.GetCPList();
            if (empCheck.EmployeeType.EmployeeTypeName == "Employee" || empCheck.EmployeeType.EmployeeTypeName == "Department Representative")
            {
                dVModel.TotalRFSubmitted = dashservice.TotalRFNosByEmployee(empCheck, RFStatus.Submitted);
                dVModel.TotalRFApproved = dashservice.TotalRFNosByEmployee(empCheck, RFStatus.Approved);
                dVModel.TotalRFRejected = dashservice.TotalRFNosByEmployee(empCheck, RFStatus.Rejected);
                dVModel.TotalRFNotCompleted = dashservice.TotalRFNosByEmployee(empCheck, RFStatus.NotCompleted);
                dVModel.TotalRFCompleted = dashservice.TotalRFNosByEmployee(empCheck, RFStatus.Completed);
                dVModel.TotalRFOngoing = dashservice.TotalRFNosByEmployee(empCheck, RFStatus.Ongoing);
            }
            if (empCheck.EmployeeType.EmployeeTypeName == "Department Representative")
            {
                dVModel.TotalDFCreated = dashservice.TotalDFNosByDept(empCheck, DFStatus.Created);
                dVModel.TotalDFPendingAssignment = dashservice.TotalDFNosByDept(empCheck, DFStatus.PendingAssignment);
                dVModel.TotalDFPendingDelivery = dashservice.TotalDFNosByDept(empCheck, DFStatus.PendingDelivery);
                dVModel.TotalDFCompleted = dashservice.TotalDFNosByDept(empCheck, DFStatus.Completed);
            }
            if (empCheck.EmployeeType.EmployeeTypeName == "Department Head" || empCheck.TempDeptHeadType != null)
            {
                dVModel.TotalRFSubmittedDept = dashservice.TotalRFNosByDept(empCheck, RFStatus.Submitted);
                dVModel.TotalRFApprovedDept = dashservice.TotalRFNosByDept(empCheck, RFStatus.Approved);
                dVModel.TotalRFRejectedDept = dashservice.TotalRFNosByDept(empCheck, RFStatus.Rejected);
                dVModel.TotalRFNotCompletedDept = dashservice.TotalRFNosByDept(empCheck, RFStatus.NotCompleted);
                dVModel.TotalRFCompletedDept = dashservice.TotalRFNosByDept(empCheck, RFStatus.Completed);
                dVModel.TotalRFOngoingDept = dashservice.TotalRFNosByDept(empCheck, RFStatus.Ongoing);
            }

            if (empCheck.EmployeeType.EmployeeTypeName == "Store Clerk" || empCheck.EmployeeType.EmployeeTypeName == "Store Supervisor" || empCheck.EmployeeType.EmployeeTypeName == "Store Manager")
            {
                dVModel.TotalRFApproved = dashservice.TotalRFNos(empCheck, RFStatus.Approved);
                dVModel.TotalRFNotCompleted = dashservice.TotalRFNos(empCheck, RFStatus.NotCompleted);

                dVModel.TotalSROpen = dashservice.TotalSRNos(empCheck, SRStatus.Open);
                dVModel.TotalSRPendingAssignment = dashservice.TotalSRNos(empCheck, SRStatus.PendingAssignment);
                dVModel.TotalSRAssigned = dashservice.TotalSRNos(empCheck, SRStatus.Assigned);

                dVModel.TotalDFCreated = dashservice.TotalDFNos(empCheck, DFStatus.Created);
                dVModel.TotalDFPendingAssignment = dashservice.TotalDFNos(empCheck, DFStatus.PendingAssignment);
                dVModel.TotalDFPendingDelivery = dashservice.TotalDFNos(empCheck, DFStatus.PendingDelivery);
                dVModel.TotalDFCompleted = dashservice.TotalDFNos(empCheck, DFStatus.Completed);
            }
            if (empCheck.EmployeeType.EmployeeTypeName == "Store Supervisor" || empCheck.EmployeeType.EmployeeTypeName == "Store Manager")
            {
                dVModel.TotalITPendingApproval = dashservice.TotalITNos(empCheck, ITStatus.PendingApproval);
                dVModel.TotalITApproved = dashservice.TotalITNos(empCheck, ITStatus.Approved);
            }
            return Ok(dVModel);
        }


    }
}
