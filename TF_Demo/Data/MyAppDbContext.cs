using Microsoft.EntityFrameworkCore;
using TF_Demo.Models;

namespace TF_Demo.Data
{

        public class MyAppDbContext : DbContext
        {
            public MyAppDbContext(DbContextOptions options) : base(options)
            {
            }
            public DbSet<Tf_Employee> Employees { get; set; }
        }
}
