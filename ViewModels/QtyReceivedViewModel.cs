using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory_Management_System.Models;

namespace Inventory_Management_System.ViewModels
{
    public class QtyReceivedViewModel
    {
        public List<LoginViewModel> users { get; set; }
        public List<StationeryRetrievalProduct> SRreceivedprods { get; set; }

    }
}
