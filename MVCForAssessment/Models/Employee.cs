using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCForAssessment.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpId { get; set; }

        public string EmpName { get; set; }

        public string City { get; set; }

        public decimal Salary { get; set; }

        public int DeptId { get; set; }
        [ForeignKey("DeptId")]
        public Depts Depts { get; set; }
    }
}
