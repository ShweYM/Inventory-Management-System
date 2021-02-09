using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        [ForeignKey("Employee")]
        public int DepHeadId { get; set; }
        public string PhoneNumber { get; set; }
        [ForeignKey("Employee")]
        public string Email { get; set; }
        public  virtual CollectionPoint CollectionPoint { get; set; }
        public string DeptCode { get; set; }
        [ForeignKey("Employee")]
        public int DeptRepId { get; set; }
        [NotMapped]
        public Employee deptHead { get; set; }
        [NotMapped]
        public Employee deptRep { get; set; }

    }
}
