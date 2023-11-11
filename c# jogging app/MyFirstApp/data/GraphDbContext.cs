using Microsoft.EntityFrameworkCore;
using MyFirstApp.Models.Entities;

namespace MyFirstApp.data
{
    public class GraphDbContext : DbContext
    {
        public GraphDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Edge> Edges { get; set; }
        public DbSet<Node> Nodes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Edge>()
                .HasOne(e => e.StartNode)
                .WithMany()
                .HasForeignKey(e => e.StartNodeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Edge>()
                .HasOne(e => e.EndNode)
                .WithMany()
                .HasForeignKey(e => e.EndNodeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Other configurations

            base.OnModelCreating(modelBuilder);
        }
    }
}
