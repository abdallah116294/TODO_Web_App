using Microsoft.EntityFrameworkCore;

namespace TODO_Web_App.Models
{
    public class ToDoContext:DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
        }
        public DbSet<ToDO> ToDOs { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Status> Statuses { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
               new Category { categoryID = "work", categoryName = "Work" },
               new Category { categoryID = "home", categoryName = "Home" },
               new Category { categoryID = "shop", categoryName = "Shopping" },
               new Category { categoryID = "call", categoryName = "Contact" },
               new Category { categoryID = "ex", categoryName = "Exercise" }
                );
            modelBuilder.Entity<Status>().HasData(
         new Status { statusID = "open", statusName = "Open" },
         new Status { statusID = "closed", statusName = "Closed" }
     );
        }
    }
}
