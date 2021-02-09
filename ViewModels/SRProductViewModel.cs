using Inventory_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.ViewModels
{
    public class SRProductViewModel
    {
        public Employee employee { get; set; }
        public String productname { get; set; }
        public int productqty { get; set; }

    }
}
