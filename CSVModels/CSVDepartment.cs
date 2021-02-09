using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class CSVDepartment
    {
        public string DepartmentName { get; set; }
        public string DepHead { get; set; }
        public string PhoneNumber { get; set; }
        public string DeptRep { get; set; }
        public string Email { get; set; }
        public  string CollectionPoint { get; set; }
        public string DeptCode { get; set; }

    }
}
