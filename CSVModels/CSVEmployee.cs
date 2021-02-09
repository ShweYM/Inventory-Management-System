using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class CSVEmployee
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string DeptRep { get; set; }
        public int SupervisedById { get; set; }
        public string EmployeeType { get; set; }
        public string Department { get; set; }
        public string TemporaryHead { get; set; }
        public string OriginalEmployeeType { get; set; }
        

    }
}
