using LearningQ.BL.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningQ.DAL
{
    public class QueueDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=../learningQ.db");
        }

        public QueueDbContext()
        {

        }

        public QueueDbContext(DbContextOptions<QueueDbContext> opt) : base(opt)
        {

        }

        public DbSet<Queue> Queues { get; set; }

       // public DbSet<Item> Items { get; set; } //it's not mandatory to map this because we don't use it by itself
    }
}
