using Microsoft.EntityFrameworkCore;
using VirtualLabAPI.Models;

namespace VirtualLabAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
		public DbSet<VirtualLabAPI.Models.Task> Tasks { get; set; }
		public DbSet<TaskInProgress> TasksInProgress { get; set; }
    }
}
