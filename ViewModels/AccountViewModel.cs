using Inventory_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.ViewModels
{
    public class AccountViewModel
    {
        public Employee employee { get; set; }
        public List<CollectionPoint> cList { get; set; }
        public CollectionPoint collectionPoint { get; set; }
    }
}
