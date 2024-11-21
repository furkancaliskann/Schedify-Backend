using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class SchedifyContext : DbContext
    {

        public SchedifyContext(DbContextOptions<SchedifyContext> options): base(options) { }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>()
                .HasOne(t => t.CreatedUser)
                .WithMany()
                .HasForeignKey(t => t.CreatedUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
