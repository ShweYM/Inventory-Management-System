using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string DeptRep { get; set; }
        public virtual Employee SupervisedBy { get; set; }
        public virtual EmployeeType EmployeeType { get; set; }
        public virtual Department Department { get; set; }
        //public virtual EmployeeType OriginalEmployeeType { get; set; }
        public virtual EmployeeType TempDeptHeadType { get; set; }




    }

}
