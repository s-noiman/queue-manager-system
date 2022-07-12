using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace QueueManager
{
    public class QueueContext : DbContext
    {
        public QueueContext(DbContextOptions<QueueContext> options) : base(options)
        {

        }

        public DbSet<Queue> Queues { get; set; }
    }

    public class Queue
    {
        [Key] 
        public int Id { get; set; }
        public string date { get; set; }
    }
}
