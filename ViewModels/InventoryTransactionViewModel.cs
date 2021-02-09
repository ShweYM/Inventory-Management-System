using Inventory_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.ViewModels
{
    public class InventoryTransactionViewModel
    {
        public Employee employee { get; set; }
        public List<InventoryTransaction> invTransList { get; set; }
        public List<Product> ProductList { get; set; }
        public string comments { get; set; }
        public List<int> invQuantityTransList { get; set; }
        public int employeeId { get; set; }
        public List<Employee> itEmployeeList { get; set; }
        public InventoryTransaction it { get; set; }
        public List<InventoryTransaction> invPendApprList { get; set; }
    }
}
