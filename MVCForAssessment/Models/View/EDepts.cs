using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCForAssessment.Models.View
{
    public class EDepts
    {
        [Key]
        public int EmpId { get; set; }

        public string EmpName { get; set; }

        public string City { get; set; }

        public decimal Salary { get; set; }

        public int DeptId { get; set; }
        public string DeptName { get; set; }

    }
}
