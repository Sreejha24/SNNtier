using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCForAssessment.Models;
using MVCForAssessment.Models.View;

namespace MVCForAssessment.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MVCForAssessment.Models.Employee> Employee { get; set; }
        public DbSet<MVCForAssessment.Models.Depts> Depts { get; set; }
        public DbSet<MVCForAssessment.Models.View.EDepts> EDepts { get; set; }
    }
}
