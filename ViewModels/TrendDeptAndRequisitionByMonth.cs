using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.ViewModels
{
    public class TrendDeptAndRequisitionByMonth
    {
        public string month { get; set; }
        public List<TrendDeptAndRequisition> deptreqlist { get; set; }
    }
}
