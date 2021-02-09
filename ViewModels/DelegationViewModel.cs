using Inventory_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.ViewModels
{
    public class DelegationViewModel
    {
        public List<DelegationForm> delegationList { get; set; }
        public Employee employee { get; set; }
        public Employee DepartmentHead { get; set; }
        public Employee delegatee { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string delegateComment { get; set; }
        public List<Employee> DeptEmployeeList { get; set; }
        public Employee AssignedDeptRep { get; set; }
        public DelegationForm dForm { get; set; }
    }
}
