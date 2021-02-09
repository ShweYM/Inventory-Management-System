using Inventory_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.ViewModels
{
    public class DepartmentViewModel
    {
        public List<Department> deptList { get; set; }
        public Employee employee { get; set; }
    }
}
