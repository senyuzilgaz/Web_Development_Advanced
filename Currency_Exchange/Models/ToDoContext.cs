using Microsoft.EntityFrameworkCore;

namespace ToDoApi.Models
{
    public class ToDoContext : DbContext 
    {
        public ToDoContext(DbContextOptions<ToDoContext> options)
            : base(options)
        {

        }

        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}