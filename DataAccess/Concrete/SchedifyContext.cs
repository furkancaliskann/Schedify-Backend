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
    }
}
