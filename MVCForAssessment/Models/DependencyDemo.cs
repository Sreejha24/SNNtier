using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCForAssessment.Models
{
    public class DependencyDemo : IDependencyDemo
    {
        public int Sum(int a, int b)
        {
            return a + b;
        }
    }
}
