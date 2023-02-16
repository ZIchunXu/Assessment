using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment.Data
{
    public class AssessmentDataContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        private readonly DbContextOptions Options;

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Author> Authors { get; set; }

        public AssessmentDataContext(DbContextOptions options)
            : base(options)
        {
            this.Options = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
