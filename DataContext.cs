using Microsoft.EntityFrameworkCore;
using pantry_be.Models;

namespace pantry_be
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Pantry> Pantries { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
   
        public DbSet<Step> Steps { get; set; }  
    }
}
