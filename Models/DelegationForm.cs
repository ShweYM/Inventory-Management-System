using Inventory_Management_System.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class DelegationForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //public virtual Employee Employee { get; set; }
        //public int DepartmentHeadId { get; set; }
        public virtual Employee DepartmentHead { get; set; }
        public virtual Employee Delegatee { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string delegateComment { get; set; }
        [Required]
        public DLStatus DLStatus { get; set; }
        //public int DelegatedType { get; set; }
        public DateTime DLAssignedDate { get; set; }
        public virtual EmployeeType DelegatedType { get; set; }
    }
}
