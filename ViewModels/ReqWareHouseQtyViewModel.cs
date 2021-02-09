using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.ViewModels
{
    public class ReqWareHouseQtyViewModel
    {
        public IDictionary<string, List<int>> requisitionItemCount { get; set; }
    }
}
